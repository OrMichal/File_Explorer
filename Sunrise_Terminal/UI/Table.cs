using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.DataHandlers;
using Sunrise_Terminal.Utilities;

namespace Sunrise_Terminal.UI
{
    public class Table
    {
        public int Limit { get; set; } = Settings.WindowDataLimit;
        public int width { get; set; }
        public int Offset { get; set; } = 0;
        public int selectedIndex { get; set; } = 0;
        public string ActivePath { get; set; }
        public string[] Headers = new string[] { "Name", "Size", "MTime" };
        private DataManagement dataMan = new DataManagement();
        private UltraFormatter formatter = new UltraFormatter();
        public List<Row> Rows { get; set; }

        public void Draw(int LocationX, bool Active)
        {
            int[] lengths = dataMan.GetLengths(Rows);

            int nameWidth = width / 2;
            int sizeWidth = width / 4;
            int timeWidth = width / 4;

            
                Console.SetCursorPosition(LocationX, 1);
                DirectoryInfo dir = new DirectoryInfo(ActivePath);
                Console.WriteLine($"┌{ActivePath.PadRight(width, '─')}┐");

                Console.SetCursorPosition(LocationX, 2);
                Console.WriteLine($"│{new string($"{formatter.DoublePadding(Headers[0], nameWidth)}│{formatter.DoublePadding(Headers[1], sizeWidth)}│{formatter.DoublePadding(Headers[2], timeWidth - 3)}").PadRight(width)}│");

                int i = 0;
                for (i = 0; i < Settings.WindowDataLimit; i++)
                {
                    int actualIndex = 0;
                    if (i < Rows.Count)
                    {
                        actualIndex = Offset + i;
                        
                            Row row = Rows[actualIndex];

                            Console.SetCursorPosition(LocationX, i + 3);
                            Console.Write("│");
                            if (actualIndex == selectedIndex && Active)
                            {
                                Settings.LWindowselectionColor();
                            }
                            if (!row.file && row.Size == 0)
                            {
                                row.Description = row.Size != 0 ? row.Size.ToString() : row.Description;
                                Console.Write($"{new string($"{formatter.PadTrimRight(row.Name, nameWidth)}│{formatter.DoublePadding(row.Description, sizeWidth)}│{formatter.DoublePadding(row.DateOfLastChange, timeWidth - 3)}").PadRight(width)}");

                            }
                            else if (!row.file)
                            {
                                Console.Write($"{new string($"{formatter.PadTrimRight("/" + row.Name, nameWidth)}│{formatter.PadTrimLeft(row.Size.ToString(), sizeWidth)}│{formatter.DoublePadding(row.DateOfLastChange, timeWidth - 3)}").PadRight(width)}");
                            }
                            else
                            {
                                Console.Write($"{new string($"{formatter.PadTrimRight(row.Name, nameWidth)}│{formatter.PadTrimLeft(row.Size.ToString(), sizeWidth)}│{formatter.DoublePadding(row.DateOfLastChange, timeWidth - 3)}").PadRight(width)}");
                            }
                            Settings.ListWindowColor();
                            Console.WriteLine("│");

                        
                    }
                    else
                    {
                        Console.SetCursorPosition(LocationX, i + 3);
                        Console.WriteLine($"│{new string($"{new string("".PadRight(nameWidth, ' '))}│{new string("".PadRight(sizeWidth, ' '))}│{new string("".PadRight(timeWidth - 4, ' '))}").PadRight(width)}│");
                    }
                }

                Console.SetCursorPosition(LocationX, i + 2);
                Console.WriteLine($"├{new string($"{new string("".PadRight(nameWidth, '─'))}┴{new string("".PadRight(sizeWidth, '─'))}┴{new string("".PadRight(timeWidth - 4, '─'))}").PadRight(width, '─')}┤");
                i++;
                Console.SetCursorPosition(LocationX, i + 2);
                Console.WriteLine($"│{formatter.PadTrimRight(Rows[selectedIndex].Name, width)}│");
                i++;
                Console.SetCursorPosition(LocationX, i + 2);
                Console.WriteLine($"└{new string("".PadRight(width, '─'))}┘");
                i++;
            
        }
    }
}
