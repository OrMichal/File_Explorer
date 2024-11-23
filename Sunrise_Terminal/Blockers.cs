using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal
{
    public static class Blockers
    {
        public static void BlockCancelation()
        {
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
            };
        }

    }
}
