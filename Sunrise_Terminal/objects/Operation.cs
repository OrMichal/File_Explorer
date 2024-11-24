using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.objects
{
    public delegate Action Use();
    public class Operation
    {
        public string Name { get; set; } = "";
        public Use Use {  get; set; }
    }
}
