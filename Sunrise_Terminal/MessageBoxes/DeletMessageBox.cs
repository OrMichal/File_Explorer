using Sunrise_Terminal.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal
{
    public class DeletMessageBox : Window, IMessageBox
    {
        public string Heading { get; set; }
        public int height { get; set; }
        new public int width { get; set; }
        public string Description { get; set; }
        private int LocationX { get; set; }
        private int LocationY { get; set; }

        private List<Button> buttons = new List<Button>();
        private DataManager dataManager = new DataManager();
        private int selectedButton = 0;
        private int MarginTop = 2;
        private int buttonWidth = 7;
        public DeletMessageBox(int Height, int Width)
        {
            height = Height; 
            width = Width;
            Heading = "Delete?";
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
            this.LocationX = Console.WindowWidth / 2 - this.width / 2 + api.Application.activeWindows.Count;
            this.LocationY = Console.WindowHeight / 2 - this.height / 2 + api.Application.activeWindows.Count;

            graphics.DrawSquare(this.width, this.height, this.LocationX , this.LocationY , Heading);
            graphics.DrawButtons(buttonWidth, this.LocationX + this.width / 2 - buttonWidth, LocationY + this.height/2 - this.height / 2 + MarginTop, this.buttons, selectedButton);
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
            else if(info.Key == ConsoleKey.RightArrow)
            {
                selectedButton = 1;
            }
            else if(info.Key == ConsoleKey.Enter)
            {
                if (selectedButton == 0)
                {
                    if(File.Exists(Path.Combine(api.GetActiveListWindow().ActivePath, api.GetSelectedFile())))
                    {
                        dataManager.DeleteFile(Path.Combine(api.GetActiveListWindow().ActivePath, api.GetSelectedFile()));
                    }
                    else
                    {
                        dataManager.DeleteDir(api.GetActiveListWindow().ActivePath, api.GetSelectedFile());
                    }
                    api.RequestFilesRefresh();
                }

                api.Erase(this.width, this.height, this.LocationX, this.LocationY);
                api.CloseActiveWindow();
                api.Application.activeWindows.Pop();
            }
        }

        
    }
}

