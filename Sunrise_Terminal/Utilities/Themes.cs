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
            Settings.LWindowselectionColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.BackgroundColor = ConsoleColor.White;
            };

            Settings.MBoxColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
            };

            return Settings.ListWindowColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Magenta;
            };
        }

        public static Action Default()
        {
            Settings.LWindowselectionColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Blue;
            };

            Settings.MBoxColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Blue;
            };

            return Settings.ListWindowColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkCyan;
            };
        }

        public static Action Metrix()
        {
            Settings.LWindowselectionColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            };

            Settings.MBoxColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            };

            return Settings.ListWindowColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.Black;
            };
        }

        public static Action SolarizedDark()
        {
            Settings.LWindowselectionColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            };

            Settings.MBoxColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            };

            return Settings.ListWindowColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
            };
        }

        public static Action Sunrise()
        {
            Settings.LWindowselectionColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            };

            Settings.MBoxColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.Red;
            };

            return Settings.ListWindowColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Black;
            };
        }

        public static Action Communism()
        {
            Settings.LWindowselectionColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.Red;
            };

            Settings.MBoxColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.DarkRed;
            };

            return Settings.ListWindowColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.DarkRed;
            };
        }


        public static Action Cyberpunk()
        {
            Settings.LWindowselectionColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Cyan;
            };

            Settings.MBoxColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            };

            return Settings.ListWindowColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
            };
        }

        public static Action Autumn()
        {
            Settings.MBoxColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.BackgroundColor = ConsoleColor.DarkRed;
            };

            Settings.LWindowselectionColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
            };

            return Settings.ListWindowColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Black;
            };
        }

        public static Action OceanBreeze()
        {
            Settings.MBoxColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            };

            Settings.LWindowselectionColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.BackgroundColor = ConsoleColor.Black;
            };

            return Settings.ListWindowColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.BackgroundColor = ConsoleColor.DarkCyan;
            };
        }

        public static Action NeonGlow()
        {
            Settings.LWindowselectionColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.DarkCyan;
            };

            Settings.MBoxColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.BackgroundColor = ConsoleColor.Magenta;
            };

            return Settings.ListWindowColor = () =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
            };
        }
    }
}
