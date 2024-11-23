using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.Utilities
{
    public class ConsoleFormatter : Window
    {
        public void FormatCheck()
        {
            int width2 = Console.WindowWidth / Settings.NumberOfWindows;
            if (width2 != Settings.WindowWidth)
            {
                Console.Clear();
            }

            int limit = Console.WindowHeight - 7;
            if (limit != Settings.WindowDataLimit)
            {
                Console.Clear();
            }

            Settings.WindowDataLimit = limit;
            Settings.WindowWidth = width2;
            if (Settings.WindowWidth * 2 < 110)
            {
                try
                {
                    Console.SetWindowSize(110, Console.WindowHeight);
                }
                catch (IOException)
                {

                }
            }

            if (Console.WindowHeight < 30)
            {
                Console.SetWindowSize(Console.WindowWidth, 30);
            }
        }
    }
}
