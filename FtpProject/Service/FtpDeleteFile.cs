using FtpProject.Interface;
using LoggerProject;
using System;
using System.Net;

namespace FtpProject.Service
{
    public class FtpDeleteFile : IFtpDeleteFile
    {
        private readonly ILogger _logger;

        private readonly string _ftpServer = "";
        private readonly string _ftpUsr = "";
        private readonly string _ftpPwd = "";

        public FtpDeleteFile(string ftpServer, string ftpUsr, string ftpPwd, ILogger logger)
        {
            _ftpServer = ftpServer;
            _ftpUsr = ftpUsr;
            _ftpPwd = ftpPwd;
            _logger = logger;
        }

        public void DeleteFile(string ftpPathOfFileToDelete)
        {
            try
            {
                var requestUrl = string.Format("{0}/{1}", _ftpServer, ftpPathOfFileToDelete);

                var request = WebRequest.Create(requestUrl);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(_ftpUsr, _ftpPwd);

                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    //closed by framework
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                _logger.Log(ex);
            }
        }
    }
}
