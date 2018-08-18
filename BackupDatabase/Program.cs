using BackupDatabase.Service;
using System;

namespace BackupDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create paths if the dont exist
            new CreatePath(Config.Default.BackupPath).Go();
            new CreatePath(Config.Default.BackupPathTemp).Go();

            //Clear temp path
            new ClearPath(Config.Default.BackupPathTemp).Go();

            //Process each database
            foreach (var database in Config.Default.DbList)
            {
                var objDump = new DatabaseDump() {
                    BackupPath = Config.Default.BackupPath,
                    Database = database,
                    DbPassword = Config.Default.DbPassword,
                    DbUser = Config.Default.DbUser,
                    DumpFileStamp = DateTime.Now,
                    MySqlServerPath = Config.Default.MySqlServerPath                    
                };

                if (objDump.DoDump())
                {
                    var dumpFile = objDump.ResultDumpFile;
                }
                else
                {
                    //TODO
                    //log objDump.Error
                }
            }
        }
    }
}
