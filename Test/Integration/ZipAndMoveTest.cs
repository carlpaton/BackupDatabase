using BackupDatabase.Interface;
using BackupDatabase.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Integration
{
    [TestClass]
    public class ZipAndMoveTest
    {
        private readonly string _backupPath;
        private readonly string _backupPathTemp;
        private readonly string _backupName;
        private readonly IZipAndMove _zipAndMove;

        public ZipAndMoveTest()
        {
            _backupPath = @"C:\Data\Backup\MySQL\";
            _backupPathTemp = @"C:\Data\Backup\MySQL\Temp\";
            _backupName = "20180818_spca_tracker.sql";

            _zipAndMove = new ZipAndMove() {
                BackupPath = _backupPath,
                TempPath = _backupPathTemp
            };
        }

        [TestMethod]
        public void ZipAndMove()
        {
            //arrange
            //act
            var pass = _zipAndMove.Go(_backupName);

            //assert
            Assert.IsTrue(pass);
        }
    }
}
