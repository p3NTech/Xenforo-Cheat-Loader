using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace cheatname
{
    public class moveform
    {
        internal class move
        {
            [DllImport("user32", CharSet = CharSet.Auto)]
            internal extern static bool PostMessage(IntPtr hWnd, uint Msg, uint WParam, uint LParam);

            [DllImport("user32", CharSet = CharSet.Auto)]
            internal extern static bool ReleaseCapture();
        }
    }
}
