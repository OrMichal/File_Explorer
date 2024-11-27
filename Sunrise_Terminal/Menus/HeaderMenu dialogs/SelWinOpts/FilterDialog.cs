using Sunrise_Terminal.Core;
using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.Menus.HeaderMenu_Actions;
using Sunrise_Terminal.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Sunrise_Terminal.Menus.HeaderMenu_dialogs.Left
{
    public class FilterDialog : Window, IMessageBox
    {
        public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        private List<CheckBox> checkBoxes;
        private int selectedBox = 0;
        private HeaderActions headerActions;
        private List<Button> buttons;
        private int selectedButton = 0;

        public FilterDialog(int width, int height, string heading, API api)
        {
            this.headerActions = new HeaderActions(api);
            this.checkBoxes = new List<CheckBox>() 
            { 
                new CheckBox(){ Text = "Files"},
                new CheckBox(){ Text = "Directories"},
                new CheckBox(){ Text = "Date"}
            };

            this.buttons = new List<Button>()
            {
                new() {Label = "Yes"},
                new() {Label = "No"},
                new() {Label = "Reset Filter"}
            };

            this.width = width;
            this.height = height;
            this.Heading = heading;
            this.LocationX = Console.WindowWidth / 2 - this.width / 2;
            this.LocationY = Console.WindowHeight / 2 - this.height / 2;
        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            graphics.DrawSquare(this.width, this.height, this.LocationX, this.LocationY, this.Heading);
            graphics.DrawCheckBoxes(this.LocationX + 1, this.LocationY + 1, this.checkBoxes, this.selectedBox);
            graphics.DrawButtons(this.LocationX + this.width/3, this.LocationY + this.height - 5, this.buttons, selectedButton);
            Window.DefaultColor();
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if(info.Key == ConsoleKey.Escape)
            {
                api.CloseActiveWindow();
                api.ReDrawDirPanel();
            }
            else if(info.Key == ConsoleKey.Spacebar)
            {
                checkBoxes[selectedBox].check();
            }
            else if(info.Key == ConsoleKey.Tab)
            {
                selectedBox = (selectedBox + 1) % checkBoxes.Count;
            }
            else if(info.Key == ConsoleKey.Enter)
            {
                if(selectedButton == 0)
                {
                    headerActions.Filter(this.checkBoxes);
                }

                if (selectedButton == 2)
                {
                    headerActions.ResetFilters();
                    foreach (var item in this.checkBoxes)
                    {
                        if(item.activeChoice == 'x')
                        {
                            item.check();
                        }
                    }
                }

                api.CloseActiveWindow();
                api.ReDrawDirPanel();
            }
            else if(info.Key == ConsoleKey.RightArrow)
            {
                if(selectedButton < buttons.Count - 1)
                {
                    selectedButton++;
                }
            }
            else if(info.Key == ConsoleKey.LeftArrow)
            {
                if(selectedButton > 0)
                {
                    selectedButton--;
                }
            }
        }

    }
}
