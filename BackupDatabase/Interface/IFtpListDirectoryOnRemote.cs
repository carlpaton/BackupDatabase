using FtpProject.Interface;
using System.Collections.Generic;

namespace BackupDatabase.Interface
{
    public interface IFtpListDirectoryOnRemote
    {
        IFtpListDirectory FtpListDirectory { get; set; }
        IFtpDeleteFile FtpDeleteFile { get; set; }

        List<string> Go(bool foldersOnly, string path);
    }
}
