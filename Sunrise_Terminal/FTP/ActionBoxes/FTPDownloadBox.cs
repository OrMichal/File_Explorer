using Sunrise_Terminal.Core;
using Sunrise_Terminal.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.FTP.ActionBoxes
{
    public class FTPDownloadBox : Window, IMessageBox
    {
        public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; } = "FTP Download";
        public string Description { get; set; }

        private int selectedFile = 0;
        private int LocationX { get; set; }
        private int LocationY { get; set; }

        public FTPDownloadBox(int width, int height)
        {
            this.width = width;
            this.height = height;

            this.LocationX = Console.WindowWidth / 2 - this.width/2;
            this.LocationY = Console.WindowHeight / 2 - this.height/2;
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
