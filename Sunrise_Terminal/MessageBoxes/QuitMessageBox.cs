using Sunrise_Terminal.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal
{
    public class QuitMessageBox : Window, IMessageBox
    {
        private int selectedButton { get; set; } = 0;
        public string Heading { get; set; }
        public int height { get; set; }
        new public int width { get; set; }
        private int MarginTop { get; set; } = 2;
        private int ButtonWidth { get; set; } = 7;
        public string Description { get; set; }
        private List<Button> buttons = new List<Button>();
        private int LocationX { get; set; }
        private int LocationY { get; set; }

        public QuitMessageBox(int height, int width)
        {
            this.height = height;
            this.width = width;
            Heading = "Quit?";
            buttons.Add(new Button()
            {
                Label = "Yes"
            });
            buttons.Add(new Button()
            {
                Label = "No"
            });
        }

        public override void Draw(int LocationX, API api, bool _ = true)
        {
            Thread.Sleep(20);
            this.LocationX = Console.WindowWidth/2 - this.width/2 + api.Application.activeWindows.Count;
            this.LocationY = Console.WindowHeight / 2 - this.height / 2 + api.Application.activeWindows.Count;

            graphics.DrawSquare(this.width, this.height, this.LocationX, this.LocationY, Heading);
            graphics.DrawButtons(ButtonWidth, this.LocationX + this.width/2 - ButtonWidth, this.LocationY + this.height/2 - this.height / 2 + MarginTop, this.buttons, selectedButton);


        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            HandleMBoxChange(info, api);

            if (info.Key == ConsoleKey.Escape)
            {
                api.Erase(this.width, this.height, this.LocationX, this.LocationY);
                api.CloseActiveWindow();
            }
            else if (info.Key == ConsoleKey.LeftArrow)
            {
                selectedButton = 0;
            }
            else if (info.Key == ConsoleKey.RightArrow)
            {
                selectedButton = 1;
            }
            else if (info.Key == ConsoleKey.Enter)
            {
                if (selectedButton == 0)
                {
                    Environment.Exit(69);
                }
                else
                {
                    api.Application.SwitchWindow(api.Application.ListWindows[0]);
                }
            }
        }
    }
}
