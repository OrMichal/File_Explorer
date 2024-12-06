using Sunrise_Terminal.Core;
using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.Menus.HeaderMenu_dialogs.Options;

namespace Sunrise_Terminal.Menus.HeaderMenu_SlideBars
{
    public class OptionsSlideBar : Window, ISlideBar
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int SelectedOperation { get; set; } = 0;
        public List<Operation> Operations { get; set; }
        private List<string> operationNames { get; set; }
        private int LocationX { get; set; }

        private Graphics graphics = new Graphics();


        public OptionsSlideBar(int width, int locationX)
        {
            this.LocationX = locationX;
            Width = width;

            Operations = new List<Operation>()
            {
                new Operation(){ Name = "Change Color" }
            };

            operationNames = Operations.Select(o => o.Name).ToList();
            LocationX = locationX;
        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            graphics.DrawListBox(Width, Height, this.LocationX, 1, operationNames, SelectedOperation);
            Window.DefaultColor();
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if (info.Key == ConsoleKey.Escape)
            {
                api.CloseActiveWindow();
                api.Erase(this.Width, this.Height, this.LocationX, 1);
                api.ReDrawDirPanel();
            }

            else if (info.Key == ConsoleKey.DownArrow)
            {
                if (SelectedOperation < Operations.Count - 1)
                {
                    SelectedOperation++;
                }
            }

            else if (info.Key == ConsoleKey.UpArrow)
            {
                if (SelectedOperation > 0)
                {
                    SelectedOperation--;
                }
            }

            else if(info.Key == ConsoleKey.Enter)
            {
                if(SelectedOperation == 0)
                {
                    api.Application.SwitchWindow(new ColorChanger(40,20));
                }
            }
        }
    }
}
