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
        public bool active { get; set; }
        private int offset { get; set; }
        private int selectedRow = 0;
        private int selectedChar = 0;
        public string path { get; set; }
        public string itemToPreview { get; set; }
        public List<string> DataParted { get; set; } = new List<string>();
        public int Limit { get; set; }
        private bool dataLoaded { get; set; } = false;
        public StreamReader SReader;
        public StreamWriter SWriter;
        private bool insertion = false;

        public bool WindowOnline { get; set; }
        public bool WindowActiveRequest { get; set; }

        public EditMessageBox(int Width, int Height)
        {
            this.width = Width;
            this.height = Height;
            Heading = "Editation";
        }

        public override void Draw(int LocationX, API api, bool _ = true)
        {
            if (!dataLoaded)
            {
                DataParted = new StreamReader(Path.Combine(path, itemToPreview)).ReadToEnd().Split('\n').ToList();
                dataLoaded = true;
            }
            IMessageBox.DefaultColor();
            Console.WriteLine($"┌─{new Formatter().DoublePadding(Heading, width + 4, '─')}─┐");

            for (int i = 0; i < Limit; i++)
            {
                int actualIndex = 0;
                if (i < DataParted.Count())
                {
                    actualIndex = offset + i;
                    if(actualIndex == selectedRow)
                    {
                        Console.Write($"| {actualIndex + 1} ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        int a = 0;
                        foreach(char c in DataParted[actualIndex])
                        {
                            if(a == selectedChar)
                            {
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write($"{DataParted[actualIndex][a]}");
                            }
                            else
                            {
                                IMessageBox.DefaultColor();
                                Console.Write($"{DataParted[actualIndex][a]}");
                            }
                            a++;
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        IMessageBox.DefaultColor();
                        Console.WriteLine($"| {actualIndex + 1} {new Formatter().PadTrimRight(DataParted[actualIndex],width + 2)} |");
                    }
                }
                else
                {
                    IMessageBox.DefaultColor();
                    Console.WriteLine($"| {new string(' ', width + 2)} |");
                }
            }
            IMessageBox.DefaultColor();
            Console.WriteLine($"└─{new string('─', width + 4)}─┘");
        }

        public void SaveChanges()
        {
            using (StreamWriter sr = new StreamWriter(Path.Combine(path,itemToPreview)))
            {
                foreach(string item in DataParted)
                {
                    sr.WriteLine(item);
                }
            }
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if (info.Key == ConsoleKey.DownArrow)
            {
                    
                if (selectedRow <= DataParted.Count() - Limit + offset - 1)
                {
                    selectedRow++;
                    if (selectedRow >= offset + Limit - 1)
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
                if(!insertion) DataParted[selectedRow] = AddCharToText(DataParted[selectedRow],info);
                else if(insertion) DataParted[selectedRow] = AddCharToText_Insert(DataParted[selectedRow],info);
            }
            else if(info.Key == ConsoleKey.Backspace)
            {
                if(selectedChar == 0 && DataParted[selectedRow].Length == 0)
                {
                    DataParted[selectedRow] = RemoveChar(DataParted[selectedRow],selectedChar);
                    DataParted.RemoveAt(selectedRow);
                }
                else
                {
                    DataParted[selectedRow] = RemoveChar(DataParted[selectedRow], selectedChar);
                }
            }
            else if(info.Key == ConsoleKey.Enter)
            {
                selectedRow++;
                DataParted.Add(" ");
            }    
            else if(info.Key == ConsoleKey.F5)
            {
                SaveChanges();
                this.active = false;
                WindowOnline = true;
                WindowActiveRequest = false;
            }
            else if( info.Key == ConsoleKey.Escape)
            {
                api.Application.SwitchWindow(api.Application.ListWindows[0]);
            }
        }

        public string AddCharToText_Insert(string text, ConsoleKeyInfo info)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (char c in text)
            {
                if (i == selectedChar)
                {
                    sb.Append(info.KeyChar);
                }
                else
                {
                    sb.Append(c);
                }
                i++;
            }

            return sb.ToString();

        }

        public string AddCharToText(string text, ConsoleKeyInfo info)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                sb.Append(c);
            }
            sb.Append(info.KeyChar);
            if (selectedRow >= offset + Limit)
            {
                offset++;
            }

            return sb.ToString();

        }


        public string RemoveChar(string text, int removalLocation)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (char c in text)
            {
                if (i != removalLocation)
                {
                    sb.Append(c);
                }
                i++;
            }

            if(selectedChar > 0) selectedChar--;
            return sb.ToString();
        }
    }
}
