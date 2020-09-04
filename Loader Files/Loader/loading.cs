using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace cheatname
{
    public partial class loading : Form
    {
        public loading()
        {
            InitializeComponent();
        }

        public string Pause(int T)
        {
            Thread.Sleep(T * 1000);
            return "Loading";
        }

        async void progress()
        {
            guna2CircleProgressBar1.Maximum = 100;    
            for (int i = 0; i < 100; i++)
            {
                guna2CircleProgressBar1.Value = i;
                await Task.Delay(250);
            }
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

        async void loading_Load(object sender, EventArgs e)
        {
            progress();
            await Task.Factory.StartNew(() => Pause(30));
            this.Enabled = false;
        }

            
        async void loading_EnabledChanged(object sender, EventArgs e)
        {
            stop();
            await Task.Factory.StartNew(() => Pause(2));
            this.Hide();
        }

        private void exitLoader()
        {
            Thread.Sleep(240000);
            foreach (Process proc in Process.GetProcessesByName("cheatname"))
            {
                proc.Kill();
            }
        }

        }
    }
