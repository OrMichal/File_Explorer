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
            if (selectedPoints.Count == 0) return;
            var pointsByRow = selectedPoints.GroupBy(p => p.Y).OrderBy(g => g.Key);

            foreach (var group in pointsByRow)
            {
                int row = group.Key;
                var pointsInRow = group.OrderBy(p => p.X).ToList();

                int start = pointsInRow.First().X;
                int end = pointsInRow.Last().X;

                string currentLine = data[row];
                string prefix;
                string suffix;
                try
                {
                    prefix = currentLine.Substring(0, start);
                    suffix = currentLine.Substring(end + 1);

                }
                catch
                {
                    prefix = "";
                    suffix = "";
                }

                data[row] = prefix + " " + suffix;
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

        public void MoveSelection(List<string> data, List<string> selection, int targetX, int targetY)
        {
            foreach (var row in selection)
            {
                int index = data.IndexOf(row);
                if (index >= 0)
                    data[index] = data[index].Replace(row, new string(' ', row.Length));
            }

            foreach (var item in selection)
            {
                data.Insert(targetY, new string(" "));
            }

            for (int i = 0; i < selection.Count; i++)
            {
                int targetRow = targetY + i;
                if (targetRow >= data.Count) break;

                string selectedRow = selection[i];
                string currentRow = data[targetRow];

                string prefix = currentRow.Substring(0, Math.Min(targetX, currentRow.Length));
                string suffix = targetX + selectedRow.Length < currentRow.Length
                    ? currentRow.Substring(targetX + selectedRow.Length)
                    : "";

                data[targetRow] = prefix + selectedRow + suffix;
            }

            ClearSelection();
        }

        


        public void ClearSelection()
        {
            this.SelectedPoints.Clear();
            this.SelectedText.Clear();
        }
    }

}
