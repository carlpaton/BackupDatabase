using FtpProject.Interface;
using LoggerProject;
using System;
using System.Net;

namespace FtpProject.Service
{
    public class FtpSendFile : IFtpSendFile
    {
        private readonly ILogger _logger;

        private readonly string _ftpUsr = "";
        private readonly string _ftpPwd = "";

        public FtpSendFile(string ftpUsr, string ftpPwd, ILogger logger)
        {
            _ftpUsr = ftpUsr;
            _ftpPwd = ftpPwd;
            _logger = logger;
        }

        public void Go(string ftpServerUrlWithFileName, string localFilePath)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(_ftpUsr, _ftpPwd);
                    client.UploadFile(ftpServerUrlWithFileName, "STOR", localFilePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.Log(ex);
            }
        }
    }
}
