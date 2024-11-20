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
        public int LocationX { get; set; } = 0;
        protected Graphics graphics = new Graphics();

        public virtual void Draw(int LocationX, API api, bool active = true)
        {
            
        }

        public virtual void HandleKey(ConsoleKeyInfo info, API api)
        {
            
        }

        protected void HandleMBoxChange(ConsoleKeyInfo info, API api)
        {
            if (info.Key == ConsoleKey.F1) api.Application.SwitchWindow(new HelpMessageBox(Settings.BigMessageBoxHeight, Settings.BigMessageBoxWidth));
            else if (info.Key == ConsoleKey.F2) api.Application.SwitchWindow(new MenuMessageBox(Settings.BigMessageBoxHeight, Settings.BigMessageBoxWidth));
            else if (info.Key == ConsoleKey.F3) api.Application.SwitchWindow(new PreviewMessageBox(Console.WindowWidth - 20, Console.WindowHeight - 20, api));
            else if (info.Key == ConsoleKey.F4) api.Application.SwitchWindow(new EditMessageBox(Console.WindowWidth - 10, Console.WindowHeight - 10, api));
            else if (info.Key == ConsoleKey.F5) api.Application.SwitchWindow(new CopyMessageBox(Settings.MediumMessageBoxHeight, Settings.MediumMessageBoxWidth, api));
            else if (info.Key == ConsoleKey.F6) api.Application.SwitchWindow(new RenMovMessageBox(Settings.MediumMessageBoxHeight, Settings.MediumMessageBoxWidth, api));
            else if (info.Key == ConsoleKey.F7) api.Application.SwitchWindow(new CrtDirMessageBox(Settings.MediumMessageBoxHeight, Settings.MediumMessageBoxWidth, api));
            else if (info.Key == ConsoleKey.F8) api.Application.SwitchWindow(new DeletMessageBox(Settings.SmallMessageBoxHeight, Settings.SmallMessageBoxWidth));
            else if (info.Key == ConsoleKey.F10) api.Application.SwitchWindow(new QuitMessageBox(Settings.SmallMessageBoxHeight, Settings.SmallMessageBoxWidth));
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
