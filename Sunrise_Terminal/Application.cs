using Sunrise_Terminal.windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal
{
    public class Application
    {
        public Window activeWindow;
        public Stack<Window> activeWindows = new Stack<Window>();
        public List<ListWindow> ListWindows = new List<ListWindow>();
        public int ActiveWindowIndex { get; set; } = 0;
        public bool End { get; set; } = false;
        public API Api { get; set; }

        private Formatter Formatter = new Formatter();
        private UpperMenu headerMenu = new UpperMenu();
        private FooterMenu footerMenu = new FooterMenu();

        public Application(int Height = 50, int Width = 160, int numberOfWindows = 2)
        {
            activeWindow = new Window();

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
            headerMenu.Draw(0, this.Api);

            int i = 0;
            if (ListWindows.Contains(this.activeWindow))
            {
                foreach (ListWindow lw in ListWindows)
                {
                    lw.Draw(Settings.WindowWidth * i, this.Api, ActiveWindowIndex == ListWindows.IndexOf(lw));
                    i++;
                }
            }
            else
            {
                this.activeWindow.Draw(0, this.Api);
            }
            
            footerMenu.Draw();
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            activeWindow.HandleKey(info, this.Api);
        }

        public void SwitchWindow(Window window)
        {
            activeWindows.Push(window);
            if(activeWindow != window)
            {
                this.activeWindow = window;
            }
        }
    }

}
