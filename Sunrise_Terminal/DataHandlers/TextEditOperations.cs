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

    public class TextEditOperations
    {
        public Cursor<string> cursor;

        public TextEditOperations(Cursor<string> cursor)
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
            cursor.Movement.Data.Insert(cursor.Y + 1, " ");
            cursor.Y++;
            cursor.X = 0;
            if (cursor.Y >= cursor.Offset + Settings.WindowDataLimit - 1)
            {
                cursor.Offset++;
            }
        }

        public void PushDown()
        {

            if (cursor.Y < cursor.Movement.Data.Count - 1)
            {
                string dataToPlace = cursor.Movement.Data[cursor.Y].ToString();
                string dataWherePlace = cursor.Movement.Data[cursor.Y + 1].ToString();
                cursor.Movement.Data[cursor.Y + 1] = dataToPlace;
                cursor.Movement.Data[cursor.Y] = dataWherePlace;
                cursor.MoveDown();
            }
        }

        public void PushUp()
        {

            if (cursor.Y >= 1)
            {
                string dataToPlace = cursor.Movement.Data[cursor.Y].ToString();
                string dataWherePlace = cursor.Movement.Data[cursor.Y - 1].ToString();
                cursor.Movement.Data[cursor.Y - 1] = dataToPlace;
                cursor.Movement.Data[cursor.Y] = dataWherePlace;
                cursor.MoveUp();
            }
        }

        public void CopyCurrentLine()
        {
            if(cursor.Y != cursor.Movement.Data.Count - 1)
            {
                cursor.Movement.Data.Insert(cursor.Y + 1, cursor.Movement.Data[cursor.Y]);
                cursor.MoveDown();
            }
        }

        public void copy(out string text)
        {
            text = cursor.Movement.Data[cursor.Y].ToString();
        }

        public void Paste(string text)
        {
            cursor.Movement.Data[cursor.Y] += text;
        }
    }
}
