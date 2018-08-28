using BackupDatabase.Interface;
using System.IO;

namespace BackupDatabase.Service
{
    public class CreatePath : ICreatePath
    {
        public void Go(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                System.Console.WriteLine("Create path: {0}", path);
            }
        }
    }
}
