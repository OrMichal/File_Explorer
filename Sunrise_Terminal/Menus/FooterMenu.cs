using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal
{
    public class FooterMenu : IMenu
    {
        public List<Object> objects { get; set; } = new List<Object>();

        public FooterMenu()
        {
            using (StreamReader sr = new StreamReader($@"C:\Users\{Environment.UserName}\Desktop\Sunrise_Terminal\Sunrise_Terminal\footerData.json"))
            {
                string text = sr.ReadToEnd();
                string[] parts = text.Split('\n');
                foreach (string part in parts)
                {
                    objects.Add(new Object()
                    {
                        name = part,
                    });
                }
            }
        }
        public void Draw()
        {
            IMenu.DefaultColor();
            int num = 1;
            int location = 0;
            foreach (Object obj in objects)
            {
                Console.SetCursorPosition(location, Settings.WindowDataLimit + 5);

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{num}");
                IMenu.DefaultColor();
                Console.Write(obj.name);
                num++;
                location += obj.name.Length + 1;
            }
            Window.DefaultColor();
        }
    }
}
