using FtpProject.Interface;
using FtpProject.Service;
using LoggerProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Integration
{
    [TestClass]
    public class FtpCreateRemotePathTest
    {
        private readonly string _remoteBasePath;
        private readonly string _year;
        private readonly string _month;
        private readonly string _remoteFtpServer;
        private readonly string _remoteFtpUsr;
        private readonly string _remoteFtpPwd;
        private readonly IFtpMakeDirectory _ftpMakeDirectory;

        public FtpCreateRemotePathTest()
        {
            _remoteBasePath = "MySQL";
            _year = "2018";
            _month = "08";
            _remoteFtpServer = "ftp://192.168.0.185";
            _remoteFtpUsr = "carl";
            _remoteFtpPwd = "carl";

            _ftpMakeDirectory = new FtpMakeDirectory(
                _remoteFtpServer,
                _remoteFtpUsr,
                _remoteFtpPwd,
                new Logger());
        }

        [TestMethod]
        public void CreateDump()
        {
            //arrange
            var remotePath = string.Format("{0}/{1}/{2}/",
                _remoteBasePath,
                _year,
                _month);
            //act
            _ftpMakeDirectory.Go(remotePath);

            //assert
        }
    }
}
