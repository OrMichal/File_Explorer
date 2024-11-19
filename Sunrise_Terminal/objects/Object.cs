using Sunrise_Terminal.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal
{
    public class Object
    {
        public bool active { get; set; } = false;
        public string name {  get; set; }
        public List<Operation> Operations { get; set; }
    }
}
