//Generator-tool for Xamarin Forms(c) 2020 Plain Concepts.

using System;
using System.IO;
using System.Linq;

namespace CreateNewProject.Helpers
{
    public static class DirectoryHelper
    {
        public static void RenameDirectory(string directory, string oldText, string newText)
        {
            try
            {
                var newDirectory = GetNameForNewDirectory(directory,oldText, newText);
                if (!newDirectory.Equals(directory))
                {
                    Directory.Move(directory, newDirectory);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{nameof(DirectoryHelper)}");
                Console.WriteLine(e.Message);
            }
        }

        private static string GetNameForNewDirectory(string directory, string oldText, string newText)
        {
            var path = directory.Split(Program.SEPATATOR);
            var fileName = path.Last();
            path[path.Length - 1] = fileName.Replace(oldText, newText);
            return string.Join(Program.SEPATATOR, path);
        }

        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (Directory.Exists(destDirName))
            {
                Directory.Delete(destDirName, true);
            }

            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
