using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            }
        }

        public void MoveDown()
        {
            if (this.Movement.Data.Count < Settings.WindowDataLimit && Y < this.Movement.Data.Count - 1)
            {
                this.Y++;
            }

            if (this.Y <= this.Movement.Data.Count() - Settings.WindowDataLimit + Offset - 1)
            {
                this.Y++;
                if (this.Y >= Offset + Settings.WindowDataLimit - 2)
                {
                    this.Offset++;
                }
            }

            if (this.X >= this.Movement.Data[this.Y].ToString().Length)
            {
                X = this.Movement.Data[this.Y].ToString().Length - 1;
            }
        }

        public void MoveRight()
        {
            if (this.X < this.Movement.Data[Y].ToString().Length - 1)
            {
                X++;
            }
        }

        public void MoveLeft()
        {
            if (this.X > 0)
            {
                this.X--;
            }
        }
    }
}
