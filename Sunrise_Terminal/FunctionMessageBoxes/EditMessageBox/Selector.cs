using Sunrise_Terminal.DataHandlers;
using Sunrise_Terminal.objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Sunrise_Terminal.Utilities;

namespace Sunrise_Terminal.FunctionMessageBoxes.EditMessageBox
{
    using System.Text;

    public class Selector
    {
        public List<string> SelectedText { get; } = new List<string>();
        public List<Point> SelectedPoints { get; } = new List<Point>();
        public Selector() { }

        
        public void SelectLineDown(int start, int end, List<string> data, int row, int next)
        {
            if (row < 0 || row >= data.Count) return;
            string line = data[row];

            StringBuilder selectedTextBuilder = new StringBuilder();
            for (int i = start; i < end; i++)
            {
                selectedTextBuilder.Append(line[i]);
                SelectedPoints.Add(new Point(i, row));
            }


            for (int i = 0; i < start; i++)
            {
                selectedTextBuilder.Append(data[row + next]);
                SelectedPoints.Add(new Point(i, row + next));
            }

            if(next < 0)
            {
                SelectedText.Insert(0, selectedTextBuilder.ToString());
                return;
            }

            SelectedText.Add(selectedTextBuilder.ToString());
            
        }

        public void SelectSingle(List<string> data, int row, int c, bool reverse = false)
        {
            Point p = new Point(c, row);
            string charToAdd = data[row][c].ToString();

            if (reverse)
            {
                if (SelectedText.Count > 0)
                {
                    SelectedText[SelectedText.Count - 1] = charToAdd + SelectedText.Last();
                }
                else
                {
                    SelectedText.Insert(SelectedText.Count - 1,charToAdd);
                }
            }
            else
            {
                if (SelectedText.Count > 0)
                {
                    SelectedText[SelectedText.Count - 1] += charToAdd;
                }
                else
                {
                    SelectedText.Add(charToAdd);
                }
            }

            SelectedPoints.Add(p);
        }


        public void DeleteSelected(List<Point> selectedPoints, List<string> data)
        {
            var pointsByRow = selectedPoints.GroupBy(p => p.Y).OrderBy(g => g.Key);

            foreach (var group in pointsByRow)
            {
                var pointsInRow = group.OrderBy(p => p.X).ToList();

                string prefix;
                string suffix;
                try
                {
                    prefix = data[group.Key].Substring(0, pointsInRow.First().X);
                    suffix = data[group.Key].Substring(pointsInRow.Last().X + 1);

                }
                catch
                {
                    prefix = "";
                    suffix = "";
                }

                data[group.Key] = prefix + " " + suffix;
            }


            var rowsToDelete = selectedPoints
                .Select(p => p.Y)
                .Where(y => y != pointsByRow.First().Key && y != pointsByRow.Last().Key)
                .Distinct()
                .OrderByDescending(y => y)
                .ToList();

            rowsToDelete.ForEach(r => data.RemoveAt(r));
            ClearSelection();
        }

        public void MoveSelection(List<string> data, List<string> selection, int targetX, int targetY, List<Point> points)
        {
            for (int i = 0; i < selection.Count; i++)
            {
                int targetRow = targetY + i;


                string prefix = data[targetRow].Substring(0, Math.Min(targetX, data[targetRow].Length));
                string suffix;

                if(targetX + selection[i].Length < data[targetRow].Length)
                {
                    suffix = data[targetRow].Substring(targetX + selection[i].Length);
                }
                else
                {
                    suffix = "";
                }

                data.Insert(targetRow, prefix + selection[i] + suffix);
            }

            var grouped = points.Select(x => x.Y).Distinct().ToList();
            data.RemoveRange(grouped.First(), grouped.Count());
            ClearSelection();
        }

        


        public void ClearSelection()
        {
            this.SelectedPoints.Clear();
            this.SelectedText.Clear();
        }
    }

}
