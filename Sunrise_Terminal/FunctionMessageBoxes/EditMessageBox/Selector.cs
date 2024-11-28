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
    public class Selector
    {
        public List<string> selectedText = new List<string>();
        public List<Point> selectedPoints = new List<Point>();
        public Selector()
        {
            
        }
        public void SelectLR(int start, int end, List<string> data, int row) 
        {
            string text = string.Empty; 
            for (int i = start; i <= end; i++)
            {
                Point currP = new Point(i, row);

                try
                {
                    text += data[row][i];
                    selectedPoints.Add(currP);
                }
                catch (Exception)
                {

                }
            }
            selectedText.Add(text);
        }

        public void SelectRL(int start, int end, List<string> data, int row)
        {
            string text = "";
            for (int i = end; i >= start; i--)
            {
                Point currP = new Point(i, row);

                try
                {
                    selectedPoints.Add(currP);
                    text += data[row][i];
                }
                catch
                {

                }
            }
            selectedText.Add(new Checkers().InvertString(text));
            text = "";
        }
    }
}
