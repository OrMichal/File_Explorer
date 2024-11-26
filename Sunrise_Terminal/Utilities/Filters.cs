using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.Utilities
{
    public class Filters
    {
        public static List<Row> FilterDesc(List<Row> rows)
        {
            var FilteredArray = rows.OrderDescending();
            return FilteredArray.ToList();
        }

        public static List<Row> FilterFiles(List<Row> rows)
        {
            var FilterArray = rows.Where(x => x.file ==  true);
            return FilterArray.ToList();
        }

        public static List<Row> FilterDirectories(List<Row> rows)
        {
            return rows.Where(x => x.file == false).ToList();
        }

        public static List<Row> FilterByOldest(List<Row> rows)
        {
            return rows.OrderBy(x => x.DateOfLastChange).ToList();  
        }
    }
}
