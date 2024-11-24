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
        private Checkers checkers = new Checkers();
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

        public void DrawEditView(int width, string Heading, List<string> array, int selectedX, int selectedY, int Offset, string highLightedText)
        {
            IMessageBox.DefaultColor();
            Console.SetCursorPosition(0, 1);
            Console.WriteLine($"┌{new UltraFormatter().DoublePadding(Heading, width - 2, '─')}┐");

            for (int i = 0; i < Settings.WindowDataLimit; i++)
            {
                if (Offset + i >= array.Count)
                {
                    IMessageBox.DefaultColor();
                    Console.SetCursorPosition(0, i + 2);
                    Console.WriteLine($"│{new string(' ', width - 2)}│");
                    continue;
                }

                int actualIndex = Offset + i;
                string currentLine = array[actualIndex];
                bool isSelected = (actualIndex == selectedY);

                Console.SetCursorPosition(0, i + 2);

                if (isSelected)
                {
                    Console.Write($"│{(actualIndex + 1).ToString().PadRight(4)} ");
                    for (int j = 0; j < currentLine.Length; j++)
                    {
                        if (j == selectedX)
                        {
                            IMessageBox.SelectionColor();
                            //IMessageBox.DefaultColor();
                            Console.Write(currentLine[j]);
                        }
                        else
                        {
                            IMessageBox.DefaultColor();
                            Console.Write(currentLine[j]);
                        }
                    }
                }
                else
                {
                    Console.Write($"│");

                    if (checkers.StringContains(currentLine, highLightedText))
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        IMessageBox.DefaultColor();
                    }
                    Console.Write($"{(actualIndex + 1).ToString().PadRight(4)} ");
                    Console.Write(currentLine);
                    try
                    {
                        Console.WriteLine($"{new string(' ', width - currentLine.Length - 7)}│");

                    }
                    catch
                    {

                    }
                }
                IMessageBox.DefaultColor();
            }

            IMessageBox.DefaultColor();
            Console.SetCursorPosition(0, Settings.WindowDataLimit + 2);
            Console.WriteLine($"└{new string('─', width - 2)}┘");
        }

    }

}