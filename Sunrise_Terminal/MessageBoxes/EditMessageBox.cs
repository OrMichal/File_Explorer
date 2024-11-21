using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.MessageBoxes
{
    public class EditMessageBox : Window, IMessageBox
    {
        public delegate void RenewFile();
        public RenewFile RenewFileDelegate;

        new public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        private int offset { get; set; }
        private int selectedRow = 0;
        private int selectedChar = 0;
        public string path { get; set; }
        public string itemToPreview { get; set; }
        public List<string> DataParted { get; set; } = new List<string>();
        public int Limit { get; set; }
        public StreamReader SReader;
        public StreamWriter SWriter;
        private bool insertion = false;
        private DataManager dataManager = new DataManager();

        public EditMessageBox(int Width, int Height, API api)
        {
            this.width = Width;
            this.height = Height;
            Heading = "Editation";

            using (StreamReader sr = new StreamReader(Path.Combine(api.GetActiveListWindow().ActivePath, api.GetSelectedFile())))
            {
                while (!sr.EndOfStream)
                {
                    DataParted.Add(sr.ReadLine());
                }
            }

            if(DataParted.Count == 0)
            {
                DataParted.Add(" ");
            }

            for (int i = 0; i < DataParted.Count; i++)
            {
                if(DataParted[i].Length == 0)
                {
                    DataParted[i] = " ";
                }
            }
        }

        public override void Draw(int LocationX, API api, bool _ = true)
        {
            IMessageBox.DefaultColor();
            Console.SetCursorPosition(0, 1);
            Console.WriteLine($"┌{new Formatter().DoublePadding(Heading, width - 2, '─')}┐");
            int i = 0;
            for (i = 0; i < Settings.WindowDataLimit; i++)
            {
                int actualIndex = 0;
                if (i < DataParted.Count())
                {
                    actualIndex = offset + i;
                    if(actualIndex == selectedRow)
                    {
                        Console.SetCursorPosition(0, i + 2);
                        Console.Write($"|{actualIndex + 1} ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        int a = 0;
                        foreach(char c in DataParted[actualIndex])
                        {
                            if(a == selectedChar)
                            {
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write($"{DataParted[actualIndex][a]}");
                                IMessageBox.DefaultColor();
                            }
                            else
                            {
                                IMessageBox.DefaultColor();
                                Console.Write($"{DataParted[actualIndex][a]}");
                            }
                            a++;
                        }
                        Console.WriteLine($"{ new string(" ").PadRight(width - DataParted[actualIndex].Length - 4)}│");
                    }
                    else
                    {
                        IMessageBox.DefaultColor();
                        Console.SetCursorPosition(0, i + 2);
                        Console.WriteLine($"│{new string($"{actualIndex + 1} {new Formatter().PadTrimRight(DataParted[actualIndex],width - 4)}").PadRight(width - 4)}│");
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
            HandleMBoxChange(info, api);

            if (info.Key == ConsoleKey.DownArrow)
            {

                if(DataParted.Count < Settings.WindowDataLimit && selectedRow < DataParted.Count - 1)
                {
                    selectedRow++;
                    return;
                }

                if (selectedRow <= DataParted.Count() - Settings.WindowDataLimit + offset - 1)
                {
                    selectedRow++;
                    if (selectedRow >= offset + Settings.WindowDataLimit - 2)
                    {
                        offset++;
                    }
                }
            }
            else if (info.Key == ConsoleKey.UpArrow)
            {
                if (selectedRow > 0)
                {
                    selectedRow--;
                    if (selectedRow < offset)
                    {
                        offset--;
                    }
                }
            }
            else if(info.Key == ConsoleKey.RightArrow)
            {
                if(selectedChar < DataParted[selectedRow].Length - 1)
                {
                    selectedChar++;
                }
            }
            else if(info.Key == ConsoleKey.LeftArrow)
            {
                if(selectedChar > 0)
                {
                    selectedChar--;
                }
            }
            else if(char.IsLetterOrDigit(info.KeyChar) || char.IsWhiteSpace(info.KeyChar))
            {
                if (!insertion)
                {
                    DataParted[selectedRow] = dataManager.AddCharToText(DataParted[selectedRow], info);
                    selectedChar = DataParted[selectedRow].Length;

                }
                else if (insertion)
                {
                    DataParted[selectedRow] = dataManager.AddCharToText_Insert(selectedChar, DataParted[selectedRow], info);
                }
            }
            else if(info.Key == ConsoleKey.Backspace)
            {
                if(selectedChar == 0 && DataParted[selectedRow].Length == 0)
                {
                    DataParted[selectedRow] = dataManager.RemoveChar(DataParted[selectedRow],selectedChar);
                    DataParted.RemoveAt(selectedRow);
                }
                else
                {
                    DataParted[selectedRow] = dataManager.RemoveChar(DataParted[selectedRow], selectedChar);
                }
            }
            else if(info.Key == ConsoleKey.Enter)
            {
                DataParted.Add(new string(" "));
            }    
            else if(info.Key == ConsoleKey.F5)
            {
                dataManager.SaveChanges(this.path, this.itemToPreview, this.DataParted);
            }
            else if( info.Key == ConsoleKey.Escape)
            {
                api.CloseActiveWindow();
            }
        }
    }
}
