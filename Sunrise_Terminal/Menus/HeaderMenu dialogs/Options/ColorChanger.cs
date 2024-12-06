using Sunrise_Terminal.Core;
using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.objects;
using Sunrise_Terminal.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.Menus.HeaderMenu_dialogs.Options
{
    public class ColorChanger : Window, IMessageBox
    {
        public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; } = "Theme change";
        public string Description { get; set; }

        private int LocationX { get; set; }
        private int LocationY { get; set; }
        private int selectedColor = 0;

        private List<Operation> colorChBoxes;
        private List<string> colorStrings;

        public ColorChanger(int width, int height)
        {
            this.height = height;
            this.width = width;

            this.LocationX = Console.WindowWidth/2 - this.width/2;
            this.LocationY = Console.WindowHeight/2 - this.height/2;

            colorChBoxes = new List<Operation>()
            {
                new(){Name = "Default", Use = Themes.Default},
                new(){Name = "Hello kitty", Use = Themes.HelloKitty},
                new(){Name = "Matrix", Use = Themes.Metrix},
                new(){Name = "Solarized Dark", Use = Themes.SolarizedDark},
                new(){Name = "Sunrise", Use = Themes.Sunrise},
                new(){Name = "Communism", Use = Themes.Communism}

            };

            colorStrings = colorChBoxes.Select(x => x.Name).ToList();
        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            graphics.DrawSquare(this.width, this.height, this.LocationX, this.LocationY, this.Heading);
            graphics.DrawListBox(this.width - 2, this.height, this.LocationX + 1, this.LocationY + 1, this.colorStrings, this.selectedColor);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if(info.Key == ConsoleKey.Escape)
            {
                api.CloseActiveWindow();
                api.RequestFilesRefresh();
            }
            else if(info.Key == ConsoleKey.Tab)
            {
                this.selectedColor = (this.selectedColor + 1) % this.colorChBoxes.Count;
            }
            else if(info.Key == ConsoleKey.Enter)
            {
                colorChBoxes[this.selectedColor].Use();
                api.CloseActiveWindow();
                api.CloseActiveWindow();
                api.CloseActiveWindow();
            }
        }
    }
}
