using System;
using System.IO;
using BackupDatabase.Interface;
using BackupDatabase.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZipProject;

namespace Test.Integration
{
    [TestClass]
    public class PurgeLocalTest
    {
        public string BackupPath { get; set; }

        private readonly IPurgeLocal _purgeLocal;

        public PurgeLocalTest()
        {
            BackupPath = @"C:\Data\Backup\MySQL\";
            _purgeLocal = new PurgeLocal()
            {
                LocalRetention = 30,
                LocalPath = BackupPath
            };

            PurgeAll();
            CreateDummyFiles();
        }

        [TestMethod]
        public void PurgeLocal()
        {
            //arrange
            //act
            _purgeLocal.Go();

            //assert
        }

        public void PurgeAll()
        {
            if (!Directory.Exists(BackupPath))
                Directory.CreateDirectory(BackupPath);

            var files = new DirectoryInfo(BackupPath).GetFiles("*.zip");
            foreach (var file in files)
            {
                File.Delete(file.FullName);
            }

            files = new DirectoryInfo(BackupPath).GetFiles("*.sql");
            foreach (var file in files)
            {
                File.Delete(file.FullName);
            }
        }

        public void CreateDummyFiles()
        {
            var dt = DateTime.Now;

            for (int i = 0; i < 100; i++)
            {
                var sqlFile = dt.ToString("yyyyMMdd") + "_dummy" + ".sql";
                var path = BackupPath + sqlFile;

                File.WriteAllText(path, i.ToString());

                ZipTools.CreateZipFile(sqlFile, BackupPath, null);
                File.Delete(Path.Combine(path));

                File.SetLastWriteTime(
                    path.Replace(".sql", ".zip"),
                    dt);

                dt = dt.AddDays(-1);
            }
        }
    }
}
