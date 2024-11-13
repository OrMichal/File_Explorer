using Sunrise_Terminal.windows;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int width { get; set; }
        public string Description { get; set; }
        public string srcPath { get; set; }
        public int destWindowIndex { get; set; } = 1;
        public string destPath { get; set; }
        public Window window { get; set; }
        private int selectedPath = 0;
        private List<string> paths = new List<string>();
        private int LocationX {  get; set; }
        private int LocationY { get; set; }

        public int a = 0;
        public CopyMessageBox(int height, int width, API api)
        {
            this.height = height;
            this.width = width;
            Heading = "Copy window";
            Description = "Choose the destination path";
            this.LocationX = Console.WindowWidth/2 - this.width/2 - 1;
            this.LocationY = Console.WindowHeight / 2 - this.height/2 - 1;

            foreach (ListWindow lw in api.Application.ListWindows)
            {
                paths.Add(lw.ActivePath);
            }
        }

        public override void Draw(int LocationX, API api, bool _ = true)
        {
            if (a == 0)
            {
                graphics.DrawSquare(this.width, this.height, this.LocationX, LocationY, Heading);
                a++;
            }

            graphics.DrawListBox(this.width - 2, api.Application.ListWindows.Count + 2, this.LocationX + 1, this.LocationY + 1, this.paths, selectedPath);
            graphics.DrawLabel(this.LocationX, this.LocationY + 2 + api.Application.ListWindows.Count + 2, Description, 2);
        }

        public override void HandleKey(ConsoleKeyInfo info, API api)
        {
            if(info.Key == ConsoleKey.Escape)
            {
                api.Application.SwitchWindow(api.Application.ListWindows[0]);
            }
            else if (info.Key == ConsoleKey.UpArrow)
            {
                if (selectedPath > 0)
                {
                    selectedPath--;
                }
            }
            else if (info.Key == ConsoleKey.DownArrow)
            {
                if (selectedPath < paths.Count - 1)
                {
                    selectedPath++;
                }
            }
            else if(info.Key == ConsoleKey.Enter)
            {
                if (File.Exists(Path.Combine(api.GetActiveListWindow().ActivePath,api.GetSelectedFile()))) copyFile(Path.Combine(api.GetActivePath(), api.GetSelectedFile()), api.Application.ListWindows[this.selectedPath].ActivePath);
                else copyDir(Path.Combine(api.GetActivePath(), api.GetSelectedFile()), api.Application.ListWindows[this.selectedPath].ActivePath);
                
                api.Application.SwitchWindow(api.Application.ListWindows[0]);
                api.Application.activeWindows.Pop();
                api.RequestFilesRefresh();
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
            Directory.CreateDirectory(Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
            DirectoryInfo src = new DirectoryInfo(sourcePath);
            DirectoryInfo dest = new DirectoryInfo(Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
            CopyDirRecurs(src, dest);
        }

        private void CopyDirRecurs(DirectoryInfo source, DirectoryInfo target)
        {
            try
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
            catch
            {

            }
        }
    }
}
