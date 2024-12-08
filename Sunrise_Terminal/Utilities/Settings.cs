using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.Utilities
{
    public class Settings
    {
        public static int NumberOfWindows { get; set; } = 1;
        public static int WindowWidth { get; set; } = Console.WindowWidth / NumberOfWindows;
        public static int WindowDataLimit { get; set; } = Console.WindowHeight - 8;

        public static int SmallMessageBoxWidth { get; set; } = 22;
        public static int SmallMessageBoxHeight { get; set; } = 7;

        public static int MediumMessageBoxHeight { get; set; } = 12;
        public static int MediumMessageBoxWidth { get; set; } = 40;

        public static int BigMessageBoxHeight { get; set; } = 20;
        public static int BigMessageBoxWidth { get; set; } = 60;

        public static Action ListWindowColor = () =>
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkCyan;
        };

        public static Action LWindowselectionColor = () =>
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Blue;
        };

        public static Action MBoxColor = () =>
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
        };
    }
}
