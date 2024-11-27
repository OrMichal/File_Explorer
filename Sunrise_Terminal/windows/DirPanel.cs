using Sunrise_Terminal.Core;
using Sunrise_Terminal.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.windows
{
    public class DirPanel : Window
    {
        public List<ListWindow> listWindows = new List<ListWindow>();
        public FooterMenu footerMenu = new FooterMenu(new List<Object>() {

                new Object(){name = "Help"},
                new Object(){name = "Menu"},
                new Object(){name = "Preview"},
                new Object(){name = "Edit"},
                new Object(){name = "Copy"},
                new Object(){name = "RenMov"},
                new Object(){name = "CrtDir"},
                new Object(){name = "Delete"},
                new Object(){name = "PullDn"},
                new Object(){name = "Quit"}
            });
        public DirPanel(int numberOfListWindows, API api)
        {
            for (int i = 0; i < numberOfListWindows; i++)
            {
                listWindows.Add(new ListWindow(api));
            }
            api.Application.SwitchWindow(listWindows[0]);
        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            int i = 0;
            foreach (var item in listWindows)
            {
                item.Draw(i * Settings.WindowWidth, api, listWindows.IndexOf(item) == api.ActiveWindowIndex);
                i++;
            }
            footerMenu.Draw();
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            listWindows[api.ActiveWindowIndex].HandleKey(info, api);
        }
    }
}
