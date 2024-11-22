using Sunrise_Terminal.Core;
using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.Menus.HeaderMenu_SlideBars
{
    public class OptionsSlideBar : Window, ISlideBar
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int SelectedOperation { get; set; } = 0;
        public List<Operation> Operations { get; set; }
        private List<string> operationNames { get; set; }

        private Graphics graphics = new Graphics();


        public OptionsSlideBar(int width)
        {
            Width = width;

            Operations = new List<Operation>()
            {
                new Operation(){ Name = "Layout" },
                new Operation(){ Name = "Configuration" },
                new Operation(){ Name = "Learn keys" },
                new Operation(){ Name = "Appearance" },
                new Operation(){ Name = "Display bits" },
                new Operation(){ Name = "Save setup" },
                new Operation(){ Name = "Save panels setup" },
                new Operation(){ Name = "Panel options" }
            };

            operationNames = Operations.Select(o => o.Name).ToList();
        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            graphics.DrawListBox(Width, Height, this.LocationX, 1, operationNames, SelectedOperation);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if (info.Key == ConsoleKey.Escape)
            {
                api.CloseActiveWindow();
            }

            if (info.Key == ConsoleKey.DownArrow)
            {
                if (SelectedOperation < Operations.Count - 1)
                {
                    SelectedOperation++;
                }
            }

            if (info.Key == ConsoleKey.UpArrow)
            {
                if (SelectedOperation > 0)
                {
                    SelectedOperation--;
                }
            }
        }
    }
}
