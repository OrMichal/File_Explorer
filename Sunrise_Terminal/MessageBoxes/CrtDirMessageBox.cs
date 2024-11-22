using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.Core;
using Sunrise_Terminal.DataHandlers;
using Sunrise_Terminal.interfaces;

namespace Sunrise_Terminal
{
    public class CrtDirMessageBox : Window, IMessageBox
    {
        public static string Input { get; set; } = "New Directory";
        public string Heading { get; set; }
        public int height { get; set; }
        new public int width { get; set; }
        public string Description { get; set; }
        public static int Offset { get; set; }
        public static int Limit {get; set; }
        public static int selected { get; set; }
        private bool insertion { get; set; }
        private bool pathSelection { get; set; }
        private int SelectedPath { get; set; } = 0;
        private int Margin { get; set; } = 2;
        private int LocationX { get; set; }
        private int LocationY { get; set; }

        private List<string> paths { get; set; } = new List<string>();
        private DataManagement dataManagement = new DataManagement();

        public CrtDirMessageBox(int height, int width, API api) 
        {
            this.height = height;
            this.width = width;
            Heading = "Directory creation";
            Offset = 0;
            selected = Input.Length - 1;
            Limit = width - 1;

            foreach(var item in api.Application.ListWindows)
            {
                paths.Add(item.ActivePath);
            }
        }

        public override void Draw(int LocationX, API api, bool _ = true)
        {
            Thread.Sleep(20);
            this.LocationX = Console.WindowWidth / 2 - this.width / 2 + api.Application.activeWindows.Count;
            this.LocationY = Console.WindowHeight / 2 - this.height / 2 + api.Application.activeWindows.Count;

            graphics.DrawSquare(this.width, this.height, this.LocationX, LocationY, Heading);
            graphics.DrawListBox(this.width - 2, this.paths.Count + 2, this.LocationX + 1, LocationY + 1, this.paths, SelectedPath);
            graphics.DrawTextBox(this.width - 2, this.LocationX + 1, this.LocationY + 2 + this.paths.Count + 2, Input, selected, Offset);

        }

        public override void HandleKey(ConsoleKeyInfo key, API api)
        {
            HandleMBoxChange(key, api);

            if (key.Key == ConsoleKey.Enter)
            {
                dataManagement.CreateDir(paths[SelectedPath], Input);
                api.Erase(this.width, this.height, this.LocationX, this.LocationY);
                api.CloseActiveWindow();
                api.RequestFilesRefresh();

            }
            else if (key.Key == ConsoleKey.LeftArrow && !pathSelection)
            {
                if (selected > 0)
                {
                    selected--;
                    if (selected < Offset)
                    {
                        Offset--;
                    }
                } 


            }
            else if (key.Key == ConsoleKey.RightArrow && !pathSelection)
            {
                if (selected <= Input.Length - 2)
                {
                    selected++;
                    if (selected >= Offset + Limit)
                    {
                        Offset++;
                    }
                }
            }

            else if(key.Key == ConsoleKey.DownArrow && pathSelection)
            {
                if(SelectedPath < paths.Count - 1)
                {
                    SelectedPath++;
                }
            }

            else if(key.Key == ConsoleKey.UpArrow && pathSelection)
            {
                if(SelectedPath > 0)
                {
                    SelectedPath--;
                }
            }

            else if(key.Key == ConsoleKey.Tab)
            {
                pathSelection = !pathSelection;
            }

            else if (key.Key == ConsoleKey.Backspace)
            {
                Input = dataManagement.RemoveChar(Input, selected);
                if (selected > 0)
                {
                    selected--;
                }
            }

            else if(key.Key == ConsoleKey.Escape)
            {
                api.Erase(this.width, this.height, this.LocationX, this.LocationY);
                api.CloseActiveWindow();
            }
            else if(key.Key == ConsoleKey.Insert)
            {
                insertion = !insertion;
            }

            else if (Char.IsLetterOrDigit(key.KeyChar) || Char.IsWhiteSpace(key.KeyChar))
            {
                if (insertion)
                {
                    Input = dataManagement.AddCharToText_Insert(selected, Input, key);
                }
                else
                {
                    Input = dataManagement.AddCharToText(Input, key);
                    if (selected >= Offset + Limit)
                    {
                        Offset++;
                    }
                    selected = Input.Length - 1;
                }
            }
        }

        
    }
}
