//Generator-tool for Xamarin Forms(c) 2020 Plain Concepts

using System;
using System.IO;
using System.Linq;

namespace CreateNewProject.Helpers
{
    public static class FileHelper
    {
        public static void ReplaceContent(string file,string oldText, string newText)
        {
            try
            {
                string text = File.ReadAllText(file);
                text = text.Replace(oldText, newText);
                File.WriteAllText(file, text);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{nameof(FileHelper)}");
                Console.WriteLine(e.Message);
            }
        }

        public static void RenameFile(string file,string oldText, string newText)
        {
            try
            {
                var fileName = Path.GetFileName(file);
                if (fileName.Contains(oldText))
                {
                    var newFile = GetNameForNewFile(file, oldText, newText);
                    File.Move(file, newFile);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{nameof(FileHelper)}");
                Console.WriteLine(e.Message);
            }
        }

        private static string GetNameForNewFile(string file, string oldText, string newText)
        {
            var path = file.Split(Program.SEPATATOR);
            var fileName = path.Last();
            path[path.Length - 1] = fileName.Replace(oldText, newText);
            return string.Join(Program.SEPATATOR, path);
        }
    }
}
