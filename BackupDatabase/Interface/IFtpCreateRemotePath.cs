using FtpProject.Interface;

namespace BackupDatabase.Interface
{
    public interface IFtpCreateRemotePath
    {
        string RemoteBasePath { get; set; }
        string Year { get; set; }
        string Month { get; set; }
        string RemoteFtpServer { get; set; }
        string RemoteFtpUsr { get; set; }
        string RemoteFtpPwd { get; set; }
        IFtpMakeDirectory FtpMakeDirectory { get; set; }

        void Go();
    }
}
