using FtpProject.Interface;
using LoggerProject;
using System;
using System.Net;

namespace FtpProject.Service
{
    public class FtpMakeDirectory : IFtpMakeDirectory
    {
        private readonly ILogger _logger;

        private readonly string _ftpServer = "";
        private readonly string _ftpUsr = "";
        private readonly string _ftpPwd = "";

        public FtpMakeDirectory(string ftpServer, string ftpUsr, string ftpPwd, ILogger logger)
        {
            _ftpServer = ftpServer;
            _ftpUsr = ftpUsr;
            _ftpPwd = ftpPwd;
            _logger = logger;
        }

        public void Go(string ftpFolderToCreate, bool log = false)
        {
            var subDirs = ftpFolderToCreate.Split('/');
            string currentDir = string.Format("{0}", _ftpServer);

            foreach (var subDir in subDirs)
            {
                //The given path request will end in / 
                //so the last element in the array will be ""
                //this can be ignored
                if (subDir == "")
                    return;

                try
                {
                    currentDir += "/" + subDir;
                    Console.WriteLine("path= {0}", currentDir);

                    if (log)
                    {
                        var s = "FtpMakeDirectory.Go()";
                        s = "\n\n currentDir=" + currentDir;
                        s += "\n _ftpUsr=" + _ftpUsr;
                        s += "\n _ftpPwd=" + _ftpPwd;

                        _logger.Log(s);
                    }

                    var request = WebRequest.Create(currentDir);
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    request.Credentials = new NetworkCredential(_ftpUsr, _ftpPwd);

                    using (var response = (FtpWebResponse)request.GetResponse())
                    {
                        //closed by framework
                    }
                }
                catch (Exception ex)
                {
                    //thrown if the dir exists
                    //could also be rights but this rudimentary assumption is fine

                    if (log)
                    {
                        Console.WriteLine(ex.Message);
                        _logger.Log(ex);
                    }
                }
            }
        }
    }
}
