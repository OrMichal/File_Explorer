using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.Core;
using Sunrise_Terminal.interfaces;

namespace Sunrise_Terminal.HelperPopUps
{
    public class ErrMessageBox : Window, IMessageBox
    {
        new public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; } = "Error occured";
        public string Description { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }

        public ErrMessageBox(string Message)
        {
            width = Message.Length + 4;
            height = 10;
            Description = Message;
            LocationX = Console.WindowWidth / 2 - width / 2;
            LocationY = Console.WindowHeight / 2 - height / 2;
        }

        public override void Draw(int LocationX, API api, bool _ = true)
        {
            graphics.DrawSquare(width, height, this.LocationX, LocationY, Heading);
            graphics.DrawLabel(this.LocationX, LocationY, Description, 6);
            Console.Beep(2000, 500);
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
