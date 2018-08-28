namespace FtpProject.Interface
{
    public interface IFtpSendFile
    {
        void Go(string ftpServerUrlWithFileName, string localFilePath);
    }
}
