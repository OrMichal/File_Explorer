using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.Core;
using Sunrise_Terminal.DataHandlers;
using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.objects;
using Sunrise_Terminal.Utilities;
using Sunrise_Terminal.HelperPopUps;
using System.Drawing;

namespace Sunrise_Terminal.MessageBoxes
{
    public class EditMessageBox : Window, IMessageBox, IHasCursor<string>
    {
        public delegate void RenewFile();
        public RenewFile RenewFileDelegate;

        new public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public int Offset { get; set; }
        public List<string> Rows { get; set; } = new List<string>();
        public List<char> selectedChars = new List<char>();
        public int Limit { get; set; }


        public StreamReader SReader;
        public StreamWriter SWriter;
        private bool insertion = false;
        private DataManagement dataManager = new DataManagement();
        public Cursor<string> cursor { get; set; } = new Cursor<string>();
        private TextEditOperations editOperations;
        public string HighLightedText = "";
        private string textToCopy = "";

        private bool marking = false;
        private string selectedText = "";
        private bool left = false;

        private List<Point> selectionLocation = new List<Point>();

        private string selectedRowText
        {
            get
            {
                return this.Rows[cursor.Y] ?? (0).ToString();
            }

        }

        public EditMessageBox(int Width, int Height, API api)
        {
            editOperations = new TextEditOperations(this.cursor);
            cursor.Movement.Data = this.Rows;
            this.width = Width;
            this.height = Height;
            Heading = "Editation";
            
            using (StreamReader sr = new StreamReader(Path.Combine(api.GetActiveListWindow().ActivePath, api.GetSelectedFile())))
            {
                while (!sr.EndOfStream)
                {
                    Rows.Add(sr.ReadLine());
                }

                Rows.Add(" ");
            }

            if(Rows.Count == 0)
            {
                Rows.Add(" ");
            }

            for (int i = 0; i < Rows.Count; i++)
            {
                if(Rows[i].Length == 0)
                {
                    Rows[i] = " ";
                }
            }
        }

        public override void Draw(int LocationX, API api, bool _ = true)
        {
            graphics.DrawEditView(this.width, this.Heading, Rows, cursor.X, cursor.Y, cursor.Offset, this.HighLightedText, this.selectionLocation);
            new FooterMenu(new List<Object>() 
            { 
                new Object() { name = "Help"}, 
                new Object() { name = "Save"}, 
                new Object() { name = "Mark"}, 
                new Object() { name = "Replace"}, 
                new Object() { name = "Copy"}, 
                new Object() { name = "Move"}, 
                new Object() { name = "Search"}, 
                new Object() { name = "Delete"}, 
                new Object() { name = "PullDn"}, 
                new Object() { name = "Quit"} 
            }).Draw();
        }

        

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if(info.Key == ConsoleKey.F1)
            {
                api.Application.SwitchWindow(new HelpMessageBox(50, 50));
            }

            if (info.Key == ConsoleKey.DownArrow)
            {
                cursor.MoveDown();
            }
            else if (info.Key == ConsoleKey.UpArrow)
            {
                cursor.MoveUp();
            }
            else if(info.Key == ConsoleKey.RightArrow)
            {
                left = false;
                cursor.MoveRight();

                if (marking)
                {
                    selectedText += Rows[this.cursor.Y][cursor.X];
                    selectionLocation.Add(new Point(this.cursor.X, this.cursor.Y));
                }
            }
            else if(info.Key == ConsoleKey.LeftArrow)
            {
                left = true;
                cursor.MoveLeft();

                if (marking)
                {
                    selectedText += Rows[this.cursor.Y][cursor.X];
                    selectionLocation.Add(new Point(this.cursor.X, this.cursor.Y));
                }
            }
            else if(info.Key == ConsoleKey.PageUp)
            {
                cursor.PgUp();
            }
            else if(info.Key == ConsoleKey.PageDown)
            {
                cursor.PgDown();
            }
            else if(info.Key == ConsoleKey.NumPad6)
            {
                editOperations.PushUp();
            }
            else if(info.Key == ConsoleKey.NumPad3)
            {
                editOperations.PushDown();
            }
            else if((char.IsLetterOrDigit(info.KeyChar) || char.IsWhiteSpace(info.KeyChar)) && info.Key != ConsoleKey.Enter)
            {
                if (insertion)
                {
                    Rows[cursor.Y] = dataManager.AddCharToText_Insert(cursor.X, selectedRowText, info);
                    if(cursor.X < this.selectedRowText.Count() - 1)
                    {
                        cursor.X++;
                    }
                }
                else if (!insertion)
                {
                    Rows[cursor.Y] = dataManager.AddCharToText(selectedRowText, info);
                    cursor.X = this.Rows[this.cursor.Y].Count() - 1;

                }
            }
            else if(info.Key == ConsoleKey.Backspace)
            {

                if(cursor.X == 0)
                {
                    editOperations.RemoveRow();
                }
                else
                {
                    editOperations.EraseLastChar();
                }
            }
            else if(info.Key == ConsoleKey.Enter)
            {
                editOperations.InsertNewLine();
            }
            else if(info.Key == ConsoleKey.F2)
            {
                dataManager.SaveChanges(api.GetActivePath(), api.GetSelectedFile(), this.Rows);
            }
            else if( info.Key == ConsoleKey.F10)
            {
                api.CloseActiveWindow();
            }
            else if(info.Key == ConsoleKey.Insert)
            {
                insertion = !insertion;
            }
            else if(info.Key == ConsoleKey.F7)
            {
                api.Application.SwitchWindow(new SearcherPopUp(20,9, this, "Searcher"));
                Console.CursorVisible = false;
            }
            else if(info.Key == ConsoleKey.R && info.Modifiers.HasFlag(ConsoleModifiers.Control))
            {
                this.HighLightedText = string.Empty;
            }
            else if(info.Key == ConsoleKey.D && info.Modifiers.HasFlag(ConsoleModifiers.Control))
            {
                editOperations.CopyCurrentLine();
            }
            else if(info.Key == ConsoleKey.F3)
            {
                marking = !marking;

                if (marking)
                {
                    selectedText += Rows[cursor.Y][cursor.X];
                    selectionLocation.Add(new Point(this.cursor.X, this.cursor.Y));
                }
                else if(marking == false)
                {
                    marking = false;
                    if(left) selectedText = editOperations.InvertString(selectedText);
                }
            }
            else if(info.Key == ConsoleKey.Escape)
            {
                if (marking)
                {
                    this.selectedText = string.Empty;
                    marking = false;
                }

                selectionLocation.Clear();
            }
            else if(info.Key == ConsoleKey.F5)
            {
                Rows[cursor.Y] = selectedText;
            }

            if (info.Key == ConsoleKey.A && info.Modifiers.HasFlag(ConsoleModifiers.Shift)) {
                throw new Exception(this.selectedText);
            }
        }

    }
}
