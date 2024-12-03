using Sunrise_Terminal.Utilities;
using Sunrise_Terminal.windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace Sunrise_Terminal.Core
{
    public class Application
    {
        public Stack<Window> activeWindows = new Stack<Window>();
        public API Api { get; set; } = new API();
        private UltraFormatter Formatter = new UltraFormatter();
        public HeaderMenu headerMenu = new HeaderMenu();
        
        private ConsoleFormatter consoleFormatter = new ConsoleFormatter();
        public DirPanel DirPanel;
        public Settings Settings = new Settings();

        public Window activeWindow
        {
            get
            {
                return activeWindows.Peek();
            }
        }

        public Application(int numberOfWindows = 2, int Height = 50, int Width = 200)
        {
            Console.Title = "Sunrise Terminal";

            Settings.NumberOfWindows = numberOfWindows;
            Settings.WindowWidth = Console.WindowWidth / numberOfWindows;
            //Console.BufferHeight = Height;

            this.Api.Application = this;
            DirPanel = new DirPanel(numberOfWindows, this.Api);
            SwitchWindow(DirPanel);

            if (Height != null && Width != null) Console.SetWindowSize(Width, Height);
            Console.CursorVisible = false;
        }


        public void Draw()
        {
            Settings.WindowWidth = Console.WindowWidth / Settings.NumberOfWindows;
            consoleFormatter.FormatCheck();
            headerMenu.Draw(0, Api);

            if (DirPanel.listWindows.Contains(activeWindow))
            {
                DirPanel.Draw(0, Api);
            }
            else
            {
                activeWindow.Draw(activeWindow.LocationX, Api);
            }

        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            activeWindow.HandleKey(info, Api);
        }

        public void SwitchWindow(Window window)
        {
            activeWindows.Push(window);
        }
    }

}
