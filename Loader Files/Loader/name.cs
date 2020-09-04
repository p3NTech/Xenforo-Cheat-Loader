using System;

namespace Loader
{
    public static class name
    {
        public static string nameValue
        {
            get { return namevalue; }
            set { namevalue = value; if (SomeEvent != null) SomeEvent(null, EventArgs.Empty); }
        }

        static string namevalue;

        public static event EventHandler SomeEvent;
    }
}
