using Sunrise_Terminal.Core;
using Sunrise_Terminal.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.Menus.HeaderMenu_dialogs.SelWinOpts
{
    public class TreeStructDialog : Window, IMessageBox
    {
        public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; } = "Tree";
        public string Description { get; set; }

        public int LocationX { get; set; }
        public int LocationY { get; set; }
        private DirectoryInfo source;
        private List<string> data = new List<string>();
        private int offset = 0;
        private bool notDirectory = false;
        public TreeStructDialog(string source)
        {
            this.source = new DirectoryInfo(source);
            GetDrawTree(this.source, 0);
        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            graphics.DrawView(Console.WindowWidth, "Tree structure", this.data, this.offset);
            Window.DefaultColor();
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            
            if(info.Key == ConsoleKey.Escape)
            {
                api.CloseActiveWindow();
                api.ReDrawDirPanel();
            }
            else if (info.Key == ConsoleKey.DownArrow && this.offset <= this.data.Count() - api.GetActiveListWindow().Limit - 1)
            {
                this.offset++;
            }
            else if (info.Key == ConsoleKey.UpArrow && this.offset > 0)
            {
                this.offset--;
            }
        }

        public void GetDrawTree(DirectoryInfo dirInfo, int level, string prefix = "")
        {
            data.Add($"{prefix}├─ {dirInfo.Name}");

            var subDirs = dirInfo.GetDirectories();
            var files = dirInfo.GetFiles();

            for (int i = 0; i < subDirs.Length; i++)
            {
                var isLastDir = (i == subDirs.Length - 1) && files.Length == 0;
                string newPrefix = prefix + (isLastDir ? "   " : "│  ");
                GetDrawTree(subDirs[i], level + 1, newPrefix);
            }

            for (int i = 0; i < files.Length; i++)
            {
                bool isLastFile = (i == files.Length - 1);
                string filePrefix = prefix + (isLastFile ? "└─ " : "├─ ");
                data.Add($"{filePrefix}{files[i].Name}");
            }
        }
    }
}
