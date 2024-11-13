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

        private List<Button> buttons = new List<Button>();
        private DataManager dataManager = new DataManager();
        private int justOnce = 0;
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
            if(justOnce == 0)
            {
                graphics.DrawSquare(this.width, this.height, Console.BufferWidth/2 - this.width/2, Console.WindowHeight/2 - this.height/2, Heading);
                justOnce++;
            }
            graphics.DrawButtons(buttonWidth, Console.WindowWidth / 2 - buttonWidth, Console.WindowHeight / 2 - this.height / 2 + MarginTop, this.buttons, selectedButton);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if (info.Key == ConsoleKey.Escape)
            {
                api.Application.SwitchWindow(api.Application.ListWindows[0]);
                api.Application.activeWindows.Pop();
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

                api.Application.SwitchWindow(api.GetActiveListWindow());
                api.Application.activeWindows.Pop();
            }
        }

        
    }
}

