using Sunrise_Terminal.Core;
using Sunrise_Terminal.DataHandlers;
using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.windows;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal
{
    public class CopyMessageBox : Window, IMessageBox
    {
        public bool confirmation = false;
        public string Heading { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public string Description { get; set; }
        public string srcPath { get; set; }
        public int destWindowIndex { get; set; } = 1;
        public string destPath { get; set; }
        public Window window { get; set; }

        private int selectedPath = 0;
        private List<string> paths = new List<string>();
        private int LocationX {  get; set; }
        private int LocationY { get; set; }
        private DataManager dataManager = new DataManager();

        public CopyMessageBox(int height, int width, API api)
        {
            this.height = height;
            this.width = width;
            Heading = "Copy window";
            Description = "Choose the destination path";
            

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
                if (File.Exists(Path.Combine(api.GetActiveListWindow().ActivePath,api.GetSelectedFile()))) dataManager.copyFile(Path.Combine(api.GetActivePath(), api.GetSelectedFile()), api.Application.ListWindows[this.selectedPath].ActivePath);
                else dataManager.copyDir(Path.Combine(api.GetActivePath(), api.GetSelectedFile()), api.Application.ListWindows[this.selectedPath].ActivePath);

                api.Erase(this.width, this.height, this.LocationX, this.LocationY);
                api.CloseActiveWindow();
                api.RequestFilesRefresh();
            }
        }
    }
}
