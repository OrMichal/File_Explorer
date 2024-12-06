using Sunrise_Terminal.Core;
using Sunrise_Terminal.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.Menus.HeaderMenu_dialogs.SelWinOpts
{
    public class DriverSelection : Window, IMessageBox
    {
        public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; } = "Driver Selection";
        public string Description { get; set; }

        private int LocationX = 0;
        private int LocationY = 0;

        private List<DriveInfo> drives { get; set; } = DriveInfo.GetDrives().ToList();
        private int selectedDrive = 0;

        private List<string> driveNames { get; set; }

        public DriverSelection(int width)
        {
            this.width = width;
            this.height = drives.Count + 4;

            this.LocationX = Console.WindowWidth / 2 - this.width / 2;
            this.LocationY = Console.WindowHeight / 2 - this.height / 2;

            driveNames = drives.Select(x => x.Name).ToList();
        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            graphics.DrawSquare(this.width, this.height, this.LocationX, this.LocationY, this.Heading);
            graphics.DrawListBox(this.width - 2, this.height, this.LocationX + 1, this.LocationY + 1, this.driveNames, this.selectedDrive);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if(info.Key == ConsoleKey.Escape)
            {
                api.CloseActiveWindow();
            }
            else if(info.Key == ConsoleKey.Enter)
            {
                api.GetActiveListWindow().ActivePath = drives[this.selectedDrive].Name;
                api.CloseActiveWindow();
                api.CloseActiveWindow();
                api.CloseActiveWindow();

                api.RequestFilesRefresh();
                api.ReDrawDirPanel();
            }
            else if(info.Key == ConsoleKey.Tab)
            {
                this.selectedDrive = (this.selectedDrive + 1) % drives.Count;
            }
        }
    }
}
