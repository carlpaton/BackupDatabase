using BackupDatabase.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test.Unit
{
    [TestClass]
    public class DatabaseDumpTest
    {
        [TestMethod]
        public void CreateDump()
        {
            //arrange
            var objDump = new DatabaseDump()
            {
                BackupPath = @"C:\Data\Backup\MySQL\",
                Database = "spca_tracker",
                DbPassword = "root",
                DbUser = "root",
                DumpFileStamp = DateTime.Now,
                MySqlServerPath = @"C:\Program Files\MySQL\MySQL Server 5.7\bin"
            };

            //act
            var pass = objDump.DoDump();

            //assert
            Assert.IsTrue(pass);
        }
    }
}
