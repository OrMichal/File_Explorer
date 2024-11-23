using Sunrise_Terminal.Core;
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

        public DirPanel(int numberOfListWindows, API api)
        {
            for (int i = 0; i < numberOfListWindows; i++)
            {
                listWindows.Add(new ListWindow());
            }
            api.Application.SwitchWindow(listWindows[0]);
        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            int i = 0;
            foreach (var item in listWindows)
            {
                item.Draw(i * item.width, api, listWindows.IndexOf(item) == api.ActiveWindowIndex);
                i++;
            }
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            listWindows[api.ActiveWindowIndex].HandleKey(info, api);
        }
    }
}
