using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.Core;

namespace Sunrise_Terminal.UI
{
    public class WindowSizeHandler
    {
        public bool continueChecking = true;
        private int perviousWidth = Console.BufferWidth;
        private int perviousHeight = Console.BufferHeight;
        private Application app;
        public WindowSizeHandler(Application app)
        {
            this.app = app;
        }

        public async Task CheckWindowSize()
        {

            int currentWidth = Console.WindowWidth;
            int currentHeight = Console.WindowHeight;

            while (continueChecking)
            {
                if (currentHeight != perviousHeight || currentWidth != perviousWidth)
                {
                    perviousWidth = currentWidth;
                    perviousHeight = currentHeight;
                    app.Draw();
                }
            }
        }

        public void StopMonitoring()
        {
            continueChecking = false;
        }

        public void ContinueMonitoring()
        {
            continueChecking = true;
        }
    }
}
