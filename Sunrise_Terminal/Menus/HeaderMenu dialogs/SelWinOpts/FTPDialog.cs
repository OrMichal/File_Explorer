using Sunrise_Terminal.Core;
using Sunrise_Terminal.FTP;
using Sunrise_Terminal.interfaces;
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
        public FTPDialog(int height, int width, string heading, string psw, string username, string ftpAdr, API api)
        {
            this.height = height;
            this.width = width;
            this.ftpService = new FTPService(ftpAdr, username, psw, api);
            this.Heading = heading;

            this.LocationX = Console.WindowWidth / 2 - this.width / 2;
            this.LocationY = Console.WindowHeight / 2 - this.height / 2;
        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            graphics.DrawSquare(this.width, this.height, this.LocationX, this.LocationY, this.Heading);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            base.HandleKey(info, api);
        }
    }
}
