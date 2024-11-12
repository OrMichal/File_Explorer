using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.objects
{
    public class Graphics
    {
        public void DrawSquare(int Width, int Height, int LocationX, int LocationY, string Heading = "")
        {
            int i = 0;

            Console.SetCursorPosition(LocationX, i + LocationY);
            IMessageBox.DefaultColor();
            Console.WriteLine($"┌{new Formatter().DoublePadding(Heading,Width - 2, '─')}┐");

            for(i = 1; i <= Height - 2; i++)
            {
                Console.SetCursorPosition(LocationX, i + LocationY);
                Console.WriteLine($"│{new string(' ', Width - 2)}│");
            }
            Console.SetCursorPosition(LocationX, i + LocationY);
            IMessageBox.DefaultColor();
            Console.WriteLine($"└{new string("").PadRight(Width - 2, '─')}┘");
        }

        public void DrawListBox(int Width, int Height, int LocationX, int LocationY, List<string> Content = null, int selected = 0)
        {
            int i = 0;

            Console.SetCursorPosition(LocationX, i + LocationY);
            IMessageBox.DefaultColor();
            Console.WriteLine($"┌{new string('─', Width - 2)}┐");
            i = 1;
            foreach(string s in Content)
            {
                if(i - 1 == selected)
                {
                    Console.SetCursorPosition(LocationX, LocationY + i);
                    Console.Write("│");
                    IMessageBox.SelectionColor();
                    Console.Write($"{new Formatter().PadTrimRight(s, Width - 2)}");
                    IMessageBox.DefaultColor();
                    Console.WriteLine("│");
                }
                else
                {
                    Console.SetCursorPosition(LocationX, LocationY + i);
                    IMessageBox.DefaultColor();
                    Console.WriteLine($"│{new Formatter().PadTrimRight(s, Width - 2)}│");
                }
                i++;
            }
            Console.SetCursorPosition(LocationX, i + LocationY);
            IMessageBox.DefaultColor();
            Console.WriteLine($"└{new string("").PadRight(Width - 2, '─')}┘");
            
        }

        public void DrawTextBox(int width, int LocationX, int LocationY, string Content = "", int SelectedIndex = 0, int offset = 0)
        {
            int i = 0;

            Console.SetCursorPosition(LocationX, i + LocationY);
            IMessageBox.DefaultColor();
            Console.WriteLine($"┌{new string('─', width - 2)}┐");
            for(i = 1; i < 2; i++)
            {
                Console.SetCursorPosition(LocationX, i + LocationY);
                Console.Write($"│");
                for (int index = 0; index < width - 2; index++)
                {
                    if(index < Content.Length)
                    {
                        if(index + offset == SelectedIndex)
                        {
                            IMessageBox.SelectionColor();
                            Console.Write(Content[index + offset]);
                        }
                        else
                        {
                            IMessageBox.DefaultColor();
                            Console.Write(Content[index + offset]);
                        }
                    }
                    else
                    {
                        IMessageBox.DefaultColor();
                        Console.Write(" ");
                    }
                }
                IMessageBox.DefaultColor();
                Console.WriteLine($"│");
            }

            Console.SetCursorPosition(LocationX, i + LocationY );
            IMessageBox.DefaultColor();
            Console.WriteLine($"└{new string("").PadRight(width - 2, '─')}┘");
        }

        public void DrawTextBox(int width, int LocationX, int LocationY, List<string> Content = null, int SelectedIndex = 0, int offset = 0)
        {
            int i = 0;

            Console.SetCursorPosition(LocationX, i + LocationY);
            IMessageBox.DefaultColor();
            Console.WriteLine($"┌{new string('─', width - 2)}┐");
            for (i = 1; i < Content.Count; i++)
            {
                Console.SetCursorPosition(LocationX, i + LocationY);
                Console.WriteLine($"│{Content[i - 1].PadRight(width - 2)}│");
            }

            Console.SetCursorPosition(LocationX, i + LocationY);
            IMessageBox.DefaultColor();
            Console.WriteLine($"└{new string("").PadRight(width - 2, '─')}┘");
        }

        public void DrawButtons(int ButtonWidth, int LocationX, int LocationY, List<Button> buttons, int selectedButton = 0)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                int buttonX = LocationX + (ButtonWidth * i);
                int buttonY = LocationY;

                Console.SetCursorPosition(buttonX, buttonY);
                IMessageBox.DefaultColor();
                Console.Write($"┌{new string('─', ButtonWidth - 2)}┐");

                Console.SetCursorPosition(buttonX, buttonY + 1);
                IMessageBox.DefaultColor();
                Console.Write("│");
                if (i == selectedButton)
                {
                    IMessageBox.SelectionColor();
                    Console.Write(buttons[i].Label.PadRight(ButtonWidth - 2));
                }
                else
                {
                    IMessageBox.DefaultColor();
                    Console.Write(buttons[i].Label.PadRight(ButtonWidth - 2));
                }
                IMessageBox.DefaultColor();
                Console.Write("│");

                // Vykreslení dolního okraje tlačítka
                Console.SetCursorPosition(buttonX, buttonY + 2);
                IMessageBox.DefaultColor();
                Console.Write($"└{new string('─', ButtonWidth - 2)}┘");
            }
        }

        public void DrawLabel(int LocationX, int LocationY, string Content = "", int marginLeft = 0)
        {
            Console.SetCursorPosition(LocationX + marginLeft, LocationY);
            Console.Write(Content);
        }

    }
}
