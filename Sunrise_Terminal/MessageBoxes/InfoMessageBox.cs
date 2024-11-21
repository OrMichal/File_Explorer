using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal
{
    public class InfoMessageBox : Window, IMessageBox
    {
        new public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; } = "Error occured";
        public string Description { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }

        public InfoMessageBox(int Width, int Height, string Message)
        {
            this.width = Width;
            this.height = Height;
            this.Description = Message;
            this.LocationX = Console.WindowWidth / 2 - this.width / 2;
            this.LocationY = Console.WindowHeight / 2 - this.height / 2;
        }

        public override void Draw(int LocationX, API api, bool _ = true)
        {
            graphics.DrawSquare(this.width, this.height, this.LocationX, this.LocationY, this.Heading);
            graphics.DrawLabel(this.LocationX, this.LocationY + 4, Description, 6);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if (info.Key == ConsoleKey.Enter)
            {
                api.CloseActiveWindow();
                api.CloseActiveWindow();
            }
            else if(info.Key == ConsoleKey.Escape)
            {
                api.CloseActiveWindow();
                api.CloseActiveWindow();
            }
        }
    }
}
