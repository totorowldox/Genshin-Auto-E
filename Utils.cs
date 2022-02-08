using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinAutoE
{
    public static class Utils
    {
        public static void DownKey(byte key)
        {
            Native.keybd_event(key, 0, 0, 0);
        }
        public static void UpKey(byte key)
        {
            Native.keybd_event(key, 0, 2, 0);
        }
    }
}
