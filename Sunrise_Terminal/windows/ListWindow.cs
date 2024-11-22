using Sunrise_Terminal.Core;
using Sunrise_Terminal.DataHandlers;
using Sunrise_Terminal.MessageBoxes;
using Sunrise_Terminal.UI;
using Sunrise_Terminal.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.windows
{
    public class ListWindow : Window
    {

        public int selectedIndex = 0;
        public int width = Console.WindowWidth / 2;
        public int Limit = Console.WindowHeight - 7;
        public int Offset = 0;
        public string ActivePath { get; set; }
        public int justOnce = 0;

        public List<Row> Rows = new List<Row>();
        private Table table = new Table();
        private DataManagement dataManagement = new DataManagement();

        public ListWindow()
        {
            ActivePath = dataManagement.StartingPath;
            Rows = new DataManagement().GetFiles(Rows, ActivePath);
        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            if (justOnce == 0)
            {
                api.RefreshFilesEvent += this.OnFileRefreshed;
                justOnce = 1;
            }

            table.width = Settings.WindowWidth - 2;
            table.Rows = this.Rows;
            table.ActivePath = this.ActivePath;
            table.selectedIndex = this.selectedIndex;
            table.Offset = this.Offset;
            table.Draw(LocationX, active);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if (info.Key == ConsoleKey.DownArrow)
            {
                if (this.selectedIndex <= this.Rows.Count() - 2)
                {
                    this.selectedIndex++;
                    if (this.selectedIndex >= this.Offset + Limit - 1)
                    {
                        this.Offset++;
                    }
                }
            }
            //---------------------------------------------------------------------------------------------------------------------------------Up Arrow key
            else if (info.Key == ConsoleKey.UpArrow)
            {
                if (this.selectedIndex > 0)
                {
                    this.selectedIndex--;
                    if (this.selectedIndex < this.Offset)
                    {
                        this.Offset--;
                    }
                }
            }
            //--------------------------------------------------------------------------------------------------------------------------------Tab key
            if (info.Key == ConsoleKey.Tab)
            {
                api.Application.ActiveWindowIndex = (api.Application.ActiveWindowIndex + 1) % api.Application.ListWindows.Count;
                api.Application.activeWindows.Pop();
                api.Application.SwitchWindow(api.Application.ListWindows[api.Application.ActiveWindowIndex]);

            }
            //--------------------------------------------------------------------------------------------------------------------------------Enter key
            if (info.Key == ConsoleKey.Enter)
            {

                if (api.GetActiveListWindow().selectedIndex == 0)
                {
                    api.GetActiveListWindow().ActivePath = dataManagement.GoBackByOne(api.GetActiveListWindow().ActivePath);
                }
                else
                {
                    api.GetActiveListWindow().ActivePath = dataManagement.GoIn(api.GetActiveListWindow().ActivePath, api.GetSelectedFile());
                }
                api.GetActiveListWindow().selectedIndex = 0;

                OnFileRefreshed();
                Offset = 0;
                selectedIndex = 0;
            }
            //--------------------------------------------------------------------------------------------------------------------------------F1 - F10 keys

            else if(info.Key >= ConsoleKey.F1 && info.Key <= ConsoleKey.F10)
            {
                HandleMBoxChange(info, api);
                
                if (info.Key == ConsoleKey.F9) api.Application.SwitchWindow(new HeaderMenu());
            }
            
        }

        private void OnFileRefreshed()
        {
            this.Rows = new DataManagement().GetFiles(this.Rows, this.ActivePath);
        }
    }
}
