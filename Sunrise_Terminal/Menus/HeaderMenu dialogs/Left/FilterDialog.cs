using Sunrise_Terminal.Core;
using Sunrise_Terminal.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.Menus.HeaderMenu_dialogs.Left
{
    public class FilterDialog : Window, IMessageBox
    {
        public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }

        public FilterDialog(int width, int height, string heading)
        {
            this.width = width;
            this.height = height;
            this.Heading = heading;
        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            base.HandleKey(info, api);
        }
    }
}
