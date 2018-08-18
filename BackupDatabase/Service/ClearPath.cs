using System.IO;

namespace BackupDatabase.Service
{
    public class ClearPath
    {
        private readonly string Path;

        public ClearPath(string path)
        {
            Path = path;
        }

        public void Go()
        {
            var files = new DirectoryInfo(Path).GetFiles();
            foreach (var file in files)
            {
                File.Delete(file.FullName);
                System.Console.WriteLine("Purge file: {0}", file.FullName);
            }
        }
    }
}
