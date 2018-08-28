using BackupDatabase.Interface;
using BackupDatabase.Service;
using FtpProject.Interface;
using FtpProject.Service;
using LoggerProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

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
            var obj = new PurgeLocalTest();
            PushLocalDummyFilesToRemoteFtp(obj.BackupPath);

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

        private void PushLocalDummyFilesToRemoteFtp(string backupPath)
        {
            var files = new DirectoryInfo(backupPath).GetFiles("*.zip");
            var alreadyDone = new List<string>();

            foreach (var file in files)
            {
                var lastWriteTime = file.LastWriteTime;

                //create folders
                var yearFolder = _remoteBasePath + "/" + lastWriteTime.Year;
                var yearMonthFolder = _remoteBasePath + "/" + lastWriteTime.Year + "/" + lastWriteTime.ToString("MM");

                if (!alreadyDone.Contains(yearFolder))
                {
                    _ftpMakeDirectory.Go(yearFolder);
                    alreadyDone.Add(yearFolder);
                }

                if (!alreadyDone.Contains(yearMonthFolder))
                {
                    _ftpMakeDirectory.Go(yearMonthFolder);
                    alreadyDone.Add(yearMonthFolder);
                }

                var ftpServerUrlWithFileName = string.Format("{0}/{1}/{2}",
                    _remoteFtpServer,
                    yearMonthFolder,
                    file.Name);

                var completelocalFilePath = backupPath + file.Name;

                _ftpSendFile.Go(ftpServerUrlWithFileName, completelocalFilePath);
            }
        }
    }
}
