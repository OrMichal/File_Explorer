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
    public class FileSlideBar : Window, ISlideBar
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int SelectedOperation { get; set; } = 0;
        public List<Operation> Operations { get; set; }
        private List<string> operationNames { get; set; }

        private Graphics graphics = new Graphics();
        new int LocationX { get; set; }

        public FileSlideBar(int width, int locationX)
        {
            this.LocationX = locationX;
            this.Width = width;

            Operations = new List<Operation>()
            {
                new Operation(){ Name = "Copy" },
                new Operation(){ Name = "Rename/Move" },
                new Operation(){ Name = "Link" },
                new Operation(){ Name = "Delete" },
                new Operation(){ Name = "Quick view" },
                new Operation(){ Name = "Edit" },
                new Operation(){ Name = "View" },
                new Operation(){ Name = "Find file"},
                new Operation(){ Name = "File properties"},
                new Operation(){ Name = "Compare directories"},
                new Operation(){ Name = "Advanced Chown"},
                new Operation(){ Name = "Advanced Chmod"},
                new Operation(){ Name = "Symlink"},
                new Operation(){ Name = "Touch"}
            };

            operationNames = Operations.Select(o => o.Name).ToList();
            LocationX = locationX;
        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            graphics.DrawListBox(this.Width, this.Height, this.LocationX, 1, operationNames, SelectedOperation);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if (info.Key == ConsoleKey.Escape)
            {
                api.CloseActiveWindow();
                api.Erase(this.Width, this.Height, this.LocationX, 1);
                api.ReDrawDirPanel();
            }

            if (info.Key == ConsoleKey.DownArrow)
            {
                if (this.SelectedOperation < Operations.Count - 1)
                {
                    this.SelectedOperation++;
                }
            }

            if (info.Key == ConsoleKey.UpArrow)
            {
                if (this.SelectedOperation > 0)
                {
                    this.SelectedOperation--;
                }
            }
        }
    }
}
