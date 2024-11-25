using System;
using System.Collections.Generic;
using System.Linq;
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


    }
}
