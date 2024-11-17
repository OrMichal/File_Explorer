using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.MessageBoxes
{
    public class MenuMessageBox : Window, IMessageBox
    {
        new public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public bool active { get; set; }
        private int selected = 0;
        private int LocationX { get; set; }
        private int LocationY { get; set; }
        private List<string> operations { get; set; } = new List<string>();

        public bool WindowOnline { get; set; }
        public bool WindowActiveRequest { get; set; }

        public MenuMessageBox(int Height, int Width)
        {
            this.width = Width;
            this.height = Height;
            this.Heading = "Menu";
            LocationX = Console.WindowWidth / 2;
            LocationY = Console.WindowHeight / 2;
            using (StreamReader sr = new StreamReader($@"C:\Users\{Environment.UserName}\Desktop\Sunrise_Terminal\Sunrise_Terminal\MenuData.json"))
            {
                this.Description = sr.ReadToEnd();
            }
            string[] parted = this.Description.Split('\n');
            foreach (string part in parted)
            {
                operations.Add(part.Substring(0, part.Length - 1));
            }
        }

        public override void Draw(int LocationX, API api, bool _ = true)
        {
            graphics.DrawListBox(this.width, this.height, Console.WindowWidth / 2 - this.width / 2, Console.WindowHeight / 2 - this.height / 2, this.operations.ToList(), this.selected);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            HandleMBoxChange(info, api);

            if (info.Key == ConsoleKey.DownArrow && selected < operations.Count() - 1)
            {
                selected++;
            }
            else if (info.Key == ConsoleKey.UpArrow && selected > 0)
            {
                selected--;
            }
            else if (info.Key == ConsoleKey.Escape)
            {
                api.CloseActiveWindow();
            }
        }
    }
}
