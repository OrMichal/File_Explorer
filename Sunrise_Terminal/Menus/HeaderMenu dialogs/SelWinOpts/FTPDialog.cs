using Sunrise_Terminal.Core;
using Sunrise_Terminal.FTP;
using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.Menus.HeaderMenu_dialogs.SelWinOpts
{
    public class FTPDialog : Window, IMessageBox
    {
        public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        new private int LocationX { get; set; }
        private int LocationY { get; set; }
        private int selectedAction = 0;
        private int selectedButton = 0;
        private FTPService ftpService;

        private List<Operation> operations;
        private List<Button> buttons;
        private List<Row> ftpFiles = new List<Row>();

        public FTPDialog(int height, int width, string heading, string psw, string username, string ftpAdr, API api)
        {
            this.height = height;
            this.width = width;
            this.ftpService = new FTPService(ftpAdr, username, psw, api);
            this.Heading = heading;

            this.LocationX = Console.WindowWidth / 2 - this.width / 2;
            this.LocationY = Console.WindowHeight / 2 - this.height / 2;

            this.operations = new List<Operation>()
            {
                new(){Name = "Create"},
                new(){Name = "Download"},
                new(){Name = "Preview"},
                new(){Name = "Link"}
            };

            this.buttons = new List<Button>()
            {
                new(){Label = "Create"},
                new(){Label = "Download"},
                new(){Label = "Preview"},
                new(){Label = "Link"}
            };


        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            graphics.DrawSquare(this.width, this.height, this.LocationX, this.LocationY, this.Heading);
            graphics.DrawButtons(this.LocationX + 2, this.LocationY + 2, this.buttons, selectedButton);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if (info.Key == ConsoleKey.Escape)
            {
                api.CloseActiveWindow();
            }
            else if(info.Key == ConsoleKey.Tab)
            {
                selectedButton = (selectedButton + 1) % this.buttons.Count;
            }
        }
    }
}
