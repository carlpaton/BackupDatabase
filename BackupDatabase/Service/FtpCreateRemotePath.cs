using BackupDatabase.Interface;
using FtpProject.Interface;

namespace BackupDatabase.Service
{
    public class FtpCreateRemotePath : IFtpCreateRemotePath
    {
        public string RemoteBasePath { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string RemoteFtpServer { get; set; }
        public string RemoteFtpUsr { get; set; }
        public string RemoteFtpPwd { get; set; }
        public IFtpMakeDirectory FtpMakeDirectory { get; set; }

        public void Go()
        {
            var remotePath = string.Format("{0}/{1}/{2}/",
                RemoteBasePath,
                Year,
                Month);

            FtpMakeDirectory.Go(remotePath);
        }
    }
}
