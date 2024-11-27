using Sunrise_Terminal.Core;
using Sunrise_Terminal.DataHandlers;
using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.FunctionMessageBoxes.EditMessageBox
{
    public class QuitEditMessageBox : Window, IMessageBox
    {
        public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }

        private List<Button> buttons;
        private EditMessageBox editBox;
        private int selectedButton = 0;
        public QuitEditMessageBox(int width, int height, EditMessageBox editbox)
        {
            this.width = width;
            this.height = height;
            this.editBox = editbox;
            this.LocationX = Console.WindowWidth/2 - this.width/2;
            this.LocationY = Console.WindowHeight/2 - this.height/2;

            buttons = new List<Button>()
            {
                new(){Label = "save"},
                new(){Label = "cancel"},
                new(){Label = "quit"}
            };
        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            graphics.DrawSquare(this.width, this.height,this.LocationX,this.LocationY);
            graphics.DrawLabel(this.LocationX + this.width / 5, this.LocationY + 1, "You have unsaved progress",3);
            graphics.DrawLabel(this.LocationX + this.width / 5, this.LocationY + 4, "do you want to save it?",4);
            graphics.DrawButtons(this.LocationX + this.width/4, this.LocationY + 6, this.buttons, this.selectedButton, 1);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if (info.Key == ConsoleKey.Escape)
            {
                api.CloseActiveWindow();
            }
            else if(info.Key == ConsoleKey.LeftArrow)
            {
                if(selectedButton > 0)
                {
                    selectedButton--;
                }
            }
            else if(info.Key == ConsoleKey.RightArrow)
            {
                if(selectedButton < buttons.Count - 1)
                {
                    selectedButton++;
                }
            }
            else if(info.Key == ConsoleKey.Enter)
            {
                if(selectedButton == 0)
                {
                    new DataManagement().SaveChanges(api.GetActiveListWindow().ActivePath, api.GetSelectedFile(), editBox.Rows);
                    api.CloseActiveWindow();
                    api.CloseActiveWindow();
                }
                else if(selectedButton == 1)
                {
                    api.CloseActiveWindow();
                }
                else if(selectedButton == 2)
                {
                    api.CloseActiveWindow();
                    api.CloseActiveWindow();
                }
            }
        }
    }
}
