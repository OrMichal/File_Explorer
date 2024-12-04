using Sunrise_Terminal.Core;
using Sunrise_Terminal.DataHandlers;
using Sunrise_Terminal.MessageBoxes;
using Sunrise_Terminal.objects;
using Sunrise_Terminal.Utilities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.FunctionMessageBoxes.EditMessageBox;
using System.Security.AccessControl;
namespace Sunrise_Terminal
{

    public class Window
    {
        public int LocationX { get; set; } = 0;
        protected Graphics graphics = new Graphics();
        protected Checkers Checkers = new Checkers();

        public virtual void Draw(int LocationX, API api, bool active = true)
        {
            
        }

        public virtual void HandleKey(ConsoleKeyInfo info, API api)
        {
            
        }

        protected void HandleMBoxChange(ConsoleKeyInfo info, API api)
        {
            if (info.Key == ConsoleKey.F1) api.Application.SwitchWindow(new HelpMessageBox(Settings.BigMessageBoxHeight, Settings.BigMessageBoxWidth));
            else if (info.Key == ConsoleKey.F2) api.Application.SwitchWindow(new MenuMessageBox(Settings.BigMessageBoxHeight, Settings.BigMessageBoxWidth));
            else if (info.Key == ConsoleKey.F3)
            {
                if (api.GetActiveListWindow().cursor.Y == 0)
                {
                    api.ThrowError("Nein! select a file");
                    return;
                }

                if (File.Exists(Path.Combine(api.GetActiveListWindow().ActivePath, api.GetSelectedFile())))
                {
                    api.Application.SwitchWindow(new PreviewMessageBox(Console.WindowWidth, Console.WindowHeight, api));
                }
                else
                {
                    api.GetActiveListWindow().ActivePath = new DataManagement().GoIn(api.GetActiveListWindow().ActivePath, api.GetSelectedFile().Substring(1));
                    api.RequestFilesRefresh();
                }

            }
            else if (info.Key == ConsoleKey.F4)
            {
                if (File.Exists(Path.Combine(api.GetActiveListWindow().ActivePath, api.GetSelectedFile())))
                {
                    api.Application.SwitchWindow(new EditMessageBox(Console.WindowWidth, Console.WindowHeight, api));
                }
                else
                {
                    api.ThrowError("Not a file");
                }

            }
            else if (info.Key == ConsoleKey.F5)
            {
                if (api.GetActiveListWindow().cursor.Y == 0)
                {
                    api.ThrowError("Nein! you cannot copy this");
                    return;
                }

                api.Application.SwitchWindow(new CopyMessageBox(Settings.MediumMessageBoxHeight, Settings.MediumMessageBoxWidth, api));
            }
            else if (info.Key == ConsoleKey.F6)
            {
                if (api.GetActiveListWindow().cursor.Y == 0)
                {
                    api.ThrowError("Nein! this cannot be moved");
                    return;
                }

                api.Application.SwitchWindow(new RenMovMessageBox(Settings.MediumMessageBoxHeight, Settings.MediumMessageBoxWidth, api));
            }
            else if (info.Key == ConsoleKey.F7) api.Application.SwitchWindow(new CrtDirMessageBox(Settings.MediumMessageBoxHeight, Settings.MediumMessageBoxWidth, api));
            else if (info.Key == ConsoleKey.F8)
            {
                if (api.GetActiveListWindow().cursor.Y == 0)
                {
                    api.ThrowError("NO!!! you cannot delete this");
                    return;
                }


                if (Checkers.IsFile(api.selectedFullPath))
                {
                    if (!Checkers.HasAccessFile(api.selectedFullPath))
                    {
                        api.ThrowError("Access Denied");
                        return;
                    }
                }


                api.Application.SwitchWindow(new DeletMessageBox(Settings.SmallMessageBoxHeight, Settings.SmallMessageBoxWidth));
            }
            else if (info.Key == ConsoleKey.F10) api.Application.SwitchWindow(new QuitMessageBox(Settings.SmallMessageBoxHeight, Settings.SmallMessageBoxWidth));
        }

        //---------------------------------------------------------------------------------------------------------------------------Přepínače barviček
        public static void DefaultColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkCyan;
        }
        public static void SelectionColor()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Blue;
        }

    }
}
