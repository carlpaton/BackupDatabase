using System.Collections.Generic;

namespace BackupDatabase.Interface
{
    public interface IFtpPurgeRemoteData
    {
        int RetentionDays { get; set; }
        string RemoteBasePath { get; set; }
        IFtpListDirectoryOnRemote FtpListDirectoryOnRemote { get; set; }

        void Go(List<string> yearFolderList);
    }
}
