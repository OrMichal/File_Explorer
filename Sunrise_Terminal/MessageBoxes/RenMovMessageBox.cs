using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal
{
    public class RenMovMessageBox : Window, IMessageBox
    {
        new public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public bool confirmation { get; set; } = false;
        public string srcPath { get; set; }
        public string destPath { get; set; }

        public RenMovMessageBox(int Height, int Width)
        {
            Heading = "Moving MessageBox";
            Description = "Are you sure?";
            this.height = Height;
            this.width = Width;
        }

        public override void Draw(int LocationX, API api, bool _ = true)
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
                    Console.Write($"│ │");

                    if (confirmation)
                    {
                        IMessageBox.SelectionColor();
                        Console.Write($"{new string("Yes").PadRight(width / 3)}");
                        IMessageBox.DefaultColor();
                        Console.Write($"│ │");
                        Console.Write($"{new string("No").PadRight(width / 3)}");
                        IMessageBox.DefaultColor();
                        Console.WriteLine($"│ {new string("").PadRight(width / 4)}│");
                    }
                    else
                    {
                        Console.Write($"{new string("Yes").PadRight(width / 3)}");
                        Console.Write($"│ │");
                        IMessageBox.SelectionColor();
                        Console.Write($"{new string("No").PadRight(width / 3)}");
                        IMessageBox.DefaultColor();
                        Console.WriteLine($"│ {new string("").PadRight(width / 4)}│");
                    }


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

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if(info.Key == ConsoleKey.Escape)
            {
                api.Application.SwitchWindow(api.Application.ListWindows[0]);
            }
            else if(info.Key == ConsoleKey.LeftArrow)
            {
                confirmation = true;    
            }
            else if(info.Key == ConsoleKey.RightArrow)
            {
                confirmation = false;
            }
            else if(info.Key == ConsoleKey.Enter)
            {
                if (confirmation)
                {
                    Move(srcPath, destPath);
                }
                api.Application.SwitchWindow(api.Application.ListWindows[0]);
            }
        }

        private void Move(string SourceFilePath, string DestinationFilePath)
        {
            try
            {
                if (File.Exists(SourceFilePath))
                {
                    if (File.Exists(DestinationFilePath))
                    {
                        File.Delete(DestinationFilePath);
                    }
                    File.Move(SourceFilePath, DestinationFilePath);
                }
                else if (Directory.Exists(SourceFilePath))
                {
                    if (Directory.Exists(DestinationFilePath))
                    {
                        Directory.Delete(DestinationFilePath, true);
                    }
                    Directory.Move(SourceFilePath, DestinationFilePath);
                }
            }
            catch (UnauthorizedAccessException)
            {
                throw new Exception("nn, nemáš přístup bráško");
            }
        }


    }
}
