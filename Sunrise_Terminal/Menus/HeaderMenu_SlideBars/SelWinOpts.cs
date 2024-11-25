using Sunrise_Terminal.Core;
using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.Menus.HeaderMenu_dialogs.Left;

namespace Sunrise_Terminal.Menus.HeaderMenu_SlideBars
{
    public class SelWinOpts :Window, ISlideBar
    {
        public int Height { get; set; }
        public int Width { get; set; }  
        public int SelectedOperation { get; set; } = 0;
        public List<Operation> Operations { get; set; }
        private List<string> operationNames { get; set; }
        new int LocationX { get; set; }

        private Graphics graphics = new Graphics();


        public SelWinOpts( int width, int locationX)
        {
            this.LocationX = locationX;
            this.Width = width;

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
            graphics.DrawListBox(this.Width, this.Height, this.LocationX, 1, operationNames, SelectedOperation);
            Window.DefaultColor();
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if(info.Key == ConsoleKey.Escape)
            {
                api.CloseActiveWindow();
                api.Erase(this.Width, this.Height, this.LocationX, 1);
                api.ReDrawDirPanel();
            }

            if(info.Key == ConsoleKey.DownArrow)
            {
                if(this.SelectedOperation < Operations.Count - 1)
                {
                    this.SelectedOperation++;
                }
            }

            if(info.Key == ConsoleKey.UpArrow)
            {
                if(this.SelectedOperation > 0)
                {
                    this.SelectedOperation--;
                }
            }
            
            if(info.Key == ConsoleKey.Enter)
            {
                if (SelectedOperation == 6) api.Application.SwitchWindow(new FilterDialog(40, 15, "Filter", api));
            }
        }
    }
}
