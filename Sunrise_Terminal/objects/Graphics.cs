using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.Utilities;

namespace Sunrise_Terminal.objects
{
    public class Graphics
    {
        private UltraFormatter formatter = new UltraFormatter();
        public void DrawSquare(int Width, int Height, int LocationX, int LocationY, string Heading = "")
        {
            int i = 0;

            Console.SetCursorPosition(LocationX, i + LocationY);
            IMessageBox.DefaultColor();
            Console.WriteLine($"┌{formatter.DoublePadding(Heading,Width - 2, '─')}┐");

            for(i = 1; i <= Height - 2; i++)
            {
                Console.SetCursorPosition(LocationX, i + LocationY);
                Console.WriteLine($"│{new string(' ', Width - 2)}│");
            }
            Console.SetCursorPosition(LocationX, i + LocationY);
            IMessageBox.DefaultColor();
            Console.WriteLine($"└{new string("").PadRight(Width - 2, '─')}┘");
        }

        public void DrawListBox(int Width, int Height, int LocationX, int LocationY, IEnumerable<dynamic> Content = null, int selected = 0)
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
                    Console.Write($"{formatter.PadTrimRight(s, Width - 2)}");
                    IMessageBox.DefaultColor();
                    Console.WriteLine("│");
                }
                else
                {
                    Console.SetCursorPosition(LocationX, LocationY + i);
                    IMessageBox.DefaultColor();
                    Console.WriteLine($"│{formatter.PadTrimRight(s, Width - 2)}│");
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
            for (i = 1; i < Content.Count(); i++)
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

        public void DrawEditView(int width, string Heading, List<string> array, int selectedX, int selectedY, int Offset)
        {
            IMessageBox.DefaultColor();
            Console.SetCursorPosition(0, 1);
            Console.WriteLine($"┌{new UltraFormatter().DoublePadding(Heading, width - 2, '─')}┐");
            int i = 0;
            for (i = 0; i < Settings.WindowDataLimit; i++)
            {
                int actualIndex = 0;
                if (i < array.Count())
                {
                    actualIndex = Offset + i;
                    if (actualIndex == selectedY)
                    {
                        Console.SetCursorPosition(0, i + 2);
                        Console.Write($"|{(actualIndex + 1).ToString().PadRight(4)} ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        int a = 0;
                        foreach (char c in array[actualIndex])
                        {
                            if (a == selectedX)
                            {
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write($"{array[actualIndex][a]}");
                                IMessageBox.DefaultColor();
                            }
                            else
                            {
                                IMessageBox.DefaultColor();
                                Console.Write($"{array[actualIndex][a]}");
                            }
                            a++;
                        }
                        Console.WriteLine($"{new string(" ").PadRight(width - array[actualIndex].Length - 7)}│");
                    }
                    else
                    {

                        IMessageBox.DefaultColor();
                        Console.SetCursorPosition(0, i + 2);
                        Console.WriteLine($"│{new string($"{new string((actualIndex + 1).ToString()).PadRight(4)} {new UltraFormatter().PadTrimRight(array[actualIndex], width - 7)}").PadRight(width - 8)}│");
                    }
                }
                else
                {
                    IMessageBox.DefaultColor();
                    Console.SetCursorPosition(0, i + 2);
                    Console.WriteLine($"│{new string(' ', width - 2)}│");
                }
            }
            IMessageBox.DefaultColor();
            Console.SetCursorPosition(0, i + 2);
            Console.WriteLine($"└{new string('─', width - 2)}┘");
        }
    }

}