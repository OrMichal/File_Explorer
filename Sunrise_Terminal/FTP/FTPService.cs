using Sunrise_Terminal.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.HelperPopUps;

namespace Sunrise_Terminal.FTP
{
    public class FTPService
    {
        private string _userID {  get; set; }
        private string _password { get; set; }
        private string _ftpAdress { get; set; }
        private API _api;
        public FTPService(string ftpAdress, string UserID, string Password, API api)
        {
            this._ftpAdress = ftpAdress;
            this._userID = UserID;
            this._password = Password;
            this._api = api;
        }

        public void CreateDirectory(string fileName)
        {
            WebRequest request = WebRequest.Create(this._ftpAdress + "//" +  fileName);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential(_userID, _password);

            using (var res = (FtpWebResponse)request.GetResponse())
            {
                _api.Application.SwitchWindow(new FTPResDialog(30, 20, res.StatusCode.ToString()));
            }
        }
    }
}
