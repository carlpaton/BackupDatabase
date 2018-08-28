using BackupDatabase.Interface;
using System;
using System.IO;
using ZipProject;

namespace BackupDatabase.Service
{
    public class ZipAndMove : IZipAndMove
    {
        public string TempPath { get; set; }
        public string BackupPath { get; set; }
        public string NewFileName { get; set; }

        public bool Go(string backupName)
        {
            try
            {
                ZipTools.CreateZipFile(backupName, TempPath, null);
                if (File.Exists(Path.Combine(TempPath, backupName)))
                    File.Delete(Path.Combine(TempPath, backupName));

                var zipName = backupName.Replace(".sql", ".zip");
                NewFileName = zipName;

                if (File.Exists(Path.Combine(TempPath, zipName)))
                    File.Move(Path.Combine(TempPath, zipName), Path.Combine(BackupPath, zipName));

                return true;
            }
            catch (Exception ex)
            {
                //TODO - ILogger
                return false;
            }
        }
    }
}
