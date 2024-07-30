using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinApp.Utilities
{
    public static class WindowStateManager
    {
        public static double Width { get; set; } = 1080;
        public static double Height { get; set; } = 720;
        public static double Top { get; set; } = 100;
        public static double Left { get; set; } = 100;
        public static bool IsMaximized { get; set; } = false;
    }
}
