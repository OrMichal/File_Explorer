using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.DataHandlers
{
    public class DataManagement
    {
        public string StartingPath { get; } = $@"C:\users\{Environment.UserName}\desktop";

        public List<Row> GetFiles(List<Row> Rows, string path)
        {
            Rows.Clear();
            DirectoryInfo dir = new DirectoryInfo(path);
            Rows.Add(new Row
            {
                Description = "UP--DIR",
                file = false,
                Name = "/..",
                Size = Convert.ToInt64(null),
                DateOfLastChange = " "

            });
            try
            {
                int i = 0;
                foreach (var file in dir.GetDirectories())
                {
                    Rows.Add(new Row
                    {
                        file = false,
                        Name = file.Name,
                        DateOfLastChange = file.LastWriteTime.ToShortDateString()
                    });
                    i++;
                }

                foreach (var file in dir.GetFiles())
                {
                    Rows.Add(new Row
                    {
                        file = true,
                        Name = file.Name,
                        DateOfLastChange = file.LastWriteTime.ToShortDateString(),
                        Size = file.Length
                    });
                }
                return Rows;

            }
            catch (Exception)
            {

            }
            return Rows;
        }

        public int[] GetLengths(List<Row> rows)
        {
            int[] lens = new int[3];

            int nameMaxLen = 0;
            int sizeMaxLen = 0;
            int dateMaxLen = 0;

            for (int i = 0; i < rows.Count; i++)
            {
                if (rows[i].Name.Length > nameMaxLen)
                {
                    nameMaxLen = rows[i].Name.Length;
                }

                if (rows[i].Size.ToString().Length > sizeMaxLen)
                {
                    sizeMaxLen = rows[i].Size.ToString().Length;
                }

                if (rows[i].DateOfLastChange.ToString().Length > dateMaxLen)
                {
                    dateMaxLen = rows[i].DateOfLastChange.ToString().Length;
                }
            }

            lens[0] = Console.WindowWidth / 4;
            lens[1] = sizeMaxLen;
            if (sizeMaxLen < rows[0].Description.Length)
            {
                lens[1] = rows[0].Description.Length;
            }
            lens[2] = dateMaxLen;

            return lens;
        }

        private long GetDirSize(string path)
        {
            long size = new DirectoryInfo(path)
                .GetFiles()
                .Sum(files => files.Length);

            size += new DirectoryInfo(path)
                .GetDirectories()
                .Sum(dirs => GetDirSize(dirs.FullName));

            return size;
        }

        public string GoBackByOne(string path)
        {
            if (path == @"C:\")
            {
                return path;
            }

            string[] dirParted = path.Split('\\');
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (string item in dirParted)
            {
                if (i == 0)
                {
                    sb.Append(item);
                }
                else if (i != dirParted.Count() - 1)
                {
                    sb.Append(@"\" + item);
                }
                i++;
            }
            return sb.ToString();
        }
        public string GoIn(string path, string fileName)
        {
            string[] dirParted = path.Split('\\');
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (string item in dirParted)
            {
                if (!(i == dirParted.Count() - 1))
                {
                    sb.Append(item + @"\");
                }
                else
                {
                    sb.Append(item);
                }
                i++;
            }
            sb.Append("\\" + fileName);
            return sb.ToString();
        }

        public string AddCharToText(string text, ConsoleKeyInfo info)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                sb.Append(c);
            }
            sb.Append(info.KeyChar);


            return sb.ToString();
        }

        public string AddCharToText_Insert(int Location, string text, ConsoleKeyInfo info)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (char c in text)
            {
                if (i == Location)
                {
                    sb.Append(info.KeyChar);
                }
                else
                {
                    sb.Append(c);
                }
                i++;
            }

            return sb.ToString();
        }

        public string RemoveChar(string text, int removalLocation)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (char c in text)
            {
                if (i != removalLocation)
                {
                    sb.Append(c);
                }
                i++;
            }



            return sb.ToString();
        }

        public void CreateDir(string path, string name)
        {
            Directory.CreateDirectory(path + "\\" + name);
        }


        public void DeleteDir(string path, string name)
        {
            Directory.Delete(path + "\\" + name, true);
        }

        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public void SaveChanges(string path, string itemToPreview, List<string> DataParted)
        {
            using (StreamWriter sr = new StreamWriter(Path.Combine(path, itemToPreview)))
            {
                foreach (string item in DataParted)
                {
                    sr.WriteLine(item);
                }
            }
        }
    }
}
