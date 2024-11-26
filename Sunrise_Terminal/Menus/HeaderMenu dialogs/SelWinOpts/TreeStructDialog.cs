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
        public string Heading { get; set; }
        public string Description { get; set; }

        public int LocationX { get; set; }
        public int LocationY { get; set; }
        private DirectoryInfo source;
        public TreeStructDialog(string source)
        {
            this.source = new DirectoryInfo(source);
        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            graphics.DrawTreeStruct(0,0, 40, source, "Tree");
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if(info.Key == ConsoleKey.Escape)
            {
                api.CloseActiveWindow();
            }
        }
    }
}
