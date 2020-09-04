using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace cheatname
{
    internal class checker
    {
        public static void killproc(string proccid)
        {
            var runningProcs = from proc in Process.GetProcesses(".") orderby proc.Id select proc;
            if (runningProcs.Count(p => p.ProcessName.Contains(proccid)) > 0)
            {
                foreach (Process proc in Process.GetProcessesByName("csgo"))
                {
                    proc.Kill();     
                }
                foreach (Process proc in Process.GetProcessesByName("loader"))
                {
                    proc.Kill();
                }
            }
        }

        public static void check()
        {
            killproc("parsecd");
            killproc("HTTPDebuggerUI");
            killproc("ida64");
            killproc("dnSpy");
            killproc("megadumper");
            killproc("x64dbg");
            killproc("ProcessHacker");
        }
    }
}
