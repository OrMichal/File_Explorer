using Sunrise_Terminal.Core;
using Sunrise_Terminal.interfaces;
using Sunrise_Terminal.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.DataHandlers;
using Sunrise_Terminal.Menus.HeaderMenu_dialogs;
using System.Net;
using Sunrise_Terminal.Menus.HeaderMenu_dialogs.SelWinOpts;

namespace Sunrise_Terminal.FTP
{
    public class FTPLoginDialog : Window, IMessageBox
    {
        public int width { get; set; }
        public int height { get; set; }
        public string Heading { get; set; } = "FTP login";
        public string Description { get; set; }
        public string adressInput { get; set; } = "";
        public string username { get; set; } = "";
        public string password { get; set; } = "";
        private int LocationX { get; set; }
        private int LocationY { get; set; }

        private int selectedTxtbox = 0;
        private int selectedButton = 0;

        private List<TextBox> textBoxes = new List<TextBox>();
        private List<Button> buttons = new List<Button>();
        private string userName
        {
            get
            {
                return textBoxes[1].content;
            }
        }
        private string passWord
        {
            get
            {
                return textBoxes[2].content;
            }
        }
        private string ftpAdress
        {
            get
            {
                return textBoxes[0].content;
            }
        }
        private string passWordEncrypted
        {
            get
            {
                return new string('*', passWord.Length);
            }
        }
        List<string> textboxStr = new List<string>();
        public FTPLoginDialog(int width, int height)
        {
            this.width = width;
            this.height = height;


            this.LocationX = Console.WindowWidth / 2 - this.width / 2;
            this.LocationY = Console.WindowHeight / 2 - this.height / 2;

            textBoxes = new List<TextBox>()
            {
                new(){content = "FTP adress"},
                new(){content = "Username"},
                new(){content = "password"}
            };

            buttons = new List<Button>()
            {
                new(){Label = "Login"},
                new(){Label = "Cancel"}
            };

            textBoxes.ForEach(t => textboxStr.Add(t.content));
        }

        public override void Draw(int LocationX, API api, bool active = true)
        {
            graphics.DrawSquare(this.width, this.height, this.LocationX, this.LocationY, this.Heading);

            graphics.DrawListBox(this.width - 2, this.height, this.LocationX + 1, this.LocationY + 2, this.textboxStr, selectedTxtbox);
            graphics.DrawButtons(this.LocationX + this.width / 4, this.LocationY + this.textBoxes.Count * 2 + 1, this.buttons, this.selectedButton);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if(info.Key == ConsoleKey.LeftArrow)
            {
                selectedButton = 0;
            }
            else if(info.Key == ConsoleKey.RightArrow)
            {
                selectedButton = 1;
            }
            else if(info.Key == ConsoleKey.Tab)
            {
                selectedTxtbox = (selectedTxtbox + 1) % textBoxes.Count;
            }
            else if(info.Key == ConsoleKey.Backspace)
            {
                textboxStr[selectedTxtbox] = new DataManagement().RemoveChar(textboxStr[selectedTxtbox], textboxStr[selectedTxtbox].Length - 1);
            }
            else if(info.Key == ConsoleKey.Escape)
            {
                api.ReDrawDirPanel();
                api.CloseActiveWindow();
            }
            else if(info.Key == ConsoleKey.Enter)
            {
                if(selectedButton == 0)
                {
                    
                    if(!Fetcher.TryReachFTP(this.username, this.passWord, this.ftpAdress))
                    {
                        api.ThrowError("could not reach FTP server");
                        return;
                    }

                    api.CloseActiveWindow();
                    api.Application.SwitchWindow(new FTPDialog(30, 30,"FTP", this.passWord, this.username, this.ftpAdress, api));
                    api.ReDrawDirPanel();
                    
                }
                else if(selectedButton == 1)
                {
                    api.ReDrawDirPanel();
                    api.CloseActiveWindow();
                }
            }
            else if (!char.IsControl(info.KeyChar))
            {
                if(selectedTxtbox == 2)
                {
                    textBoxes[selectedTxtbox].content = new DataManagement().AddCharToText(textBoxes[selectedTxtbox].content, info);
                    textboxStr[selectedTxtbox] += '*';
                    return;
                }

                textboxStr[selectedTxtbox] = new DataManagement().AddCharToText(textboxStr[selectedTxtbox], info);
            }
            else if(info.Key == ConsoleKey.A && info.Modifiers.HasFlag(ConsoleModifiers.Alt))
            {
                throw new Exception(passWord);
            }
        }
    }
}
