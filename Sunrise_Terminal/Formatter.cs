using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal
{
    public class Formatter
    {
        public void ConsoleFormatCheck()
        {
            if (!ConsoleSizeChanged())
            {
                return;
            }

            int width2 = Console.WindowWidth / 2;
            if (width2 != Settings.WindowWidth)
            {
                Console.Clear();
                Console.CursorVisible = false;
            }

            int limit = Console.WindowHeight - 7;
            if (limit != Settings.WindowDataLimit)
            {
                Console.Clear();
                Console.CursorVisible = false;
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

        public bool ConsoleSizeChanged()
        {
            int width2 = Console.WindowWidth / 2;
            if (width2 != Settings.WindowWidth)
            {
                Console.CursorVisible = false;
                return true;
            }

            int limit = Console.WindowHeight - 7;
            if (limit != Settings.WindowDataLimit)
            {
                Console.CursorVisible = false;
                return true;
            }

            return false;
        }

        public string DoublePadding(string text, int length, char c = ' ')
        {
            if (text == null) text = "";

            if (length <= text.Length)
            {
                return text.Substring(0, length);
            }

            if (text.Length % 2 != 0) text += " ";

            int totalPadding = length - text.Length;
            int paddingSide = totalPadding / 2;

            return $"{new string(c, paddingSide)}{text}{new string(c, totalPadding - paddingSide)}";
        }


        public string PadTrimRight(string text, int Length)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (char c in text)
            {
                if (i <= Length - 2)
                {
                    sb.Append(c);
                }
                else if (i == Length - 1)
                {
                    sb.Append('~');
                }
                i++;
            }
            return sb.ToString().PadRight(Length);
        }

        public string PadTrimLeft(string text, int Length)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (char c in text)
            {
                if (i <= Length - 2)
                {
                    sb.Append(c);
                }
                else if (i == Length - 1)
                {
                    sb.Append('~');
                }
                i++;
            }
            return sb.ToString().PadLeft(Length);
        }
    }
}
