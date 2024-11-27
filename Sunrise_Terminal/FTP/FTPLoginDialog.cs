using Sunrise_Terminal.Core;
using Sunrise_Terminal.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.FTP
{
    public class FTPLoginDialog : Window, IMessageBox
    {
        public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; } = "FTP login";
        public string Description { get; set; }
        public string adressInput { get; set; } = "";
        public string username { get; set; } = "";
        public string password { get; set; } = "";
        private int LocationX { get; set; }
        private int LocationY { get; set; }

        private int selectedCharAdress = 0;
        private int selectedCharPasswd = 0;
        private int selectedCharUsername = 0;

        private int offsetAdress = 0;
        private int offsetPasswd = 0;
        private int offsetUsername = 0;
        public FTPLoginDialog(int width, int height)
        {
            this.width = width;
            this.height = height;


            this.LocationX = Console.WindowWidth / 2 - this.width / 2;
            this.LocationY = Console.WindowHeight / 2 - this.height / 2;

        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            graphics.DrawSquare(this.width, this.height, this.LocationX, this.LocationY, this.Heading);

            graphics.DrawTextBox(this.width - 2, this.LocationX + 1, this.LocationY + 1, this.adressInput, this.selectedCharAdress, this.offsetAdress);
            graphics.DrawTextBox(this.width - 2, this.LocationX + 1, this.LocationY + 5, this.username, this.selectedCharUsername, this.offsetUsername);
            graphics.DrawTextBox(this.width - 2, this.LocationX + 1, this.LocationY + 9, this.password, this.selectedCharPasswd, this.offsetPasswd);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            base.HandleKey(info, api);
        }
    }
}
