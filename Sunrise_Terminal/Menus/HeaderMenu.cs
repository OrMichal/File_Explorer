using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.Menus.HeaderMenu_SlideBars;
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
        public List<Object> objects { get; set; } = new List<Object>();
        public Stack<ISlideBar> slideBars { get; set; } = new Stack<ISlideBar>();
        public int selectedObject { get; set; } = 0;

        public ISlideBar ActiveSlideBar
        {
            get
            {
                if(slideBars.Count > 0)
                {
                    return slideBars.Peek();
                }
                return null;
            }
        }
        public HeaderMenu()
        {
            using(StreamReader sr = new StreamReader($@"C:\Users\{Environment.UserName}\Desktop\Sunrise_Terminal\Sunrise_Terminal\HeaderData.json"))
            {
                string text = sr.ReadToEnd();
                string[] parts = text.Split(';');
                foreach(string part in parts)
                {
                    objects.Add(new Object()
                    {
                        name = part,
                    });
                }
            }
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

            if(ActiveSlideBar != null)
            {
                ActiveSlideBar.Draw((3 + objects[selectedObject].name.Length) * selectedObject + 1);
            }

            Console.WriteLine(new string(' ', GetLastGapLength()));
            Window.DefaultColor();
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if(ActiveSlideBar != null)
            {
                ActiveSlideBar.HandleKey(info, api);
                return;
            }

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
                if(selectedObject < objects.Count - 2)
                {
                    selectedObject++;
                }
            }
            else if(info.Key == ConsoleKey.Enter)
            {
                if(selectedObject == 0) slideBars.Push(new LeftSlideBar(20));
            }
            //------------------------------------------------------------------------------------------------------------------------------------F9 key
            else if (info.Key == ConsoleKey.F9)
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
    }
}
