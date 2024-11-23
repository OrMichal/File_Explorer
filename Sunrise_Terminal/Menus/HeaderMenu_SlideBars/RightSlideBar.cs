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
    public class RightSlideBar : Window, ISlideBar
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int SelectedOperation { get; set; } = 0;
        public List<Operation> Operations { get; set; }
        private List<string> operationNames { get; set; }
        private int LocationX { get; set; }

        private Graphics graphics = new Graphics();


        public RightSlideBar(int width, int locationX)
        {
            this.LocationX = locationX;
            Width = width;

            Operations = new List<Operation>()
            {
                new Operation(){ Name = "File listing" },
                new Operation(){ Name = "Quick view" },
                new Operation(){ Name = "Info" },
                new Operation(){ Name = "Tree" },
                new Operation(){ Name = "Listing format" },
                new Operation(){ Name = "Sort order" },
                new Operation(){ Name = "Filter" },
                new Operation(){ Name = "Encoding"},
                new Operation(){ Name = "FTP link"},
                new Operation(){ Name = "Shell link"},
                new Operation(){ Name = "SMB link"},
                new Operation(){ Name = "Panelize"},
                new Operation(){ Name = "Rescan"}
            };

            operationNames = Operations.Select(o => o.Name).ToList();
            LocationX = locationX;
        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            graphics.DrawListBox(Width, Height, this.LocationX, 1, operationNames, SelectedOperation);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if (info.Key == ConsoleKey.Escape)
            {
                api.Erase(this.Width, this.Height, this.LocationX, 1);
                api.CloseActiveWindow();
                api.ReDrawDirPanel();
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
