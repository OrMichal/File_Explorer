using Sunrise_Terminal.Core;
using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.HelperPopUps
{
    public class BootingPopUp : Window, IMessageBox
    {
        new public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; } = "Boot";
        public string Description { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }

        public BootingPopUp(int Width, int Height, string Message)
        {
            width = Width;
            height = Height;
            Description = Message;
            LocationX = Console.WindowWidth / 2 - width / 2;
            LocationY = Console.WindowHeight / 2 - height / 2;
        }

        public override void Draw(int LocationX, API api, bool _ = true)
        {
            graphics.DrawSquare(width, height, this.LocationX, LocationY, Heading);
            graphics.DrawLabel(this.LocationX, LocationY + 2, Description, 6);
        }
    }
}
