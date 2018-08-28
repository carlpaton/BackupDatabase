using FtpProject.Interface;
using LoggerProject;
using System;
using System.Net;

namespace FtpProject.Service
{
    public class FtpRemoveDirectory : IFtpRemoveDirectory
    {
        private readonly ILogger _logger;

        private readonly string _ftpServer = "";
        private readonly string _ftpUsr = "";
        private readonly string _ftpPwd = "";

        public FtpRemoveDirectory(string ftpServer, string ftpUsr, string ftpPwd, ILogger logger)
        {
            _ftpServer = ftpServer;
            _ftpUsr = ftpUsr;
            _ftpPwd = ftpPwd;
            _logger = logger;
        }

        public void RemoveDirectory(string ftpPathOfDirectoryToDelete)
        {
            try
            {
                var requestUrl = string.Format("{0}/{1}", _ftpServer, ftpPathOfDirectoryToDelete);

                var request = WebRequest.Create(requestUrl);
                request.Method = WebRequestMethods.Ftp.RemoveDirectory;
                request.Credentials = new NetworkCredential(_ftpUsr, _ftpPwd);

                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    //closed by framework
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
