using System.IO;

namespace Test.Helpers
{
    public class PurgeAll
    {
        public void Go(string backupPath)
        {
            if (!Directory.Exists(backupPath))
                Directory.CreateDirectory(backupPath);

            var files = new DirectoryInfo(backupPath).GetFiles("*.zip");
            foreach (var file in files)
            {
                File.Delete(file.FullName);
            }

            files = new DirectoryInfo(backupPath).GetFiles("*.sql");
            foreach (var file in files)
            {
                File.Delete(file.FullName);
            }
        }
    }
}
