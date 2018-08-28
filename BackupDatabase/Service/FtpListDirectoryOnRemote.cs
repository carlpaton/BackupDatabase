using BackupDatabase.Interface;
using FtpProject.Interface;
using System.Collections.Generic;

namespace BackupDatabase.Service
{
    public class FtpListDirectoryOnRemote : IFtpListDirectoryOnRemote
    {
        public IFtpListDirectory FtpListDirectory { get; set; }
        public IFtpDeleteFile FtpDeleteFile { get; set; }

        public List<string> Go(bool foldersOnly, string path)
        {
            return FtpListDirectory.ListDirectory(foldersOnly, path);
        }
    }
}
