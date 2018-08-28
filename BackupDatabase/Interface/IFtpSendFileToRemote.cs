using FtpProject.Interface;

namespace BackupDatabase.Interface
{
    public interface IFtpSendFileToRemote
    {
        string RemoteBasePath { get; set; }
        string Year { get; set; }
        string Month { get; set; }
        string RemoteFtpServer { get; set; }
        string RemoteFtpUsr { get; set; }
        string RemoteFtpPwd { get; set; }
        string BackupPath { get; set; }

        IFtpSendFile FtpSendFile { get; set; }

        void Go(string fileName);
    }
}
