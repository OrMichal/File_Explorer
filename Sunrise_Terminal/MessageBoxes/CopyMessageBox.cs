using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal
{
    public class CopyMessageBox : Window, IMessageBox
    {
        public bool confirmation = false;
        public string Heading { get; set; }
        public int height { get; set; }
        new public int width { get; set; }
        public string Description { get; set; }
        public string srcPath { get; set; }
        public int destWindowIndex { get; set; } = 1;
        public string destPath { get; set; }
        public Window window { get; set; }
        public CopyMessageBox(int height, int width)
        {
            this.height = height;
            this.width = width;
            Heading = "Copy window";
            Description = "Are you sure?";
        }

        public override void Draw(int LocationX, API api, bool _ = true)
        {

            int i = 0;
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, i + Console.WindowHeight / 2);
            IMessageBox.DefaultColor();
            Console.Write($"┌─{Heading.PadRight(width + 2, '─')}─");
            Console.WriteLine("┐");

            for (i = 1; i < height - 1; i++)
            {
                if (i < height / 2)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, i + Console.WindowHeight / 2);
                    IMessageBox.DefaultColor();
                    Console.Write($"│ {new string("".PadRight(width + 2))} ");
                    Console.WriteLine("│");
                }

                if (i == (height / 2))
                {
                    IMessageBox.DefaultColor();
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, i + Console.WindowHeight / 2);
                    Console.WriteLine($"│  {Description.PadRight(width)}  │");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, i + Console.WindowHeight / 2 + 1);

                    IMessageBox.DefaultColor();
                    Console.Write($"│ ┌{new string("".PadRight(width / 3, '─'))}┐ ");
                    Console.Write($"┌{new string("".PadRight(width / 3, '─'))}┐ ");
                    Console.WriteLine($"{new string("").PadRight(width / 4)}│");

                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, i + Console.WindowHeight / 2 + 2);
                    IMessageBox.DefaultColor();
                    Console.Write($"│ │");

                    if (confirmation)
                    {
                        IMessageBox.SelectionColor();
                        Console.Write($"{new string("Yes").PadRight(width / 3)}");

                        IMessageBox.DefaultColor();
                        Console.Write($"│ │");
                        Console.Write($"{new string("No").PadRight(width / 3)}");

                        Console.WriteLine($"│ {new string("").PadRight(width / 4)}│");
                    }
                    else
                    {
                        Console.Write($"{new string("Yes").PadRight(width / 3)}");
                        Console.Write($"│ │");

                        IMessageBox.SelectionColor();
                        Console.Write($"{new string("No").PadRight(width / 3)}");
                        IMessageBox.DefaultColor();
                        Console.WriteLine($"│ {new string("").PadRight(width / 4)}│");
                    }


                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, i + Console.WindowHeight / 2 + 3);
                    IMessageBox.DefaultColor();
                    Console.Write($"│ └{new string("".PadRight(width / 3, '─'))}┘ └{new string("".PadRight(width / 3, '─'))}┘ ");
                    Console.WriteLine($"{new string("").PadRight(width / 4)}│");
                }
                else if (i > height / 2)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, i + Console.WindowHeight / 2 + 3);
                    IMessageBox.DefaultColor();
                    Console.Write($"│ {new string("".PadRight(width + 2))} ");
                    Console.WriteLine("│");
                }
            }
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, i + Console.WindowHeight / 2 + 3);
            IMessageBox.DefaultColor();
            Console.Write($"└─{new string("").PadRight(width + 2, '─')}─");
            Console.WriteLine("┘");

        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if(info.Key == ConsoleKey.Escape)
            {
                api.Application.SwitchWindow(api.Application.ListWindows[0]);
            }
            else if (info.Key == ConsoleKey.LeftArrow)
            {
                confirmation = true;
            }
            else if (info.Key == ConsoleKey.RightArrow)
            {
                confirmation = false;
            }
            else if(info.Key == ConsoleKey.Enter)
            {
                if (!confirmation)
                {
                    api.Application.SwitchWindow(api.Application.ListWindows[0]);
                }

                if (File.Exists(srcPath)) copyFile(srcPath, destPath);
                else copyDir(srcPath, destPath);
            }
        }

        public void copyFile(string sourceFilePath, string destinationFolderPath)
        {
            string fileName = Path.GetFileName(sourceFilePath);
            string destinationFilePath = Path.Combine(destinationFolderPath, fileName);

            using (FileStream sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
            using (FileStream destStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
            {
                sourceStream.CopyTo(destStream);
            }
        }


        private void copyDir(string sourcePath, string destinationPath)
        {
            DirectoryInfo src = new DirectoryInfo(sourcePath);
            DirectoryInfo dest = new DirectoryInfo(destinationPath);
            CopyDirRecurs(src, dest);
        }

        private void CopyDirRecurs(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                DirectoryInfo targetDir = target.CreateSubdirectory(dir.Name);
                CopyDirRecurs(dir, targetDir);
            }

            foreach (FileInfo item in source.GetFiles())
            {
                string destFile = Path.Combine(target.FullName, item.Name);
                using (FileStream sourceStream = item.OpenRead())
                using (FileStream destStream = new FileStream(destFile, FileMode.Create, FileAccess.Write))
                {
                    sourceStream.CopyTo(destStream);
                }
            }
        }
    }
}
