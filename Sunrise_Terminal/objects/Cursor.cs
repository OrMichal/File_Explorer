using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.objects
{
    public class Cursor<TArrayDataType>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Offset { get; set; }
        public Movement<TArrayDataType> Movement { get; set; } = new Movement<TArrayDataType>();
        private int lastOkX = 0;
        public Cursor()
        {
            this.X = 0;
            this.Y = 0;
            this.Offset = 0;
        }

        public void MoveUp()
        {
            if(this.Y > 0)
            {
                this.Y--;
                if(this.Y < this.Offset)
                {
                    this.Offset--;
                }

                if (this.X >= this.Movement.Data[this.Y].ToString().Length)
                {
                    this.X = this.Movement.Data[this.Y].ToString().Length - 1;
                }
                else
                {
                    this.X = this.lastOkX;
                }
            }
        }

        public void MoveDown()
        {
            if (this.Movement.Data.Count <= Settings.WindowDataLimit && this.Y < this.Movement.Data.Count - 1)
            {
                this.Y++;
                return;
            }

            if (this.Y < this.Movement.Data.Count - 1)
            {
                this.Y++;

                if (this.Y >= Offset + Settings.WindowDataLimit)
                {
                    this.Offset++;
                }
            }

            if (this.X >= this.Movement.Data[this.Y].ToString().Length)
            {
                this.X = this.Movement.Data[this.Y].ToString().Length - 1;
            }
            else
            {
                this.X = this.lastOkX;
            }
        }


        public void MoveRight()
        {
            if (this.X < this.Movement.Data[Y].ToString().Length - 1)
            {
                this.X++;
                lastOkX++;
            }
        }

        public void MoveLeft()
        {
            if (this.X > 0)
            {
                this.X--;
                lastOkX--;
            }
        }

        public void PgUp()
        {
            if(this.Offset > Settings.WindowDataLimit)
            {
                this.Offset -= Settings.WindowDataLimit;
                this.Y = 0;
            }
            else
            {
                this.Offset = 0;
                this.Y = 0;
            }
        }

        public void PgDown()
        {
            if (this.Offset + Settings.WindowDataLimit < this.Movement.Data.Count)
            {
                this.Offset += Settings.WindowDataLimit;
                this.Y = Math.Min(this.Y + Settings.WindowDataLimit, this.Movement.Data.Count - 1);
            }
            else
            {
                int remainingLines = this.Movement.Data.Count - this.Offset;
                this.Offset = this.Movement.Data.Count - Settings.WindowDataLimit;
                this.Y = Math.Min(this.Offset + remainingLines - 1, this.Movement.Data.Count - 1);
            }
        }

        public void LocateText(List<string> data, string searchText)
        {
            int currentRow = this.Y;
            int closestDistance = int.MaxValue;
            int targetRow = 0;

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Contains(searchText))
                {
                    int distance = Math.Abs(i - currentRow);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        targetRow = i;
                    }
                }
            }

            if (targetRow != -1)
            {
                this.Y = targetRow;
                this.X = data[targetRow].IndexOf(searchText);
            }

            if(this.Y >= this.Offset)
            {
                this.Offset += this.Y;
            }
            else if(this.Y < this.Offset)
            {
                this.Offset = this.Y; 
            }
        }

    }
}
