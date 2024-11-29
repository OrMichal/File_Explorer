using Sunrise_Terminal.Core;
using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.DataHandlers;

namespace Sunrise_Terminal.FunctionMessageBoxes.EditMessageBox
{
    public class EditMBoxReplaceDialog : Window, IMessageBox
    {
        public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }

        private List<string> data;
        private List<Button> buttons;
        private List<TextBox> textBoxes;
        private int LocationX { get; set; }
        private int LocationY { get; set; }
        private int selectedX = 0;
        private int selectedButton = 0;
        private EditMessageBox ebox;
        private int selectedTextbox = 0;

        private string inputText 
        { get
            {
                return this.textboxesStrings[0];
            } 
        }
        private string inputTextReplace
        {
            get
            {
                return this.textboxesStrings[1];
            }
        }

        private List<string> textboxesStrings = new List<string>();
        public EditMBoxReplaceDialog(int height, int width, List<string> data, EditMessageBox ebox)
        {
            this.height = height;
            this.width = width;
            this.data = data;
            this.ebox = ebox;

            this.LocationX = Console.WindowWidth/2 - this.width/2;
            this.LocationY = Console.WindowHeight/2 - this.height/2;

            this.buttons = new List<Button>()
            {
                new(){Label = "Replace all"},
                new(){Label = "Cancel"}
            };

            this.textBoxes = new List<TextBox>()
            {
                new TextBox() {content = "Text to replace"},
                new TextBox() {content = "replacement"}
            };

            textBoxes.ForEach(x => this.textboxesStrings.Add(x.content));
        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            graphics.DrawSquare(this.width, this.height, this.LocationX, this.LocationY, "Replacer");
            graphics.DrawListBox(this.width - 2, this.height, this.LocationX + 1, this.LocationY + 3, this.textboxesStrings, selectedTextbox);
            graphics.DrawButtons(this.LocationX + this.width/3, this.LocationY + 7, this.buttons, this.selectedButton);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if(info.Key == ConsoleKey.Escape)
            {
                api.CloseActiveWindow();
            }
            else if(info.Key == ConsoleKey.LeftArrow)
            {
                if(selectedButton > 0)
                {
                    this.selectedButton--;
                }
            }
            else if(info.Key == ConsoleKey.RightArrow)
            {
                if(selectedButton < this.buttons.Count - 1)
                {
                    this.selectedButton++;
                }
            }
            else if(info.Key == ConsoleKey.Enter)
            {
                if(selectedButton == 0)
                {
                    ebox.editOperations.Replace(inputText, inputTextReplace);
                    api.CloseActiveWindow();
                }
            }
            else if (char.IsLetterOrDigit(info.KeyChar))
            {
                this.textboxesStrings[selectedTextbox] = new DataManagement().AddCharToText(this.textboxesStrings[selectedTextbox], info);
                
            }
            else if(info.Key == ConsoleKey.Backspace)
            {
                this.textboxesStrings[selectedTextbox] = new DataManagement().RemoveChar(this.textboxesStrings[selectedTextbox], this.textboxesStrings[selectedTextbox].Length - 1);
            }
            else if(info.Key == ConsoleKey.Tab)
            {
                selectedTextbox = (selectedTextbox + 1) % textBoxes.Count;
            }
        }
    }
}
