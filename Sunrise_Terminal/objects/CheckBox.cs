using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.objects
{
    public class CheckBox
    {
        public string Text { get; set; } = "";
        private char[] options = new char[2] {' ', 'x' };
        private int checkIndex = 0;
        public char activeChoice
        {
            get
            {
                return options[checkIndex];
            }
        }

        public CheckBox()
        {

        }

        public void check()
        {
            this.checkIndex = (checkIndex + 1) % options.Length;
        }
    }
}
