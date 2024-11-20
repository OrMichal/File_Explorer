using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.Menus.HeaderMenu_SlideBars
{
    public class CommandSlideBar : Window, ISlideBar
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int SelectedOperation { get; set; } = 0;
        public List<Operation> Operations { get; set; }
        private List<string> operationNames { get; set; }

        private Graphics graphics = new Graphics();


        public CommandSlideBar(int width)
        {
            Width = width;

            Operations = new List<Operation>()
            {
                new Operation(){ Name = "Select group" },
                new Operation(){ Name = "Unselect grou" },
                new Operation(){ Name = "Reverse selection" },
                new Operation(){ Name = "Quick cd" },
                new Operation(){ Name = "Rescan directory" },
                new Operation(){ Name = "Swap panels" },
                new Operation(){ Name = "Quick View" },
                new Operation(){ Name = "Filter by pattern"},
                new Operation(){ Name = "Edit extension file"},
                new Operation(){ Name = "Edit user menu"},
                new Operation(){ Name = "External panelize"},
                new Operation(){ Name = "View file"},
                new Operation(){ Name = "Edit file"},
                new Operation(){ Name = "Run command"},
                new Operation(){ Name = "Background jobs"}
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
