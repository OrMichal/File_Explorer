using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.Utilities;

namespace Sunrise_Terminal
{
    public class FooterMenu : IMenu
    {
        public List<Object> objects { get; set; }

        public FooterMenu()
        {
            objects = new List<Object>()
            {
                new Object(){name = "Help"},
                new Object(){name = "Menu"},
                new Object(){name = "Preview"},
                new Object(){name = "Edit"},
                new Object(){name = "Copy"},
                new Object(){name = "RenMov"},
                new Object(){name = "CrtDir"},
                new Object(){name = "Delete"},
                new Object(){name = "PullDn"},
                new Object(){name = "Quit"}
            };
        }
        public void Draw()
        {
            Console.SetCursorPosition(0, Console.WindowHeight- 2);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine($"{new string("Hint: To access administrator account press alt + f4!").PadRight(Console.WindowWidth)}");
            
            IMenu.DefaultColor();
            int num = 1;
            int location = 0;
            foreach (Object obj in objects)
            {
                Console.SetCursorPosition(location, Console.WindowHeight - 1);

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{num}");
                IMenu.DefaultColor();
                Console.Write(obj.name);
                Console.Write(new string(' ', Console.WindowWidth/10 - num.ToString().Length - obj.name.Length));
                num++;
                location += Console.WindowWidth/objects.Count;
            }
            Window.DefaultColor();
        }
    }
}
