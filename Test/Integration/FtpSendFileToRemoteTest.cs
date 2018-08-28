using FtpProject.Interface;
using FtpProject.Service;
using LoggerProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Integration
{
    [TestClass]
    public class FtpSendFileToRemoteTest
    {
        private readonly string _backupPath;
        private readonly string _remoteBasePath;
        private readonly string _year;
        private readonly string _month;
        private readonly string _remoteFtpServer;
        private readonly string _remoteFtpUsr;
        private readonly string _remoteFtpPwd;
        private readonly string _file;
        private readonly IFtpSendFile _ftpSendFile;

        public FtpSendFileToRemoteTest()
        {
            _remoteBasePath = "MySQL";
            _backupPath = @"C:\Data\Backup\MySQL\";
            _year = "2018";
            _month = "08";
            _remoteFtpServer = "ftp://192.168.0.185";
            _remoteFtpUsr = "carl";
            _remoteFtpPwd = "carl";
            _file = "20180818_my_test_db.zip";

            _ftpSendFile = new FtpSendFile(
                _remoteFtpUsr,
                _remoteFtpPwd,
                new Logger());
        }

        [TestMethod]
        public void FtpFile()
        {
            //arrange
            var ftpServerUrlWithFileName = string.Format("{0}/{1}/{2}/{3}/{4}",
                _remoteFtpServer,
                _remoteBasePath,
                _year,
                _month,
                _file);

            var localFilePath = _backupPath + _file;

            //act
            _ftpSendFile.Go(ftpServerUrlWithFileName, localFilePath);

            //assert
        }
    }
}
