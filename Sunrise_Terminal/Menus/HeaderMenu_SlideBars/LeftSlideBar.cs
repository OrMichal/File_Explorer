using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.Menus.HeaderMenu_SlideBars
{
    public class LeftSlideBar : ISlideBar
    {
        public int Height { get; set; }
        public int Width { get; set; }  

        public int SelectedOperation { get; set; } = 0;
        public List<Operation> Operations { get; set; }
        private List<string> operationNames { get; set; }

        private Graphics graphics = new Graphics();


        public LeftSlideBar(int width)
        {
            this.Width = width;

            Operations = new List<Operation>()
            {
                new Operation()
                {
                    Name = "Tree"
                },

                new Operation()
                {
                    Name = "Filter"
                }
            };

            operationNames = Operations.Select(o => o.Name).ToList();
        }

        public void Draw(int LocationX)
        {
            graphics.DrawListBox(this.Width, this.Height, LocationX, 1, operationNames, SelectedOperation);
        }

        public void HandleKey(ConsoleKeyInfo info, API api)
        {
            if(info.Key == ConsoleKey.Escape)
            {
                api.CloseSlideBar();
            }
        }
    }
}
