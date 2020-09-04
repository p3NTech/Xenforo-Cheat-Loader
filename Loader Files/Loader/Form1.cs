using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Security.Principal;
using loader;
using System.Drawing;
using System.Diagnostics;
using System.Net;
using cheatname;
using HWIDGrabber;
using AntiDebug;
using AntiDebugTools;

namespace Loader
{
    public partial class Form1 : Form
    {
        string adress = "https://drown.pw/forums/"; // ur forum link
        string secret = "6d6ye3SUkGKUC4I5b088R1w3otqHi3lyxsRT4i15hL";
        string version = "1.3.3.7";
        bool hwid;
        string hwidstring;
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            AdminRelauncher();
            versions();
            DebugProtect1.PerformChecks();
            Scanner.ScanAndKill();
            start();
        }
        private void AdminRelauncher()
        {
            bool isElevated;
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);

            if (isElevated == false)
            {
                gunaGradientButton1.Enabled = false;
                gunaLabel2.Visible = true;
                login.Enabled = false;
                password.Enabled = false;
                gunaLabel2.Text = "Please run the loader as administrator";
            };
        }

        private bool sub()
        {
            if (GET2.Download($"{adress}info.php/?username=" + login.Text + "&isMemberOf") == "nosub" || GET2.Download($"{adress}info.php/?username=" + login.Text + "&expire") == "nosub")
            {
                return false;
            }
            else if (GET2.Download($"{adress}info.php/?username=" + login.Text + "&isMemberOf") != "nosub" || GET2.Download($"{adress}info.php/?username=" + login.Text + "&expire") != "nosub")
            {
                return true;
            }
            return true;
        }

        private bool hwidcheck()
        {
            hwidstring = HWDI.GetMachineGuid();
            if (GET2.Download($"{adress}hwid.php/?username=" + login.Text + "&hwid=" + hwidstring) == "0" || GET2.Download($"{adress}info.php/?username=" + login.Text + "&expire") == "4")
            {
                return false;
            }
            else if (GET2.Download($"{adress}hwid.php/?username=" + login.Text + "&hwid=" + hwidstring) == "1" || GET2.Download($"{adress}hwid.php/?username=" + login.Text + "&hwid=" + hwidstring) == "3" || GET2.Download($"{adress}hwid.php/?username=" + login.Text + "&hwid=" + hwidstring) == "2")
            {
                return true;
            }
            return true;
        }
        
        private void versions()
        {
            if (GET2.Download($"{adress}info.php?version") != version)
            {
                gunaGradientButton1.Enabled = false;
                login.Enabled = false;
                password.Enabled = false;
                gunaLabel2.Visible = true;
                gunaLabel2.Text = "Please update the loader.";
            }
            else { }
        }

        /* private void makepoghack()
         {
             System.IO.Directory.CreateDirectory("C:\\Windows\\poghack");
         }*/

        /*private void dlCheat()
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "f68f1fe894d548a1bbc66165c46e61eb");
            System.IO.Directory.CreateDirectory("C:\\Windows\\poghack");
            wc.DownloadFileAsync(new Uri($"{adress}/REexwlYHpkcTgs24DRpA2t90FNosfegB7iuJ9454Hh.dll"), @"C:\Windows\poghack\REexwlYHpkcTgs24DRpA2t90FNosfegB7iuJ9454Hh.dll");
        }*/

        private void start()
        {
            Opacity = 0;
            var timer1 = new Timer();
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
            if (File.Exists(@"C:\cheatname\loader\account.cheatname"))
            {
                string[] empty = File.ReadAllLines(@"C:\cheatname\loader\account.cheatname");
                if (empty.Length == 0)
                {
                    return;
                }
                string password_decode = File.ReadLines(@"C:\cheatname\loader\account.cheatname").Skip(1).First();
                string login_decode = File.ReadLines(@"C:\cheatname\loader\account.cheatname").Skip(0).First();
                login.Text = base64.decode(login_decode);
                password.Text = base64.decode(password_decode);
            }
            else
            {
                Directory.CreateDirectory(@"C:\cheatname\loader\");
                File.Create(@"C:\cheatname\loader\account.cheatname");
                Application.Restart();
            }
        }


        private void materialFlatButton1_Click(object sender, EventArgs e)
        {

            try
            {
                if (hwidcheck())
                {
                    if (sub())
                    {
                        {
                            if (GET2.Download($"{adress}info.php?username={login.Text}&password={password.Text}") == "success")
                            {
                                StreamWriter file = new StreamWriter(@"C:\cheatname\loader\account.cheatname");
                                string encode_login = base64.encode(login.Text);
                                string encode_password = base64.encode(password.Text);
                                file.WriteLine(encode_login);
                                file.WriteLine(encode_password);
                                file.Close();
                                Form ifrm = new mainForm();
                                name.nameValue = login.Text;
                                ifrm.Show();
                                this.Hide();
                            }
                            else gunaLabel2.Visible = true;
                            gunaLabel2.Text = "Invalid Credentials";
                        }
                    }
                    else
                    {
                        gunaLabel2.Visible = true;
                        gunaLabel2.Text = "No subscription";
                    }
                }
                else
                {
                    gunaLabel2.Visible = true;
                    gunaLabel2.Text = "Invalid HWID";
                }
            }

            catch (Exception) //Exception ex
            {
                MessageBox.Show("Internal error, contact staff");//MessageBox.Show(ex.Message);
            }
        }


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            moveform.move.ReleaseCapture();
            moveform.move.PostMessage(this.Handle, 0x0112, 0xF012, 0);
        }



        private void gunaLinkLabel1_MouseClick(object sender, MouseEventArgs e)
        {
            Process.Start($"{adress}lost-password/");
        }

        private void GunaButton1_Click(object sender, EventArgs e)
        {
            Process.Start($"{adress}register/");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            checker.check();
        }

        private void gunaLabel4_Click(object sender, EventArgs e)
        {

        }
    }
}