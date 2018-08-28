using BackupDatabase.Interface;
using System;
using System.IO;

namespace BackupDatabase.Service
{
    public class PurgeLocal : IPurgeLocal
    {
        public int LocalRetention { get; set; }
        public string LocalPath { get; set; }

        public void Go()
        {
            var files = new DirectoryInfo(LocalPath).GetFiles("*.zip");
            var localRetention = LocalRetention * -1;

            foreach (var file in files)
            {
                if (DateTime.Now.AddDays(localRetention) > file.LastWriteTime)
                    File.Delete(file.FullName);
            }
        }
    }
}
