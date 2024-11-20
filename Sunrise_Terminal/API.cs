using Sunrise_Terminal.windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal
{
    public class API
    {
        public Application Application { get; set; }
        public Window activeWindow { get; set; }

        public event Action RefreshFilesEvent;


        public void RequestFilesRefresh()
        {
            RefreshFilesEvent?.Invoke();
        }

        public string GetSelectedFile()
        {
            return this.GetActiveListWindow().Rows[this.GetActiveListWindow().selectedIndex].Name;
        }

        public string GetActivePath()
        {
            return this.GetActiveListWindow().ActivePath;
        }

        public ListWindow GetActiveListWindow()
        {
            return this.Application.ListWindows[Application.ActiveWindowIndex];
        }

        public void CloseActiveWindow()
        {
            Application.activeWindows.Pop();
        }

        public void Erase(int width, int height, int LocationX, int LocationY)
        {
            for (int y = LocationY; y < height + LocationY; y++)
            {
                Console.SetCursorPosition(LocationX, y);
                Console.Write(new string(' ', width));
            }
        }
    }
}
