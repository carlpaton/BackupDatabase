using BackupDatabase.Interface;
using BackupDatabase.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test.Integration
{
    [TestClass]
    public class DatabaseDumpTest
    {
        private readonly string _backupPath;
        private readonly string _backupPathTemp;
        private readonly string _database;
        private readonly ICreatePath _createPath;
        private readonly IClearPath _clearPath;
        private readonly IDatabaseDump _databaseDump;        

        public DatabaseDumpTest()
        {
            _backupPath = @"C:\Data\Backup\MySQL\";
            _backupPathTemp = @"C:\Data\Backup\MySQL\Temp\";
            _database = "my_test_db";
            _createPath = new CreatePath();
            _clearPath = new ClearPath();

            _databaseDump = new DatabaseDump()
            {
                BackupPath = _backupPath,
                DbPassword = "root",
                DbUser = "root",
                DumpFileStamp = DateTime.Now,
                MySqlServerPath = @"C:\Program Files\MySQL\MySQL Server 5.7\bin"
            };

            _createPath.Go(_backupPath);
            _createPath.Go(_backupPathTemp);
            _clearPath.Go(_backupPathTemp);
        }

        [TestMethod]
        public void CreateDump()
        {
            //arrange
            //act
            var pass = _databaseDump.Go(_database);

            //assert
            Assert.IsTrue(pass);
        }
    }
}
