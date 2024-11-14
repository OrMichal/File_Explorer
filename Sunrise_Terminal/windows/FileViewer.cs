using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.windows
{
    public class FileViewer
    {
        int offset = 0;
        int Limit = Console.WindowHeight - 7;
        public void ViewFile(string Path)
        {
            using (StreamReader sr = new StreamReader(Path))
            {
                string[] parts = sr.ReadToEnd().Split('\n');
                for(int i = 0; i < Limit; i++)
                {
                    int actualIndex = 0;
                    if(i < parts.Count())
                    {
                        actualIndex = offset + i;

                    }
                }
            }
        }

        public void HandleKey(ConsoleKeyInfo info)
        {

        }
    }
}
