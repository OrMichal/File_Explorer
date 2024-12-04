using Sunrise_Terminal.windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.HelperPopUps;

namespace Sunrise_Terminal.Core
{
    public class API
    {
        public delegate void FilterDelegate(List<Row> rows);
        public Application Application { get; set; }
        public Window activeWindow { get; set; }

        public event Action RefreshFilesEvent;
        public int ActiveWindowIndex { get; set; } = 0;
        public FilterDelegate Filter { get; set; }
        public void RequestFilesRefresh()
        {
            RefreshFilesEvent?.Invoke();
        }
        public string selectedFullPath
        {
            get
            {
                return Path.Combine(this.GetActiveListWindow().ActivePath, this.GetSelectedFile());
            }
        }

        public string GetSelectedFile()
        {
            if (Directory.Exists(GetActiveListWindow().Rows[GetActiveListWindow().cursor.Y].Name.Substring(1)))
            {
                return GetActiveListWindow().Rows[GetActiveListWindow().cursor.Y].Name.Substring(1);
            }
            else
            {
                return GetActiveListWindow().Rows[GetActiveListWindow().cursor.Y].Name;
            }

        }

        public string GetActivePath()
        {
            return GetActiveListWindow().ActivePath;
        }

        public ListWindow GetActiveListWindow()
        {
            return Application.DirPanel.listWindows[this.ActiveWindowIndex];
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
            Application.SwitchWindow(new ErrMessageBox(message));
        }

        public void ReDrawDirPanel()
        {
            Application.DirPanel.Draw(0, this);
        }
    }
}
