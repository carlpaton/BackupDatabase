using BackupDatabase.Interface;
using BackupDatabase.Service;
using FtpProject.Interface;
using FtpProject.Service;
using LoggerProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helpers;

namespace Test.Integration
{
    [TestClass]
    public class FtpPurgeRemoteDataTests
    {
        private readonly IFtpPurgeRemoteData _ftpPurgeRemoteData;
        private readonly IFtpMakeDirectory _ftpMakeDirectory;
        private readonly IFtpSendFile _ftpSendFile;
        private readonly IFtpListDirectoryOnRemote _ftpListDirectoryOnRemote;

        private readonly string _remoteFtpServer;
        private readonly string _remoteFtpUsr;
        private readonly string _remoteFtpPwd;
        private readonly int _remoteRetention;
        private readonly string _remoteBasePath;

        public FtpPurgeRemoteDataTests()
        {
            _remoteFtpServer = "ftp://192.168.0.185";
            _remoteFtpUsr = "carl";
            _remoteFtpPwd = "carl";
            _remoteRetention = 60;
            _remoteBasePath = "MySQL";

            var ftpListDirectory = new FtpListDirectory(
                _remoteFtpServer,
                _remoteFtpUsr,
                _remoteFtpPwd
            );

            var ftpDeleteFile = new FtpDeleteFile(
                _remoteFtpServer,
                _remoteFtpUsr,
                _remoteFtpPwd,
                new Logger()
            );

            _ftpListDirectoryOnRemote = new FtpListDirectoryOnRemote()
            {
                FtpListDirectory = ftpListDirectory,
                FtpDeleteFile = ftpDeleteFile
            };

            var _ftpRemoveDirectory = new FtpRemoveDirectory(
                _remoteFtpServer,
                _remoteFtpUsr,
                _remoteFtpPwd,
                new Logger()
            );

            _ftpPurgeRemoteData = new FtpPurgeRemoteData(
                _ftpListDirectoryOnRemote,
                _ftpRemoveDirectory,
                _remoteRetention,
                _remoteBasePath);

            _ftpMakeDirectory = new FtpMakeDirectory(
                _remoteFtpServer,
                _remoteFtpUsr,
                _remoteFtpPwd,
                new Logger());

            _ftpSendFile = new FtpSendFile(
                _remoteFtpUsr,
                _remoteFtpPwd,
                new Logger());
        }

        [TestMethod]
        public void PurgeRemoteData()
        {
            //precondition, create dummy local files
            var backupPath = @"C:\Data\Backup\MySQL\";

            new PurgeAll().Go(backupPath);
            new CreateDummyFiles().CreateHundred(backupPath);

            new PushLocalDummyFilesToRemoteFtp(_ftpMakeDirectory, _ftpSendFile)
                .Go(backupPath, _remoteBasePath, _remoteFtpServer);

            //arrange
            var yearFolderList = _ftpListDirectoryOnRemote.Go(true, _remoteBasePath);

            //act
            _ftpPurgeRemoteData.Go(yearFolderList);

            //assert
        }

        [TestMethod]
        public void PurgeRemoteFolders()
        {
            //precondition, create dummy local folders

            //arrange

            //act

            //assert
        }
    }
}
