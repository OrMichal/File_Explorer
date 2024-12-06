using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;
using Sunrise_Terminal.Core;
using Sunrise_Terminal.HelperPopUps;
using Sunrise_Terminal.Utilities;

namespace Sunrise_Terminal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Application app = new Application();
            Console.CursorVisible = false;
            Blockers.BlockCancelation();
            //new BootingPopUp(20, 4, "booting mc").Draw(0, app.Api);
            /*
            Console.Beep(440, 300);
            Console.Beep(440, 300);
            Console.Beep(440, 300);
            Console.Beep(349, 150);
            Console.Beep(523, 50);

            Console.Beep(440, 300);
            Console.Beep(349, 150);
            Console.Beep(523, 50);
            Console.Beep(440, 500);
            */

            for (;;)
            {
                try
                {
                    app.Draw();
                    ConsoleKeyInfo info = Console.ReadKey(true);

                    app.HandleKey(info);

                }
                catch (System.IO.IOException)
                {
                    app.Api.ThrowError("Nein, this is being used");
                }
            }
        }
    }
}
