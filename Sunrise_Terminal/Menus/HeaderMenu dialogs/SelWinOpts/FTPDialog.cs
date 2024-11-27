using Sunrise_Terminal.Core;
using Sunrise_Terminal.FTP;
using Sunrise_Terminal.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.Menus.HeaderMenu_dialogs.SelWinOpts
{
    public class FTPDialog : Window, IMessageBox
    {
        public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }

        private int selectedAction = 0;
        private int selectedButton = 0;
        private FTPService ftpService;
        public FTPDialog(int height, int width, API api)
        {
            this.height = height;
            this.width = width;
            //this.ftpService = new FTPService(api);
        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            base.Draw(LocationX, api, active);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            base.HandleKey(info, api);
        }
    }
}
