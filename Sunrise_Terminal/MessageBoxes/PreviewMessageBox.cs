using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.Core;
using Sunrise_Terminal.DataHandlers;
using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.Utilities;

namespace Sunrise_Terminal.MessageBoxes
{
    public class PreviewMessageBox : Window, IMessageBox
    {
        public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public int Limit { get; set; }
        public Window activeWindow { get; set; }

        private List<string> DataParted { get; set; } = new List<string>();
        private int offset = 0;
        private DataManagement dataManager = new DataManagement();
        private UltraFormatter formatter = new UltraFormatter();

        public PreviewMessageBox(int Width, int Height, API api)
        {
            this.width = Width;
            this.height = Height;
            Heading = "Previewing: ";

            if(File.Exists(Path.Combine(api.GetActiveListWindow().ActivePath, api.GetSelectedFile())))
            {
                using (StreamReader sr = new StreamReader(Path.Combine(api.GetActiveListWindow().ActivePath, api.GetSelectedFile())))
                {
                    while (!sr.EndOfStream)
                    {
                        DataParted.Add(sr.ReadLine());
                    }
                }
            }
        }
        public override void Draw(int LocationX, API api, bool _ =  true)
        {
            if (Directory.Exists(api.GetActiveListWindow().ActivePath + api.GetSelectedFile()))
            {
                api.GetActiveListWindow().ActivePath = dataManager.GoIn(api.GetActiveListWindow().ActivePath, api.GetSelectedFile());
                api.RequestFilesRefresh();
                return;
            }
            

            IMessageBox.DefaultColor();
            int i = 0;
            Console.SetCursorPosition(0, 1);
            Console.WriteLine($"┌{formatter.DoublePadding(Heading + api.GetSelectedFile(), width - 2, '─')}┐");
            for (i = 0; i < api.GetActiveListWindow().Limit; i++)
            {
                int actualIndex = 0;
                if (i < DataParted.Count())
                {
                    actualIndex = offset + i;
                    Console.SetCursorPosition(0, i + 2);
                    Console.WriteLine($"│{(actualIndex + 1).ToString().PadRight(4)} {formatter.PadTrimRight(DataParted[actualIndex], width - 7)}│");
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
            HandleMBoxChange(info, api);

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
                api.CloseActiveWindow();
            }
        }
    }
}
