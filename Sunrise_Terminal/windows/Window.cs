using Sunrise_Terminal.MessageBoxes;
using Sunrise_Terminal.objects;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal
{

    public class Window
    {
        protected Graphics graphics = new Graphics();

        public virtual void Draw(int LocationX, API api, bool active = true)
        {
            
        }

        public virtual void HandleKey(ConsoleKeyInfo info, API api)
        {
            
        }

        //---------------------------------------------------------------------------------------------------------------------------Přepínače barviček
        public static void DefaultColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkCyan;
        }
        public static void SelectionColor()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Blue;
        }

    }
}
