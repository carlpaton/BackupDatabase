using System.IO;

namespace BackupDatabase.Service
{
    public class CreatePath
    {
        private readonly string Path;

        public CreatePath(string path)
        {
            Path = path;
        }

        public void Go()
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
                System.Console.WriteLine("Create path: {0}", Path);
            }
        }
    }
}
