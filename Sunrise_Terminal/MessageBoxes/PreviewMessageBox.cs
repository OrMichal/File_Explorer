using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.MessageBoxes
{
    public class PreviewMessageBox : Window, IMessageBox
    {
        public delegate void RenewFilesDelegate();
        public RenewFilesDelegate RenewFiles;
        public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public bool active { get; set; }
        public int Limit { get; set; }
        private string[] DataParted { get; set; }
        private int offset = 0;

        public Window activeWindow { get; set; }
        public bool WindowOnline { get; set; }
        public bool WindowActiveRequest { get; set; }
        public PreviewMessageBox(int Width, int Height)
        {
            this.width = Width;
            this.height = Height;
            Heading = "Previewing: ";
        }
        public override void Draw(int LocationX, API api, bool _ =  true)
        {
            try
            {
                if (new DirectoryInfo(Path.Combine(api.GetActiveListWindow().ActivePath,api.GetSelectedFile())).GetDirectories().Count() != 0)
                {
                    api.GetActiveListWindow().ActivePath = new DataManager().GoIn(api.GetActiveListWindow().ActivePath, api.GetSelectedFile());
                    api.RequestFilesRefresh();
                    return;
                }
            }
            catch (IOException)
            {

            }
            StreamReader sr = new StreamReader(Path.Combine(api.GetActiveListWindow().ActivePath, api.GetSelectedFile()));
            DataParted = sr.ReadToEnd().Split('\n'); 

            IMessageBox.DefaultColor();
            int i = 0;
            Console.SetCursorPosition(0, 1);
            Console.WriteLine($"┌{new Formatter().DoublePadding(Heading + api.GetSelectedFile(), width - 2, '─')}┐");
            for (i = 0; i < api.GetActiveListWindow().Limit; i++)
            {
                int actualIndex = 0;
                if (i < DataParted.Count())
                {
                    actualIndex = offset + i;
                    Console.SetCursorPosition(0, i + 2);
                    Console.WriteLine($"│{(actualIndex + 1).ToString().PadRight(4)} {new Formatter().PadTrimRight(DataParted[actualIndex], width - 7)}│");
                }
                else
                {
                    Console.SetCursorPosition(0, i + 2);
                    Console.WriteLine($"│{new string(' ',width - 2)}│");
                }
            }
            Console.SetCursorPosition(0, i + 2);
            Console.WriteLine($"└{new string('─', width - 2)}┘");
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if (info.Key == ConsoleKey.DownArrow && this.offset <= DataParted.Count() - api.GetActiveListWindow().Limit - 1)
            {
                offset++;
            }
            else if(info.Key == ConsoleKey.UpArrow &&  this.offset > 0)
            {
                offset--;
            }
            else if( info.Key == ConsoleKey.Escape)
            {
                api.Application.SwitchWindow(api.Application.ListWindows[0]);
                api.Application.activeWindows.Pop();
            }
        }
    }
}
