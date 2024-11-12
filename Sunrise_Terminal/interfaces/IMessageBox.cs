using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal
{
    public interface IMessageBox
    {
        public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public static void DefaultColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
        }

        public static void SelectionColor()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.Gray;
        }

    }
}
