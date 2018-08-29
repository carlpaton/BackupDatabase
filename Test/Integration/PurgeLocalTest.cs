using BackupDatabase.Interface;
using BackupDatabase.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helpers;

namespace Test.Integration
{
    [TestClass]
    public class PurgeLocalTest
    {
        private readonly IPurgeLocal _purgeLocal;

        public PurgeLocalTest()
        {
            var backupPath = @"C:\Data\Backup\MySQL\";
            _purgeLocal = new PurgeLocal()
            {
                LocalRetention = 30,
                LocalPath = backupPath
            };

            new PurgeAll().Go(backupPath);
            new CreateDummyFiles().CreateHundred(backupPath);
        }

        [TestMethod]
        public void PurgeLocal()
        {
            //arrange
            //act
            _purgeLocal.Go();

            //assert
        }
    }
}
