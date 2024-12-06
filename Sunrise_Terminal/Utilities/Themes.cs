using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.Utilities
{
    public class Themes
    {
        public static Action HelloKitty()
        {
            return Settings.ActiveColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Magenta;
            };
        }

        public static Action Default()
        {
            return Settings.ActiveColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkCyan;
            };
        }

        public static Action Metrix()
        {
            return Settings.ActiveColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.Black;
            };
        }

        public static Action SolarizedDark()
        {
            return Settings.ActiveColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
            };
        }

        public static Action Sunrise()
        {
            return Settings.ActiveColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Black;
            };
        }

        public static Action Communism()
        {
            return Settings.ActiveColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.DarkRed;
            };
        }
    }
}
