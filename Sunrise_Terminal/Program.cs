using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;

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

            for(; ; )
            {
                Parallel.For(0, 1, (Action) => {
                    app.Draw();
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo info = Console.ReadKey(true);
                        app.HandleKey(info);
                    }
                });
            }
        }
    }
}
