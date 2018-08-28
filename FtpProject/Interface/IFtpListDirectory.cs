using System.Collections.Generic;

namespace FtpProject.Interface
{
    public interface IFtpListDirectory
    {
        List<string> ListDirectory(bool foldersOnly, string ftpPath);
    }
}
