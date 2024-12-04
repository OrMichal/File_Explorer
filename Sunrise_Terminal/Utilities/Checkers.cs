using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.Utilities
{
    public class Checkers
    {
        public bool StringContains(string MainText, string textItShouldContain)
        {
            if (string.IsNullOrEmpty(MainText) || string.IsNullOrEmpty(textItShouldContain))
                return false;

            if (MainText.Length < textItShouldContain.Length)
                return false;

            for (int i = 0; i <= MainText.Length - textItShouldContain.Length; i++)
            {
                int j;
                for (j = 0; j < textItShouldContain.Length; j++)
                {
                    if (MainText[i + j] != textItShouldContain[j])
                        break;
                }

                if (j == textItShouldContain.Length)
                    return true;
            }

            return false;
        }

        public string InvertString(string text)
        {
            return new string(text.Reverse().ToArray());
        }

        public bool HasAccessFile(string path)
        {
            try
            {
                using var fs = File.Open(path, FileMode.Open, FileAccess.ReadWrite);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        public bool HasAccessDirectory(string path)
        {
            try
            {


                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool IsFile(string path)
        {
            return File.Exists(path);
        }

        public bool IsDirectory(string path)
        {
            return Directory.Exists(path);
        }
    }
}
