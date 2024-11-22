using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.Core;
using Sunrise_Terminal.interfaces;

namespace Sunrise_Terminal.MessageBoxes
{
    public class HelpMessageBox : Window, IMessageBox
    {
        new public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public bool active { get; set; }
        private int offset = 0;
        private int LocationX { get; set; }
        private int LocationY { get; set; }
        private string[] descParted {  get; set; }

        public bool WindowOnline { get; set; }
        public bool WindowActiveRequest { get; set; }
        private List<string> content = new List<string>();

        public HelpMessageBox(int Height, int Width)
        {
            this.width = Width;
            this.height = Height;
            this.Heading = "Helper";

            using (StreamReader sr = new StreamReader($@"../help.json"))
            {
                this.Description = sr.ReadToEnd();
            }
            descParted = Description.Split('\n');
            foreach (var desc in descParted)
            {
                content.Add(desc.Substring(0, desc.Length - 1));
            }
        }

        public override void Draw(int LocationX, API api, bool _ = true)
        {
            this.LocationX = Console.WindowWidth / 2 - this.width / 2 + api.Application.activeWindows.Count;
            this.LocationY = Console.WindowHeight / 2 - this.height / 2 + api.Application.activeWindows.Count;

            graphics.DrawTextBox(this.width,this.LocationX , this.LocationY , content);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            HandleMBoxChange(info, api);

            if(info.Key == ConsoleKey.DownArrow && offset < descParted.Count() - api.GetActiveListWindow().Limit)
            {
                offset++;
            }
            else if(info.Key == ConsoleKey.UpArrow && offset > 0)
            {
                offset--;
            }
            else if(info.Key == ConsoleKey.Escape)
            {
                api.Erase(this.width, this.height, this.LocationX, this.LocationY);
                api.CloseActiveWindow();
            }
        }
    }
}
