using Sunrise_Terminal.Core;
using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.Menus.HeaderMenu_SlideBars;
using Sunrise_Terminal.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal
{
    public class HeaderMenu : Window, IMenu
    {
        public List<Object> objects { get; set; }
        public int selectedObject { get; set; } = 0;
        private bool online = true;
        public int slideBarLocationX { get; set; }
        public HeaderMenu()
        {
            objects = new List<Object>(){ 
                new Object(){name = "SelWinOpts"},
                new Object(){name = "File"},
                new Object(){name = "Command"},
                new Object(){name = "Options"}
            };
        }


        public override void Draw(int LocationX, API api, bool _ = true)
        {
            Console.SetCursorPosition(0, 0);
            IMenu.DefaultColor();
            int i = 0;
            foreach (Object obj in objects)
            {
                if(selectedObject == i && api.Application.activeWindow == this)
                {
                    Console.Write("   ");
                    IMenu.SelectedColor();
                    Console.Write($"{obj.name}");
                    IMenu.DefaultColor();
                    Console.Write(new string(' ', Console.WindowWidth / 15));
                }
                else
                {
                    Console.Write($"   {obj.name.PadRight(Console.WindowWidth / 15 + obj.name.Length)}");
                }
                i++;
            }

            
            slideBarLocationX = GetLocation();
            
            Console.WriteLine(new string(' ', GetLastGapLength()));
            Settings.ActiveColor();
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            
            //------------------------------------------------------------------------------------------------------------------------------------left arrow key
            if (info.Key == ConsoleKey.LeftArrow)
            {
                if(selectedObject > 0)
                {
                    selectedObject--;
                }
            }
            //------------------------------------------------------------------------------------------------------------------------------------right arrow key
            else if (info.Key == ConsoleKey.RightArrow)
            {
                if(selectedObject < objects.Count - 1)
                {
                    selectedObject++;
                }
            }
            else if(info.Key == ConsoleKey.Enter)
            {
                if (selectedObject == 0) OpenSlideBar(new SelWinOpts(20, this.slideBarLocationX), api);
                else if (selectedObject == 1) OpenSlideBar(new FileSlideBar(25, this.slideBarLocationX), api);
                else if (selectedObject == 2) OpenSlideBar(new CommandSlideBar(23, this.slideBarLocationX), api);
                else if (selectedObject == 3) OpenSlideBar(new OptionsSlideBar(20, this.slideBarLocationX), api);
            }
            //------------------------------------------------------------------------------------------------------------------------------------F9 key
            else if (info.Key == ConsoleKey.F9 || info.Key == ConsoleKey.Escape)
            {
                api.Application.SwitchWindow(api.GetActiveListWindow());
            }
        }

        private int GetLastGapLength()
        {
            int GapLength = 0;
            foreach (Object obj in objects)
            {
                GapLength += (obj.name.Length + Console.WindowWidth / 15 + 3);
            }
            return (Console.WindowWidth - GapLength) % 2 == 0 ? Console.WindowWidth - GapLength : Console.WindowWidth - GapLength + 1;
        }

        private void OpenSlideBar(Window slideBar, API api)
        {
            api.Application.activeWindows.Push(slideBar);
        }

        private int GetLocation()
        {
            int num = 0;
            for (int i = 0; i < selectedObject; i++)
            {
                num +=  3 + objects[i].name.Length + Console.WindowWidth/15;
            }
            return num;
        }
    }
}
