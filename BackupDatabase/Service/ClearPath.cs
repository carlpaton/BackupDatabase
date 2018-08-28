using BackupDatabase.Interface;
using System.IO;

namespace BackupDatabase.Service
{
    public class ClearPath : IClearPath
    {
        public void Go(string path)
        {
            var files = new DirectoryInfo(path).GetFiles();
            foreach (var file in files)
            {
                File.Delete(file.FullName);
                System.Console.WriteLine("Purge file: {0}", file.FullName);
            }
        }
    }
}
