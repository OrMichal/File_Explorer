using Sunrise_Terminal.Core;
using Sunrise_Terminal.DataHandlers;
using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.windows;
using System;
using System.Collections.Generic;
using System.IO;
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

        private int LocationX { get; set; }
        private int LocationY { get; set; }
        private List<string> paths = new List<string>();
        private int justOnce;
        private int selectedPath = 0;
        private DataManager dataManager = new DataManager();

        public RenMovMessageBox(int Height, int Width, API api)
        {
            justOnce = 0;
            Heading = "Moving MessageBox";
            Description = "Are you sure?";
            this.height = Height;
            this.width = Width;


            foreach (ListWindow lw in api.Application.ListWindows)
            {
                paths.Add(lw.ActivePath);
            }
        }

        public override void Draw(int LocationX, API api, bool _ = true)
        {
            Thread.Sleep(20);
            this.LocationX = Console.WindowWidth / 2 - this.width / 2 + api.Application.activeWindows.Count;
            this.LocationY = Console.WindowHeight / 2 - this.height / 2 + api.Application.activeWindows.Count;

            graphics.DrawSquare(this.width, this.height, this.LocationX, LocationY, Heading);
            graphics.DrawListBox(this.width - 2, api.Application.ListWindows.Count + 2, this.LocationX + 1, this.LocationY + 1, this.paths, selectedPath);
            graphics.DrawLabel(this.LocationX, this.LocationY + 2 + api.Application.ListWindows.Count + 2, Description, 2);

        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            HandleMBoxChange(info, api);

            if (info.Key == ConsoleKey.Escape)
            {
                api.Erase(this.width, this.height, this.LocationX, this.LocationY);
                api.CloseActiveWindow();
            }
            else if (info.Key == ConsoleKey.UpArrow)
            {
                if (selectedPath > 0)
                {
                    selectedPath--;
                }
            }
            else if (info.Key == ConsoleKey.DownArrow)
            {
                if (selectedPath < paths.Count - 1)
                {
                    selectedPath++;
                }
            }
            else if(info.Key == ConsoleKey.Enter)
            {
                dataManager.Move(Path.Combine(api.GetActivePath(), api.GetSelectedFile()), api.Application.ListWindows[this.selectedPath].ActivePath);
                api.RequestFilesRefresh();
                api.Erase(this.width, this.height, this.LocationX, this.LocationY);
                api.CloseActiveWindow();
            }
        }
    }
}
