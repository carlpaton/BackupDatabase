using FtpProject.Interface;
using FtpProject.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Integration
{
    [TestClass]
    public class FtpListDirectoryOnRemoteTest
    {
        private readonly string _path;
        private readonly string _remoteFtpServer;
        private readonly string _remoteFtpUsr;
        private readonly string _remoteFtpPwd;
        private readonly IFtpListDirectory _ftpListDirectory;

        public FtpListDirectoryOnRemoteTest()
        {
            _path = "MySQL";
            _remoteFtpServer = "ftp://192.168.0.185";
            _remoteFtpUsr = "carl";
            _remoteFtpPwd = "carl";

            _ftpListDirectory = new FtpListDirectory(
                _remoteFtpServer,
                _remoteFtpUsr,
                _remoteFtpPwd);
        }

        [TestMethod]
        public void YearFolderList()
        {
            //arrange
            //act
            var list = _ftpListDirectory.ListDirectory(true, _path);

            //assert
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void MonthFolderList()
        {
            //arrange
            //act
            var list = _ftpListDirectory.ListDirectory(true, "MySQL/2018");

            //assert
            Assert.IsTrue(list.Count > 0);
        }
    }
}
