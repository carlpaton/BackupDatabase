using BackupDatabase.Service;
using FtpProject.Service;
using LoggerProject;
using System;

namespace BackupDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            var createPath = new CreatePath();
            var clearPath = new ClearPath();
            var dt = DateTime.Now;
            var logger = new Logger();

            var databaseDump = new DatabaseDump()
            {
                BackupPath = Config.Default.BackupPath,
                DbPassword = Config.Default.DbPassword,
                DbUser = Config.Default.DbUser,
                DumpFileStamp = DateTime.Now,
                MySqlServerPath = Config.Default.MySqlServerPath
            };
            var zipAndMove = new ZipAndMove()
            {
                BackupPath = Config.Default.BackupPath,
                TempPath = Config.Default.BackupPathTemp
            };
            var purgeLocal = new PurgeLocal()
            {
                LocalRetention = Config.Default.LocalRetention,
                LocalPath = Config.Default.BackupPath
            };

            var ftpMakeDirectory = new FtpMakeDirectory(
                Config.Default.RemoteFtpServer,
                Config.Default.RemoteFtpUsr,
                Config.Default.RemoteFtpPwd,
                logger);

            var ftpCreateRemotePath = new FtpCreateRemotePath()
            {
                FtpMakeDirectory = ftpMakeDirectory,
                Month = dt.ToString("MM"),
                RemoteBasePath = Config.Default.RemoteBasePath,
                RemoteFtpPwd = Config.Default.RemoteFtpPwd,
                RemoteFtpServer = Config.Default.RemoteFtpServer,
                RemoteFtpUsr = Config.Default.RemoteFtpUsr,
                Year = dt.Year.ToString()
            };

            var ftpSendFile = new FtpSendFile(
                Config.Default.RemoteFtpUsr,
                Config.Default.RemoteFtpPwd,
                logger);

            var ftpSendFileToRemote = new FtpSendFileToRemote()
            {
                FtpSendFile = ftpSendFile,
                Month = dt.ToString("MM"),
                RemoteBasePath = Config.Default.RemoteBasePath,
                RemoteFtpPwd = Config.Default.RemoteFtpPwd,
                RemoteFtpServer = Config.Default.RemoteFtpServer,
                RemoteFtpUsr = Config.Default.RemoteFtpUsr,
                Year = dt.Year.ToString(),
                BackupPath = Config.Default.BackupPath
            };

            var ftpListDirectory = new FtpListDirectory(
                Config.Default.RemoteFtpServer,
                Config.Default.RemoteFtpUsr,
                Config.Default.RemoteFtpPwd
            );

            var ftpDeleteFile = new FtpDeleteFile(
                Config.Default.RemoteFtpServer,
                Config.Default.RemoteFtpUsr,
                Config.Default.RemoteFtpPwd,
                logger
            );

            var ftpListDirectoryOnRemote = new FtpListDirectoryOnRemote()
            {
                FtpListDirectory = ftpListDirectory,
                FtpDeleteFile = ftpDeleteFile
            };

            var ftpRemoveDirectory = new FtpRemoveDirectory(
                Config.Default.RemoteFtpServer,
                Config.Default.RemoteFtpUsr,
                Config.Default.RemoteFtpPwd,
                logger
            );

            var ftpPurgeRemoteData = new FtpPurgeRemoteData(
                ftpListDirectoryOnRemote,
                ftpRemoveDirectory,
                Config.Default.RemoteRetention,
                Config.Default.RemoteBasePath);

            new EntryPoint(
                createPath,
                clearPath,
                databaseDump,
                zipAndMove,
                purgeLocal,
                ftpCreateRemotePath,
                ftpSendFileToRemote,
                ftpListDirectoryOnRemote,
                ftpPurgeRemoteData,
                Config.Default.BackupPath,
                Config.Default.BackupPathTemp,
                Config.Default.RemoteBasePath,
                Config.Default.DbList)
                .Go();

            Console.WriteLine("DONE");
        }
    }
}
