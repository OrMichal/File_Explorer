using Sunrise_Terminal.Core;
using Sunrise_Terminal.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.HelperPopUps
{
    public class FTPResDialog : Window, IMessageBox
    {
        new public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; } = "FTP message";
        public string Description { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }

        public FTPResDialog(int Width, int Height, string Message)
        {
            width = Width;
            height = Height;
            this.Description = Message;
            LocationX = Console.WindowWidth / 2 - width / 2;
            LocationY = Console.WindowHeight / 2 - height / 2;
        }

        public override void Draw(int LocationX, API api, bool _ = true)
        {
            graphics.DrawSquare(width, height, this.LocationX, LocationY, Heading);
            graphics.DrawLabel(this.LocationX, LocationY + 4, Description, 6);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if (info.Key == ConsoleKey.Enter)
            {
                api.CloseActiveWindow();
            }
            else if (info.Key == ConsoleKey.Escape)
            {
                api.CloseActiveWindow();
            }
        }
    }
}
