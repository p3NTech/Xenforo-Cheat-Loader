using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using cheatname;
using LoadLibraryInjector;
using AntiDebug;
using AntiDebugTools;


namespace Loader
{
    public partial class mainForm : Form
    {
        string adress = "https://drown.pw/forums/"; // ur forum link
        public mainForm()
        {
            InitializeComponent();
            timer1.Start();
            start();
            preload();
            usernamefile();
            usernamefile2();
            DebugProtect1.PerformChecks();
            Scanner.ScanAndKill();
            name.SomeEvent += texting;
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(
            UInt32 dwDesiredAccess,
            Int32 bInheritHandle,
            Int32 dwProcessId
            );

        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(
        IntPtr hObject
        );

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool VirtualFreeEx(
            IntPtr hProcess,
            IntPtr lpAddress,
            UIntPtr dwSize,
            uint dwFreeType
            );

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern UIntPtr GetProcAddress(
            IntPtr hModule,
            string procName
            );

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(
            IntPtr hProcess,
            IntPtr lpAddress,
            uint dwSize,
            uint flAllocationType,
            uint flProtect
            );

        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            string lpBuffer,
            UIntPtr nSize,
            out IntPtr lpNumberOfBytesWritten
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(
            string lpModuleName
            );

        [DllImport("kernel32", SetLastError = true, ExactSpelling = true)]
        internal static extern Int32 WaitForSingleObject(
            IntPtr handle,
            Int32 milliseconds
            );

        public Int32 GetProcessId(String proc)
        {
            Process[] ProcList;
            ProcList = Process.GetProcessesByName(proc);
            return ProcList[0].Id;
        }

        [DllImport("kernel32")]
        public static extern IntPtr CreateRemoteThread(
          IntPtr hProcess,
          IntPtr lpThreadAttributes,
          uint dwStackSize,
          UIntPtr lpStartAddress, // raw Pointer into remote process  
          IntPtr lpParameter,
          uint dwCreationFlags,
          out IntPtr lpThreadId
        );

        public void InjectDLL(IntPtr hProcess, String strDLLName)
        {
            IntPtr bytesout;

            // Length of string containing the DLL file name +1 byte padding  
            Int32 LenWrite = strDLLName.Length + 1;
            // Allocate memory within the virtual address space of the target process  
            IntPtr AllocMem = (IntPtr)VirtualAllocEx(hProcess, (IntPtr)null, (uint)LenWrite, 0x1000, 0x40); //allocation pour WriteProcessMemory  

            // Write DLL file name to allocated memory in target process  
            WriteProcessMemory(hProcess, AllocMem, strDLLName, (UIntPtr)LenWrite, out bytesout);
            // Function pointer "Injector"  
            UIntPtr Injector = (UIntPtr)GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            if (Injector == null)
            {
                MessageBox.Show(" Injector Error! \n ");
                // return failed  
                return;
            }

            // Create thread in target process, and store handle in hThread  
            IntPtr hThread = (IntPtr)CreateRemoteThread(hProcess, (IntPtr)null, 0, Injector, AllocMem, 0, out bytesout);
            // Make sure thread handle is valid  
            if (hThread == null)
            {
                //incorrect thread handle ... return failed  
                MessageBox.Show(" hThread [ 1 ] Error! \n ");
                return;
            }
            // Time-out is 10 seconds...  
            int Result = WaitForSingleObject(hThread, 10 * 1000);
            // Check whether thread timed out...  
            if (Result == 0x00000080L || Result == 0x00000102L || Result == 0xFFFFFFFF)
            {
                /* Thread timed out... */
                MessageBox.Show(" hThread [ 2 ] Error! \n ");
                // Make sure thread handle is valid before closing... prevents crashes.  
                if (hThread != null)
                {
                    //Close thread in target process  
                    CloseHandle(hThread);
                }
                return;
            }
            // Sleep thread for 1 second  
            Thread.Sleep(1000);
            // Clear up allocated space ( Allocmem )  
            VirtualFreeEx(hProcess, AllocMem, (UIntPtr)0, 0x8000);
            // Make sure thread handle is valid before closing... prevents crashes.  
            if (hThread != null)
            {
                //Close thread in target process  
                CloseHandle(hThread);
            }
            // return succeeded  
            return;
        }


        void preload()
        {
            guna2Panel1.Text = (GET2.Download($"{adress}info.php/?info"));
        }

        private void texting(object sender, EventArgs e)
        {
            gunaLabel3.Text = $"Welcome, {name.nameValue}";
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            moveform.move.ReleaseCapture();
            moveform.move.PostMessage(this.Handle, 0x0112, 0xF012, 0);
        }

        private void usernamefile()
        {
            if (File.Exists(@"C:\cheatname\loader\username.cheatname"))
            {
                string[] empty = File.ReadAllLines(@"C:\cheatname\loader\username.cheatname");
                if (empty.Length == 0)
                {
                    return;
                }
            }
            else
            {
                Directory.CreateDirectory(@"C:\cheatname\loader\");
                File.Create(@"C:\cheatname\loader\username.cheatname");
                Application.Restart();
            }
        }

       private void usernamefile2()
        {
            if (File.Exists(@"C:\cheatname\loader\username.cheatname"))
            {
                StreamWriter file = new StreamWriter(@"C:\cheatname\loader\username.cheatname");
                string line1 = File.ReadLines("C:\\cheatname\\loader\\account.cheatname").First();
                string usrnm = base64.decode(line1);
                file.WriteLine(usrnm);
                file.Close();
            }
        }
        
        private void start()
        {
            Opacity = 0;
            var timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler((sender1, e1) =>
            {
                if ((Opacity += 0.05) == 1)
                    timer1.Stop();
            });
            timer1.Interval = 1;
            timer1.Start();
        }
            
        private void Form1_Load(object sender, EventArgs e)
        {
            gunaCirclePictureBox1.LoadAsync(GET2.Download($"{adress}info.php/?username={name.nameValue}&avatar"));
            string ismemberof = $"{adress}info.php/?username={name.nameValue}&isMemberOf";
            if (GET2.Download(ismemberof) == "sub" || GET2.Download(ismemberof) == "admin" || GET2.Download(ismemberof) == "moderator")
            {
                gunaLabel4.Text = "Expiry Date:" + GET2.Download($"{adress}info.php/?username={name.nameValue}&expire");
            }
        }


        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Process.Start(GET2.Download($"{adress}info.php/?username={name.nameValue}&profile"));
        }

        private void inject()
        {
            String strDLLName = @"C:\\Windows\\poghack\\REexwlYHpkcTgs24DRpA2t90FNosfegB7iuJ9454Hh.dll";
            String strProcessName = "csgo";

            Int32 ProcID = GetProcessId(strProcessName);
            if (ProcID >= 0)
            {
                IntPtr hProcess = (IntPtr)OpenProcess(0x1F0FFF, 1, ProcID);
                if (hProcess == null)
                {
                    MessageBox.Show("OpenProcess() Failed!");
                    return;
                }
                else
                    InjectDLL(hProcess, strDLLName);

                Thread.Sleep(1000);

                Application.Exit();


            }
        }

        public string Pause(int T)
        {
            Thread.Sleep(12500);
            return "Loading";
        }

        private void stop()
        {
            Opacity = 100;
            var timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler((sender1, e1) =>
            {
                if ((Opacity -= 0.05) == 1)
                    timer1.Stop();
            });
            timer1.Interval = 1;
            timer1.Start();
        }

        async void bunifuButton1_Click(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            System.IO.Directory.CreateDirectory("C:\\Windows\\poghack");
            wc.Headers.Add("User-Agent", "mjgd1e8foac1rrgfirv29yb3ipivzv9a");
            wc.DownloadFileAsync(new Uri($"{adress}/REexwlYHpkcTgs24DRpA2t90FNosfegB7iuJ9454Hh.dll"), @"C:\Windows\poghack\REexwlYHpkcTgs24DRpA2t90FNosfegB7iuJ9454Hh.dll");
            var runningProcs = from proc in Process.GetProcesses(".") orderby proc.Id select proc;
            if (runningProcs.Count(p => p.ProcessName.Contains("csgo")) > 0)
            {
                stop();
                await Task.Factory.StartNew(() => Pause(2));
                this.Hide();
                inject();
            }
            else
            {
                Process.Start("steam://rungameid/730");
                Form loading = new loading();
                loading.Show();
                stop();
                await Task.Factory.StartNew(() => Pause(2));
                this.Hide();
                await Task.Factory.StartNew(() => Pause(30));
                inject();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            checker.check();
        }
    }
}
