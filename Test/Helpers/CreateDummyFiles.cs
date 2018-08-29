using System;
using System.IO;
using ZipProject;

namespace Test.Helpers
{
    public class CreateDummyFiles
    {
        public void CreateHundred(string backupPath)
        {
            var dt = DateTime.Now;
            for (int i = 0; i < 100; i++)
            {
                CreateSingle(i, dt, backupPath);
                dt = dt.AddDays(-1);
            }
        }

        public string CreateSingle(int i, DateTime dt, string backupPath, bool deleteSqlFile = true)
        {
            var sqlFile = dt.ToString("yyyyMMdd") + "_dummy" + ".sql";
            var path = backupPath + sqlFile;

            File.WriteAllText(path, i.ToString());

            ZipTools.CreateZipFile(sqlFile, backupPath, null);

            if (deleteSqlFile)
                File.Delete(Path.Combine(path));

            File.SetLastWriteTime(
                path.Replace(".sql", ".zip"),
                dt);

            return sqlFile;
        }
    }
}
