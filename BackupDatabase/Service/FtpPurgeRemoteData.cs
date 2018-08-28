using BackupDatabase.Interface;
using FtpProject.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BackupDatabase.Service
{
    public class FtpPurgeRemoteData : IFtpPurgeRemoteData
    {
        public int RetentionDays { get; set; }
        public string RemoteBasePath { get; set; }
        public IFtpListDirectoryOnRemote FtpListDirectoryOnRemote { get; set; }
        public IFtpRemoveDirectory FtpRemoveDirectory { get; set; }

        public FtpPurgeRemoteData(IFtpListDirectoryOnRemote ftpListDirectoryOnRemote, IFtpRemoveDirectory ftpRemoveDirectory,
            int retentionDays, string remoteBasePath)
        {
            RetentionDays = retentionDays;
            RemoteBasePath = remoteBasePath;
            FtpListDirectoryOnRemote = ftpListDirectoryOnRemote;
            FtpRemoveDirectory = ftpRemoveDirectory;
        }

        public void Go(List<string> yearFolderList)
        {
            //purge old files
            var keepFilesAfter = DateTime.Now.AddDays(RetentionDays * -1);
            foreach (var year in yearFolderList)
            {
                var monthFolderList = FtpListDirectoryOnRemote.Go(true, year);
                foreach (var month in monthFolderList)
                {
                    var files = FtpListDirectoryOnRemote.Go(false, RemoteBasePath + "/" + month);

                    foreach (var file in files)
                    {
                        //file is in the format 08/20180821_spca_tracker.zip
                        var filePart = file.Split('/')[1];   //~ 20180821_spca_tracker.zip
                        filePart = filePart.Substring(0, 8); //~ 20180821

                        var assumedFileDate = DateTime.ParseExact(filePart, "yyyyMMdd", CultureInfo.InvariantCulture);
                        if (assumedFileDate < keepFilesAfter)
                        {
                            var ftpPathOfFileToDelete = year + "/" + file;
                            FtpListDirectoryOnRemote.FtpDeleteFile.DeleteFile(ftpPathOfFileToDelete);
                        }
                    }
                }
            }

            //purge old/empty folders
            foreach (var year in yearFolderList)
            {
                var monthFolderList = FtpListDirectoryOnRemote.Go(true, year);
                foreach (var month in monthFolderList)
                {
                    var files = FtpListDirectoryOnRemote.Go(false, RemoteBasePath + "/" + month);

                    //delete month folders if empty
                    if (files.Count.Equals(0))
                    {
                        FtpRemoveDirectory.RemoveDirectory(RemoteBasePath + "/" + month);
                    }
                }

                //delete year folders if empty
                if (monthFolderList.Count.Equals(0))
                {
                    FtpRemoveDirectory.RemoveDirectory(year);
                }
            }
        }
    }
}
