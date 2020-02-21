//Generator-tool for Xamarin Forms(c) 2020 Plain Concepts

using CreateNewProject.Helpers;
using System;
using System.IO;

namespace CreateNewProject
{
    class Program
    {
        //Replace 
        public const string SEPATATOR = "\\";
        public const string BaseProject = "FirstDays";

        public const string _pathBaseProject = "./BaseProject";

        static void Main(string[] args)
        {
            string newProjectName;
            if (args.Length == 0 
                || string.IsNullOrWhiteSpace(args[0]))
            {
                Console.WriteLine("Argument required => write the project name:");
                newProjectName = Console.ReadLine();
            }
            else
            {
                newProjectName = args[0];
            }

            newProjectName = newProjectName.FirstCharToUpper();

            Console.WriteLine($"The new project is:{newProjectName}");

            CleanHelper.CleanProject(_pathBaseProject);

            Console.WriteLine($"Project clear");

            DirectoryHelper.DirectoryCopy(_pathBaseProject, $"./NewProject", true);
            Console.WriteLine($"New project created");

            RenameFilesAndDirectories($"./NewProject", newProjectName);
            Console.WriteLine($"Files and directories renamed");
        }

        static void RenameFilesAndDirectories(string directory, string nameNewProject)
        {
            try
            {
                var files = Directory.GetFiles(directory);
                var directories = Directory.GetDirectories(directory);

                //Files
                foreach (var file in files)
                {
                    FileHelper.ReplaceContent(file, Program.BaseProject, nameNewProject);
                    FileHelper.RenameFile(file, Program.BaseProject, nameNewProject);
                }

                //Directories
                foreach (var d in directories)
                {
                    RenameFilesAndDirectories(d, nameNewProject);
                    DirectoryHelper.RenameDirectory(d, Program.BaseProject, nameNewProject);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{nameof(CreateNewProject)} error:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
