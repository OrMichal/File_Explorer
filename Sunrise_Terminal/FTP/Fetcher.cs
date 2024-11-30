using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.FTP
{
    public class Fetcher
    {
        public static bool TryReachFTP(string userName,  string password, string ftpAdress)
        {
            try
            {
                FtpWebRequest req = (FtpWebRequest)WebRequest.Create(ftpAdress);
                req.Method = WebRequestMethods.Ftp.ListDirectory;

                req.Credentials = new NetworkCredential(userName, password);

                using (FtpWebResponse res = (FtpWebResponse)req.GetResponse())
                {
                    return true;
                }

            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                return false;
            }
        }
    }
}
