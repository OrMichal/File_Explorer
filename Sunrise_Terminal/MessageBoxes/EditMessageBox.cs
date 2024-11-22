using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.Core;
using Sunrise_Terminal.DataHandlers;
using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.objects;
using Sunrise_Terminal.Utilities;

namespace Sunrise_Terminal.MessageBoxes
{
    public class EditMessageBox : Window, IMessageBox, IHasCursor<string>
    {
        public delegate void RenewFile();
        public RenewFile RenewFileDelegate;

        new public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public int Offset { get; set; }
        public string path { get; set; }
        public string itemToPreview { get; set; }
        public List<string> Rows { get; set; } = new List<string>();
        public List<char> selectedChars = new List<char>();
        public int Limit { get; set; }


        public StreamReader SReader;
        public StreamWriter SWriter;
        private bool insertion = false;
        private DataManagement dataManager = new DataManagement();
        public Cursor<string> cursor { get; set; } = new Cursor<string>();
        private string selectedRowText
        {
            get
            {
                return this.Rows[cursor.Y] ?? (0).ToString();
            }

        }

        public EditMessageBox(int Width, int Height, API api)
        {
            cursor.Movement.Data = this.Rows;
            this.width = Width;
            this.height = Height;
            Heading = "Editation";
            try
            {
                using (StreamReader sr = new StreamReader(Path.Combine(api.GetActiveListWindow().ActivePath, api.GetSelectedFile())))
                {
                    while (!sr.EndOfStream)
                    {
                        Rows.Add(sr.ReadLine());
                    }

                    Rows.Add(" ");
                }

            }
            catch (FileNotFoundException)
            {
                return;
            }
            catch (UnauthorizedAccessException)
            {
                return;
            }
            

            if(Rows.Count == 0)
            {
                Rows.Add(" ");
            }

            for (int i = 0; i < Rows.Count; i++)
            {
                if(Rows[i].Length == 0)
                {
                    Rows[i] = " ";
                }
            }
        }

        public override void Draw(int LocationX, API api, bool _ = true)
        {
            if (!(File.Exists(Path.Combine(api.GetActiveListWindow().ActivePath, api.GetSelectedFile()))))
            {
                api.Application.SwitchWindow(new InfoMessageBox(30, 7, "This is not a File"));
                return;
            }

            IMessageBox.DefaultColor();
            Console.SetCursorPosition(0, 1);
            Console.WriteLine($"┌{new Formatter().DoublePadding(Heading, width - 2, '─')}┐");
            int i = 0;
            for (i = 0; i < Settings.WindowDataLimit; i++)
            {
                int actualIndex = 0;
                if (i < Rows.Count())
                {
                    actualIndex = cursor.Offset + i;
                    if(actualIndex == cursor.Y)
                    {
                        Console.SetCursorPosition(0, i + 2);
                        Console.Write($"|{(actualIndex + 1).ToString().PadRight(4)} ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        int a = 0;
                        foreach(char c in Rows[actualIndex])
                        {
                            if(a == cursor.X)
                            {
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write($"{Rows[actualIndex][a]}");
                                IMessageBox.DefaultColor();
                            }
                            else
                            {
                                IMessageBox.DefaultColor();
                                Console.Write($"{Rows[actualIndex][a]}");
                            }
                            a++;
                        }
                        Console.WriteLine($"{ new string(" ").PadRight(width - Rows[actualIndex].Length - 7)}│");
                    }
                    else
                    {

                        IMessageBox.DefaultColor();
                        Console.SetCursorPosition(0, i + 2);
                        Console.WriteLine($"│{new string($"{new string((actualIndex + 1).ToString()).PadRight(4)} {new Formatter().PadTrimRight(Rows[actualIndex],width - 7)}").PadRight(width - 8)}│");
                    }
                }
                else
                {
                    IMessageBox.DefaultColor();
                    Console.SetCursorPosition (0, i + 2);
                    Console.WriteLine($"│{new string(' ', width - 2)}│");
                }
            }
            IMessageBox.DefaultColor();
            Console.SetCursorPosition(0, i +2);
            Console.WriteLine($"└{new string('─', width - 2)}┘");
        }

        

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if (!(File.Exists(Path.Combine(api.GetActiveListWindow().ActivePath, api.GetSelectedFile()))))
            {
                return;
            }

            HandleMBoxChange(info, api);

            if (info.Key == ConsoleKey.DownArrow)
            {
                cursor.MoveDown();
                
            }
            else if (info.Key == ConsoleKey.UpArrow)
            {
                cursor.MoveUp();
            }
            else if(info.Key == ConsoleKey.RightArrow)
            {
                cursor.MoveRight();
            }
            else if(info.Key == ConsoleKey.LeftArrow)
            {
                cursor.MoveLeft();   
            }
            else if(char.IsLetterOrDigit(info.KeyChar) || char.IsWhiteSpace(info.KeyChar) && info.Key != ConsoleKey.Enter)
            {
                if (insertion)
                {
                    Rows[cursor.Y] = dataManager.AddCharToText_Insert(cursor.X, selectedRowText, info);
                    if(cursor.X < this.selectedRowText.Count() - 1)
                    {
                        cursor.X++;
                    }
                }
                else if (!insertion)
                {
                    Rows[cursor.Y] = dataManager.AddCharToText(selectedRowText, info);
                    cursor.X = this.Rows[this.cursor.Y].Count() - 1;

                }
            }
            else if(info.Key == ConsoleKey.Backspace)
            {

                if(cursor.X == 0)
                {
                    if(!(Rows.Count > 1))
                    {
                        return;
                    }

                    Rows[cursor.Y] = dataManager.RemoveChar(selectedRowText,cursor.X);
                    Rows.RemoveAt(cursor.Y);
                    if(cursor.Y > 0)
                    {
                        cursor.Y--;
                    }
                    cursor.X = 0;
                }
                else
                {
                    if(cursor.X == Rows[cursor.Y].Length - 1)
                    {
                        cursor.X--;
                    }

                    Rows[cursor.Y] = dataManager.RemoveChar(selectedRowText, cursor.X);
                }
            }
            else if(info.Key == ConsoleKey.Enter)
            {
                Rows.Insert(cursor.Y + 1, " ");
                cursor.Y++;
                cursor.X = 0;
                if(cursor.Y >= cursor.Offset + Settings.WindowDataLimit - 1)
                {
                    cursor.Offset++;
                }
            }
            else if(info.Key == ConsoleKey.F5)
            {
                dataManager.SaveChanges(this.path, this.itemToPreview, this.Rows);
            }
            else if( info.Key == ConsoleKey.Escape)
            {
                api.CloseActiveWindow();
            }
            else if(info.Key == ConsoleKey.Insert)
            {
                insertion = !insertion;
            }
        }

    }
}
