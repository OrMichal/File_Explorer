using Sunrise_Terminal.Utilities;
using Sunrise_Terminal.windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace Sunrise_Terminal.Core
{
    public class Application
    {
        public Stack<Window> activeWindows = new Stack<Window>();
        public List<ListWindow> ListWindows = new List<ListWindow>();
        public int ActiveWindowIndex { get; set; } = 0;
        public API Api { get; set; }

        private Formatter Formatter = new Formatter();
        public HeaderMenu headerMenu = new HeaderMenu();
        private FooterMenu footerMenu = new FooterMenu();

        public Window activeWindow
        {
            get
            {
                return activeWindows.Peek();
            }
        }

        public Application(int Height = 50, int Width = 160, int numberOfWindows = 2)
        {
            Settings.NumberOfWindows = numberOfWindows;
            SwitchWindow(new Window());
            headerMenu.slideBar = activeWindows.Peek();

            if (Height != null && Width != null) Console.SetWindowSize(Width, Height);
            Console.CursorVisible = false;

            for (int i = 0; i < numberOfWindows; i++)
            {
                ListWindows.Add(new ListWindow());
            }
            SwitchWindow(ListWindows[0]);
        }


        public void Draw()
        {
            Formatter.ConsoleFormatCheck();
            headerMenu.Draw(0, Api);

            if (ListWindows.Contains(activeWindow))
            {
                int i = 0;
                foreach (ListWindow lw in ListWindows)
                {
                    lw.Draw(Settings.WindowWidth * i, Api, ActiveWindowIndex == ListWindows.IndexOf(lw));
                    i++;
                }

            }
            else
            {
                activeWindow.Draw(activeWindow.LocationX, Api);

            }



            footerMenu.Draw();
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
