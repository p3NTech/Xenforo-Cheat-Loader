using System;
using System.Text;

namespace cheatname
{
    internal class base64
    {
        public static string decode(string coded)
        {
            var bytes = Convert.FromBase64String(coded);
            var text = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            return text;
        }

        public static string encode(string text)
        {
            byte[] byteslog = Encoding.UTF8.GetBytes(text);
            string base64log = Convert.ToBase64String(byteslog);
            return base64log;
        }
    }
}
