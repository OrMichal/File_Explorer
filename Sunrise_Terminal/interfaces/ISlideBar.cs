using Sunrise_Terminal.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.interfaces
{
    public interface ISlideBar
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int SelectedOperation { get; set; }
        public List<Operation> Operations { get; set; }

    }
}
