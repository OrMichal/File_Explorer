﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.Core;
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
            Settings.MBoxColor();
            Console.WriteLine($"┌{formatter.DoublePadding(Heading,Width - 2, '─')}┐");

            for(i = 1; i <= Height - 2; i++)
            {
                Console.SetCursorPosition(LocationX, i + LocationY);
                Console.WriteLine($"│{new string(' ', Width - 2)}│");
            }
            Console.SetCursorPosition(LocationX, i + LocationY);
            Settings.MBoxColor();
            Console.WriteLine($"└{new string("").PadRight(Width - 2, '─')}┘");
        }

        public void DrawListBox(int Width, int Height, int LocationX, int LocationY, IEnumerable<dynamic> Content = null, int selected = 0)
        {
            int i = 0;

            Console.SetCursorPosition(LocationX, i + LocationY);
            Settings.MBoxColor();
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
                    Settings.MBoxColor();
                    Console.WriteLine("│");
                }
                else
                {
                    Console.SetCursorPosition(LocationX, LocationY + i);
                    Settings.MBoxColor();
                    Console.WriteLine($"│{formatter.PadTrimRight(s, Width - 2)}│");
                }
                i++;
            }
            Console.SetCursorPosition(LocationX, i + LocationY);
            Settings.MBoxColor();
            Console.WriteLine($"└{new string("").PadRight(Width - 2, '─')}┘");
            
        }

        public void DrawTextBox(int width, int LocationX, int LocationY, string Content = "", int SelectedIndex = 0, int offset = 0)
        {
            int i = 0;

            Console.SetCursorPosition(LocationX, i + LocationY);
            Settings.MBoxColor();
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
                            Settings.MBoxColor();
                            Console.Write(Content[index + offset]);
                        }
                    }
                    else
                    {
                        Settings.MBoxColor();
                        Console.Write(" ");
                    }
                }
                Settings.MBoxColor();
                Console.WriteLine($"│");
            }

            Console.SetCursorPosition(LocationX, i + LocationY );
            Settings.MBoxColor();
            Console.WriteLine($"└{new string("").PadRight(width - 2, '─')}┘");
        }

        public void DrawTextBox(int width, int LocationX, int LocationY, List<string> Content = null, int SelectedIndex = 0, int offset = 0)
        {
            int i = 0;

            Console.SetCursorPosition(LocationX, i + LocationY);
            Settings.MBoxColor();
            Console.WriteLine($"┌{new string('─', width - 2)}┐");
            for (i = 1; i < Content.Count(); i++)
            {
                Console.SetCursorPosition(LocationX, i + LocationY);
                Console.WriteLine($"│{Content[i - 1].PadRight(width - 2)}│");
            }

            Console.SetCursorPosition(LocationX, i + LocationY);
            Settings.MBoxColor();
            Console.WriteLine($"└{new string("").PadRight(width - 2, '─')}┘");
        }

        public void DrawButtonsVertical(int LocationX, int LocationY, List<Button> buttons, int selectedButton = 0, int padding = 2)
        {
            int currentY = LocationY;

            foreach (var button in buttons)
            {
                string label = button.Label;
                int buttonWidth = buttons.Select(x => x.Label.Length).Max() + 2;

                Console.SetCursorPosition(LocationX, currentY);
                Settings.MBoxColor();
                Console.Write($"┌{new string('─', buttonWidth)}┐");

                Console.SetCursorPosition(LocationX, currentY + 1);
                Settings.MBoxColor();
                Console.Write("│");
                if (buttons.IndexOf(button) == selectedButton)
                {
                    IMessageBox.SelectionColor();
                }
                Console.Write(label.PadRight(buttonWidth));
                Settings.MBoxColor();
                Console.Write("│");

                Console.SetCursorPosition(LocationX, currentY + 2);
                Settings.MBoxColor();
                Console.Write($"└{new string('─', buttonWidth)}┘");

                currentY += 3 + padding;
            }
        }

        public void DrawButtons(int LocationX, int LocationY, List<Button> buttons, int selectedButton = 0, int padding = 2)
        {
            int currentX = LocationX;

            foreach (var button in buttons)
            {
                string label = button.Label;
                int buttonWidth = label.Length + 2;

                Console.SetCursorPosition(currentX, LocationY);
                Settings.MBoxColor();
                Console.Write($"┌{new string('─', label.Length)}┐");

                Console.SetCursorPosition(currentX, LocationY + 1);
                Settings.MBoxColor();
                Console.Write("│");
                if (buttons.IndexOf(button) == selectedButton)
                {
                    IMessageBox.SelectionColor();
                }
                Console.Write(label);
                Settings.MBoxColor();
                Console.Write("│");

                Console.SetCursorPosition(currentX, LocationY + 2);
                Settings.MBoxColor();
                Console.Write($"└{new string('─', label.Length)}┘");

                currentX += buttonWidth + padding;
            }
        }


        public void DrawLabel(int LocationX, int LocationY, string Content = "", int marginLeft = 0)
        {
            Console.SetCursorPosition(LocationX + marginLeft, LocationY);
            Console.Write(Content);
        }

        public void DrawEditView(int width, string Heading, List<string> array, int selectedX, int selectedY, int Offset, List<Point> selectLoacations)
        {
            try
            {

                Settings.MBoxColor();
                Console.SetCursorPosition(0, 2);
                Console.WriteLine(new string(' ', width));
                Console.SetCursorPosition(0, 1);
                Console.WriteLine($"┌{new UltraFormatter().DoublePadding(Heading, width - 2, '─')}┐");

                for (int i = 0; i < Settings.WindowDataLimit + 2; i++)
                {
                    if (Offset + i >= array.Count)
                    {
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
                        Console.Write($"│ ");
                        for (int j = 0; j < currentLine.Length; j++)
                        {
                            Point currP = new Point(j,i);

                            if (j == selectedX)
                            {
                                IMessageBox.SelectionColor();
                                Console.Write(currentLine[j]);
                            }
                            else if(selectLoacations.Contains(currP))
                            {
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write(currentLine[j]);
                                Window.DefaultColor();
                            }
                            else
                            {
                                Settings.MBoxColor();
                                Console.Write(currentLine[j]);
                            }
                        }
                    }
                    else
                    {
                        Console.Write($"│ ");
                        StringBuilder outputLine = new StringBuilder();

                        for (int j = 0; j < currentLine.Length; j++)
                        {
                            Point currentPoint = new Point(j, i + Offset);
                            if (selectLoacations.Contains(currentPoint))
                            {
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write(currentLine[j]);
                                Settings.MBoxColor();
                            }
                            else
                            {
                                Console.Write(currentLine[j]);
                            }
                        }

                        if (outputLine.Length > 0)
                        {
                            Settings.MBoxColor();
                            Console.Write(outputLine.ToString());
                        }
                        Console.WriteLine(new string(' ', width - currentLine.Length - 3) + "│");

                    }
                    Settings.MBoxColor();
                }

                Settings.MBoxColor();
                Console.SetCursorPosition(0, Settings.WindowDataLimit + 4);
                Console.WriteLine($"└{new string('─', width - 2)}┘");

            }
            catch (Exception)
            {

            }
        }

        public void DrawView(int width, string Heading, List<string> array, int Offset)
        {
            
            Settings.MBoxColor();
            int i = 0;
            Console.SetCursorPosition(0, 1);
            Console.WriteLine($"┌{formatter.DoublePadding(Heading, width - 2, '─')}┐");
            for (i = 0; i < Settings.WindowDataLimit; i++)
            {
                int actualIndex = 0;
                if (i < array.Count())
                {

                    actualIndex = Offset + i;
                    Console.SetCursorPosition(0, i + 2);
                    Console.WriteLine($"│{(actualIndex + 1).ToString().PadRight(4)} {formatter.PadTrimRight(array[actualIndex], width - 7)}│");
                }
                else
                {
                    Console.SetCursorPosition(0, i + 2);
                    Console.WriteLine($"│{new string(' ', width - 2)}│");
                }
            }
            Console.SetCursorPosition(0, i + 2);
            Console.WriteLine($"└{new string('─', width - 2)}┘");
        }

        public void DrawCheckBoxes(int LocationX, int LocationY, List<CheckBox> checkBoxes, int selectedBox, int margin = 2)
        {
            for (int i = 0; i < checkBoxes.Count; i++)
            {
                Console.SetCursorPosition(LocationX + margin, LocationY + ((i + 1) * margin));
                Console.Write("[");
                if(selectedBox == i)
                {
                    IMessageBox.SelectionColor();
                    Console.Write($"{checkBoxes[i].activeChoice}");
                    Settings.MBoxColor();
                }
                else
                {
                    Settings.MBoxColor();
                    Console.Write($"{checkBoxes[i].activeChoice}");
                }
                Console.Write($"] {checkBoxes[i].Text}");
            }
        }

        
    }

}