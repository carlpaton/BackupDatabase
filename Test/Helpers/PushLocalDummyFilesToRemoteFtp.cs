using FtpProject.Interface;
using System.Collections.Generic;
using System.IO;

namespace Test.Helpers
{
    public class PushLocalDummyFilesToRemoteFtp
    {
        private readonly IFtpMakeDirectory _ftpMakeDirectory;
        private readonly IFtpSendFile _ftpSendFile;

        public PushLocalDummyFilesToRemoteFtp(IFtpMakeDirectory ftpMakeDirectory, IFtpSendFile ftpSendFile)
        {
            _ftpMakeDirectory = ftpMakeDirectory;
            _ftpSendFile = ftpSendFile;
        }

        public void Go(string backupPath, string remoteBasePath, string remoteFtpServer)
        {
            var files = new DirectoryInfo(backupPath).GetFiles("*.zip");
            var alreadyDone = new List<string>();

            foreach (var file in files)
            {
                var lastWriteTime = file.LastWriteTime;

                //create folders
                var yearFolder = remoteBasePath + "/" + lastWriteTime.Year;
                var yearMonthFolder = remoteBasePath + "/" + lastWriteTime.Year + "/" + lastWriteTime.ToString("MM");

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
                    remoteFtpServer,
                    yearMonthFolder,
                    file.Name);

                var completelocalFilePath = backupPath + file.Name;

                _ftpSendFile.Go(ftpServerUrlWithFileName, completelocalFilePath);
            }
        }
    }
}
