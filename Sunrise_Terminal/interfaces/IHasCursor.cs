using Sunrise_Terminal.objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.interfaces
{
    public interface IHasCursor<TArrayDataType>
    {
        Cursor<TArrayDataType> cursor { get; set; }
        int Offset { get; set; }
        List<TArrayDataType> Rows { get; set; }
    }
}
