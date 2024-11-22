using Sunrise_Terminal.windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.Core
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
            return GetActiveListWindow().Rows[GetActiveListWindow().selectedIndex].Name;
        }

        public string GetActivePath()
        {
            return GetActiveListWindow().ActivePath;
        }

        public ListWindow GetActiveListWindow()
        {
            return Application.ListWindows[Application.ActiveWindowIndex];
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

        public void ThrowError(string message)
        {
            Application.SwitchWindow(new InfoMessageBox(30, 7, message));
        }
    }
}
