﻿using Sunrise_Terminal.Core;
using Sunrise_Terminal.DataHandlers;
using Sunrise_Terminal.FunctionMessageBoxes.EditMessageBox;
using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.HelperPopUps
{
    public class SearcherPopUp : Window, IMessageBox
    {
        public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        private EditMessageBox editBox;
        new private int LocationX;
        private int LocationY;
        private int selectedChar = 0;
        private int offset = 0;
        private DataManagement dataMan = new DataManagement();
        private bool insertion = false;
        private int selectedButton = 0;
        private List<Button> buttons;
        private string textToFind = "text to find";

        public SearcherPopUp(int width, int height, EditMessageBox editMBox, string heading = "")
        {
            this.width = width;
            this.height = height;
            this.editBox = editMBox;
            this.Heading = heading;

            this.LocationX = Console.WindowWidth / 2 - this.height / 2;
            this.LocationY = Console.WindowHeight / 2 - this.height / 2;

            buttons = new List<Button>
            {
                new Button(){ Label = "Yes" },
                new Button(){ Label = "No" }
            };
        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            graphics.DrawSquare(this.width, this.height, this.LocationX, this.LocationY, this.Heading);
            graphics.DrawListBox(this.width -2, this.height, this.LocationX + 1, this.LocationY + 1, new List<TextBox>() { new() { content = textToFind} }.Select(x => x.content));
            graphics.DrawButtons(this.LocationX + 1, this.LocationY + 4, this.buttons, this.selectedButton);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if(info.Key == ConsoleKey.Escape)
            {
                api.Erase(this.width, this.height, this.LocationX, this.LocationY);
                api.CloseActiveWindow();
            }
            else if (char.IsLetterOrDigit(info.KeyChar))
            {
                textToFind = dataMan.AddCharToText(textToFind, info);
            }
            else if(info.Key == ConsoleKey.Backspace)
            {
                this.textToFind = dataMan.RemoveChar(this.textToFind, this.textToFind.Length - 1);
            }
            else if(info.Key == ConsoleKey.Enter)
            {
                if(selectedButton == 0)
                {
                    editBox.cursor.LocateText(editBox.Rows, this.textToFind);
                    api.Erase(this.width, this.height, this.LocationX, this.LocationY);
                    api.CloseActiveWindow();
                }
                else if(selectedButton == 1)
                {
                    editBox.HighLightedText = "";
                    api.Erase(this.width, this.height, this.LocationX, this.LocationY);
                    api.CloseActiveWindow();
                }
            }
            else if(info.Key == ConsoleKey.RightArrow)
            {
                selectedButton = 1;
            }
            else if(info.Key == ConsoleKey.LeftArrow)
            {
                selectedButton = 0;
            }
            
        }
    }
}
