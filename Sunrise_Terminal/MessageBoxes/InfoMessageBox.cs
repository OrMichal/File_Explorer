using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal
{
    public class InfoMessageBox : Window, IMessageBox
    {
        new public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }

        public InfoMessageBox(int Width, int Height)
        {
            this.width = Width;
            this.height = Height;
        }

        public virtual void Draw(int LocationX, API api, bool _ = true)
        {

            int i = 0;
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, i + Console.WindowHeight / 2);
            IMessageBox.DefaultColor();
            Console.Write($"┌─{Heading.PadRight(width + 2, '─')}─");
            Console.WriteLine("┐");

            for (i = 1; i < height - 1; i++)
            {
                if (i < height / 2)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, i + Console.WindowHeight / 2);
                    IMessageBox.DefaultColor();
                    Console.Write($"│ {new string("".PadRight(width + 2))} ");
                    Console.WriteLine("│");
                }

                if (i == (height / 2))
                {
                    IMessageBox.DefaultColor();
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, i + Console.WindowHeight / 2);
                    Console.WriteLine($"│  {Description.PadRight(width)}  │");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, i + Console.WindowHeight / 2 + 1);

                    IMessageBox.DefaultColor();
                    Console.Write($"│ ┌{new string("".PadRight(width / 3, '─'))}┐ ");
                    Console.Write($"┌{new string("".PadRight(width / 3, '─'))}┐ ");
                    Console.WriteLine($"{new string("").PadRight(width / 4)}│");

                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, i + Console.WindowHeight / 2 + 2);
                    IMessageBox.DefaultColor();

                        Console.Write($"{new string(' ', width / 3)}│ │");
                        Console.Write($"{new string("Ok").PadRight(width / 3)}");
                        IMessageBox.DefaultColor();
                        Console.WriteLine($"│ {new string("").PadRight(width / 4)}│");

                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, i + Console.WindowHeight / 2 + 3);
                    IMessageBox.DefaultColor();
                    Console.Write($"│ └{new string("".PadRight(width / 3, '─'))}┘ └{new string("".PadRight(width / 3, '─'))}┘ ");
                    Console.WriteLine($"{new string("").PadRight(width / 4)}│");
                }
                else if (i > height / 2)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, i + Console.WindowHeight / 2 + 3);
                    IMessageBox.DefaultColor();
                    Console.Write($"│ {new string("".PadRight(width + 2))} ");
                    Console.WriteLine("│");
                }
            }
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, i + Console.WindowHeight / 2 + 3);
            IMessageBox.DefaultColor();
            Console.Write($"└─{new string("").PadRight(width + 2, '─')}─");
            Console.WriteLine("┘");

        }

        public virtual void HandleKey(ConsoleKeyInfo info, API api)
        {
            if(info.Key == ConsoleKey.Enter)
            {
                api.Application.SwitchWindow(api.Application.ListWindows[0]);
            }
            else if(info.Key == ConsoleKey.Escape)
            {
                api.Application.SwitchWindow(api.Application.ListWindows[0]);
            }
        }
    }
}
