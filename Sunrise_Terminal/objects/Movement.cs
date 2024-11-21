using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.objects
{
    public class Movement
    {
        public void CursorUp()
        {

        }

        public int CursorDown(int cursorLocation, int offset, IEnumerable<dynamic> array)
        {
            if (array.Count() < Settings.WindowDataLimit && cursorLocation < array.Count() - 1)
            {
                cursorLocation++;
                return cursorLocation;
            }

            if (cursorLocation <= array.Count() - Settings.WindowDataLimit + offset - 1)
            {
                cursorLocation++;
                if (cursorLocation >= offset + Settings.WindowDataLimit - 2)
                {
                    offset++;
                }
            }
            return cursorLocation;
        }

        public void CursorLeft()
        {

        }

        public void CursorRight()
        {

        }
    }
}
