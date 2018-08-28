using FtpProject.Interface;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace FtpProject.Service
{
    public class FtpListDirectory : IFtpListDirectory
    {
        readonly string _ftpServer = "";
        readonly string _ftpUsr = "";
        readonly string _ftpPwd = "";

        public FtpListDirectory(string ftpServer, string ftpUsr, string ftpPwd)
        {
            _ftpServer = ftpServer;
            _ftpUsr = ftpUsr;
            _ftpPwd = ftpPwd;
        }

        public List<string> ListDirectory(bool foldersOnly, string ftpPath)
        {
            var requestUrl = string.Format("{0}/{1}", _ftpServer, ftpPath);

            //the request could be to the root
            if (ftpPath.Equals(""))
                requestUrl = _ftpServer;

            var request = WebRequest.Create(requestUrl);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(_ftpUsr, _ftpPwd);

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                var responseStream = response.GetResponseStream();
                var reader = new StreamReader(responseStream);
                var result = new List<string>();

                while (!reader.EndOfStream)
                {
                    var item = reader.ReadLine();

                    //Console.WriteLine("item=" + item);

                    if (item.Equals("")) //invalid
                        continue;

                    //when testing I noticed . and .. came back 
                    //~ folder traversing not sure but its not valid for this function
                    if (item.Contains("/.") || item.Contains("/.."))
                        continue;

                    if (foldersOnly)
                    {
                        //assume files have extension .something
                        if (!item.Contains("."))
                            result.Add(item);
                    }
                    else
                        result.Add(item); //return files and folders
                }

                return result;
            }
        }
    }
}
