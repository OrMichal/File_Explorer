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
using Sunrise_Terminal.MessageBoxes;
using System.Runtime.CompilerServices;

namespace Sunrise_Terminal.FunctionMessageBoxes.EditMessageBox
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
        public TextEditOperations editOperations;
        private FooterMenu footerMenu;
        public string HighLightedText = "";
        private string textToCopy = "";


        private bool chgMade = false;
        private bool marking = false;
        private string selectedText = "";
        private bool left = false;

        private Selector selector = new Selector();

        private string selectedRowText
        {
            get
            {
                return Rows[cursor.Y] ?? 0.ToString();
            }
            
        }

        public EditMessageBox(int Width, int Height, API api)
        {
            editOperations = new TextEditOperations(cursor);
            cursor.Movement.Data = Rows;
            width = Width;
            height = Height;
            Heading = "Editation";

            using (StreamReader sr = new StreamReader(Path.Combine(api.GetActiveListWindow().ActivePath, api.GetSelectedFile())))
            {
                while (!sr.EndOfStream)
                {
                    Rows.Add(sr.ReadLine());
                }

                Rows.Add(" ");
            }
            
            if (Rows.Count == 0)
            {
                Rows.Add(" ");
            }

            for (int i = 0; i < Rows.Count; i++)
            {
                if (Rows[i].Length == 0)
                {
                    Rows[i] = " ";
                }
            }

            footerMenu = new FooterMenu(new List<Object>()
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
            });
        }

        public override void Draw(int LocationX, API api, bool _ = true)
        {
            graphics.DrawEditView(width, Heading, Rows, cursor.X, cursor.Y, cursor.Offset, selector.SelectedPoints);
            footerMenu.Draw();
        }



        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if (info.Key == ConsoleKey.F1)
            {
                api.Application.SwitchWindow(new HelpMessageBox(50, 50));
            }

            if (info.Key == ConsoleKey.DownArrow)
            {
                if(marking)
                {
                    if(cursor.Y == this.Rows.Count - 1)
                    {
                        return;
                    }

                    selector.SelectLineDown(this.cursor.X, this.Rows[cursor.Y].Length, this.Rows, this.cursor.Y, 1);
                }
                    cursor.MoveDown();
                

            }
            else if (info.Key == ConsoleKey.UpArrow)
            {
                if (marking)
                {
                    if(cursor.Y == 0)
                    {
                        return;
                    }

                    selector.SelectLineDown(cursor.X, this.Rows[cursor.Y].Length, this.Rows, cursor.Y, -1);
                }
                cursor.MoveUp();
                
            }
            else if (info.Key == ConsoleKey.RightArrow)
            {
                int oldX = cursor.X;
                left = false;
                cursor.MoveRight();

                if (marking && oldX != cursor.X)
                {
                    selector.SelectSingle(this.Rows, this.cursor.Y, this.cursor.X);
                }
            }
            else if (info.Key == ConsoleKey.LeftArrow)
            {
                int oldX = cursor.X;
                left = true;
                cursor.MoveLeft();

                if (marking && oldX != cursor.X)
                {
                    selector.SelectSingle(this.Rows, this.cursor.Y, cursor.X, true);
                }
            }
            else if (info.Key == ConsoleKey.PageUp)
            {
                cursor.PgUp();
            }
            else if (info.Key == ConsoleKey.PageDown)
            {
                cursor.PgDown();
            }
            else if (info.Key == ConsoleKey.NumPad6)
            {
                editOperations.PushUp();
                chgMade = true;
            }
            else if (info.Key == ConsoleKey.NumPad3)
            {
                editOperations.PushDown();
                chgMade = true;
            }
            else if ((char.IsLetterOrDigit(info.KeyChar) || char.IsWhiteSpace(info.KeyChar)) && info.Key != ConsoleKey.Enter)
            {
                chgMade = true;
                if (insertion)
                {
                    Rows[cursor.Y] = dataManager.AddCharToText_Insert(cursor.X, selectedRowText, info);
                    if (cursor.X < selectedRowText.Count() - 1)
                    {
                        cursor.X++;
                    }
                }
                else if (!insertion)
                {
                    Rows[cursor.Y] = dataManager.AddCharToText(selectedRowText, info);
                    cursor.X = Rows[cursor.Y].Count() - 1;

                }
            }
            else if (info.Key == ConsoleKey.Backspace)
            {
                chgMade = true;
                if (cursor.X == 0)
                {
                    editOperations.RemoveRow();
                }
                else
                {
                    editOperations.EraseLastChar();
                }
            }
            else if (info.Key == ConsoleKey.Enter)
            {
                editOperations.InsertNewLine();
            }
            else if (info.Key == ConsoleKey.F2)
            {
                dataManager.SaveChanges(api.GetActivePath(), api.GetSelectedFile(), Rows);
            }
            else if (info.Key == ConsoleKey.F10)
            {
                if(!chgMade)
                {
                    api.CloseActiveWindow();
                }
                else
                {
                    api.Application.SwitchWindow(new QuitEditMessageBox(50,10, this));
                }

            }
            else if (info.Key == ConsoleKey.Insert)
            {
                insertion = !insertion;
            }
            else if (info.Key == ConsoleKey.F7)
            {
                api.Application.SwitchWindow(new SearcherPopUp(20, 9, this, "Searcher"));
                Console.CursorVisible = false;
            }
            else if (info.Key == ConsoleKey.R && info.Modifiers.HasFlag(ConsoleModifiers.Control))
            {
                HighLightedText = string.Empty;
            }
            else if (info.Key == ConsoleKey.D && info.Modifiers.HasFlag(ConsoleModifiers.Control))
            {
                chgMade = true;
                editOperations.CopyCurrentLine();
            }
            else if (info.Key == ConsoleKey.F3)
            {
                marking = !marking;
                if (marking)
                {
                    selectedText += Rows[cursor.Y][cursor.X];
                    selector.SelectSingle(this.Rows, this.cursor.Y, this.cursor.X);
                }
                else if (marking == false)
                {
                    marking = false;
                    if (left) selectedText = new Checkers().InvertString(selectedText);
                }
            }
            else if(info.Key == ConsoleKey.F4)
            {
                api.Application.SwitchWindow(new EditMBoxReplaceDialog(16, 40, this.Rows, this));
            }
            else if (info.Key == ConsoleKey.Escape)
            {
                if (marking)
                {
                    selectedText = string.Empty;
                    marking = false;
                }

                selector.ClearSelection();
            }
            else if (info.Key == ConsoleKey.F5)
            {
                chgMade = true;
                /*
                Rows[cursor.Y] = selectedText;*/

                int i = 0;
                selector.SelectedText.ForEach(x =>
                {
                    this.Rows.Insert(cursor.Y + i, x);
                    i++;
                });
            }
            else if(info.Key == ConsoleKey.F6)
            {
                selector.MoveSelection(this.Rows, selector.SelectedText, cursor.X, cursor.Y);
            }
            else if(info.Key == ConsoleKey.F8)
            {
                selector.DeleteSelected(selector.SelectedPoints, this.Rows, " ");
            }
            else if(info.Key == ConsoleKey.F1)
            {
                api.Application.SwitchWindow(new EditMBoxHelper(40, 50));
                return;
            }

            if (info.Key == ConsoleKey.A && info.Modifiers.HasFlag(ConsoleModifiers.Shift))
            {
                throw new Exception(selectedText);
            }
        }

    }
}
