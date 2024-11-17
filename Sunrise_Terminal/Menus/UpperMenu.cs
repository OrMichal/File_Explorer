using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal
{
    public class UpperMenu : Window, IMenu
    {
        public List<Object> objects { get; set; } = new List<Object>();
        public int selectedObject { get; set; } = 0;
        public UpperMenu()
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
            selectedObject = 0;
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
            Console.WriteLine(new string(' ', GetLastGapLength()));
            Window.DefaultColor();
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
                if(selectedObject < objects.Count - 2)
                {
                    selectedObject++;
                }
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
