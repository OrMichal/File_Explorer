using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;
using Sunrise_Terminal.Core;

namespace Sunrise_Terminal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Application app = new Application();
            API api = new API();
            app.Api = api;
            api.Application = app;
            api.activeWindow = app.activeWindow;
            Console.CursorVisible = false;
            for(;;)
            {

                app.Draw();
                ConsoleKeyInfo info = Console.ReadKey(true);
                app.HandleKey(info);
            }
        }
    }
}
