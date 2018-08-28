namespace FtpProject.Interface
{
    public interface IFtpMakeDirectory
    {
        void Go(string ftpFolderToCreate, bool verboseLog = false);
    }
}
