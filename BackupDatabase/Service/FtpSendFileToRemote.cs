using BackupDatabase.Interface;
using FtpProject.Interface;

namespace BackupDatabase.Service
{
    public class FtpSendFileToRemote : IFtpSendFileToRemote
    {
        public string RemoteBasePath { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string RemoteFtpServer { get; set; }
        public string RemoteFtpUsr { get; set; }
        public string RemoteFtpPwd { get; set; }
        public string BackupPath { get; set; }

        public IFtpSendFile FtpSendFile { get; set; }

        public void Go(string localFilePath)
        {
            var ftpServerUrlWithFileName = string.Format("{0}/{1}/{2}/{3}/{4}",
                RemoteFtpServer,
                RemoteBasePath,
                Year,
                Month,
                localFilePath);

            var completelocalFilePath = BackupPath + localFilePath;

            FtpSendFile.Go(ftpServerUrlWithFileName, completelocalFilePath);
        }
    }
}
