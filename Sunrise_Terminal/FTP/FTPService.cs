using Sunrise_Terminal.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Sunrise_Terminal.HelperPopUps;
using System.Reflection.Emit;

namespace Sunrise_Terminal.FTP
{
    public class FTPService
    {
        private string _hostAdress { get; set; }
        private API _api;
        private NetworkCredential _credentials;
        public FTPService(string ftpAdress, string UserID, string Password, API api)
        {
            this._hostAdress = ftpAdress;
            this._api = api;
            this._credentials = new NetworkCredential(UserID, Password);
        }

        public void CreateDirectory(string fileName)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(this._hostAdress);

            WebRequest request = WebRequest.Create(_hostAdress);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = this._credentials;

            using (var res = (FtpWebResponse)request.GetResponse())
            {
                _api.Application.SwitchWindow(new FTPResDialog(30, 20, res.StatusCode.ToString()));
            }
        }

    }
}
