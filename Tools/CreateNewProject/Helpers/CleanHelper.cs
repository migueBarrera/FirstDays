//Generator-tool for Xamarin Forms(c) 2020 Plain Concepts

using System.IO;

namespace CreateNewProject.Helpers
{
    public static class CleanHelper
    {
        public static void CleanProject(string directory)
        {
            var directories = Directory.GetDirectories(directory);

            foreach (var d in directories)
            {
                if (d.Contains("bin") || d.Contains("obj"))
                {
                    Directory.Delete(d, true);
                }
                else
                {
                    CleanProject(d);
                }
            }
        }
    }
}
