using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.objects;
using Sunrise_Terminal.Utilities;

namespace Sunrise_Terminal.DataHandlers
{

    public class EditOperations
    {
        public Cursor<string> cursor;

        public EditOperations(Cursor<string> cursor)
        {
            this.cursor = cursor;  
        }

        public void EraseLastChar()
        {
            if (cursor.X == cursor.Movement.Data[cursor.Y].Length - 1)
            {
                cursor.X--;
            }

            cursor.Movement.Data[cursor.Y] = new DataManagement().RemoveChar(cursor.Movement.Data[cursor.Y], cursor.X);
        }

        public void RemoveRow()
        {
            if (!(cursor.Movement.Data.Count > 1))
            {
                return;
            }

            if(cursor.Y >= 1)
            {
                cursor.Movement.Data.RemoveAt(cursor.Y - 1);
            }

            if (cursor.Y > 0)
            {
                cursor.MoveUp();
            }
            cursor.X = 0;
        }

        public void InsertNewLine()
        {
            cursor.Movement.Data.Insert(cursor.Y, " ");
            cursor.Y++;
            cursor.X = 0;
            if (cursor.Y >= cursor.Offset + Settings.WindowDataLimit - 1)
            {
                cursor.Offset++;
            }
        }
    }
}
