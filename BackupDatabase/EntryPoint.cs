using BackupDatabase.Interface;
using LoggerProject;
using System;
using System.Collections.Specialized;

namespace BackupDatabase
{
    public class EntryPoint
    {
        private readonly ICreatePath _createPath;
        private readonly IClearPath _clearPath;
        private readonly IDatabaseDump _databaseDump;
        private readonly IZipAndMove _zipAndMove;
        private readonly IPurgeLocal _purgeLocal;
        private readonly IFtpCreateRemotePath _ftpCreateRemotePath;
        private readonly IFtpSendFileToRemote _ftpSendFileToRemote;
        private readonly IFtpListDirectoryOnRemote _ftpListDirectoryOnRemote;
        private readonly IFtpPurgeRemoteData _ftpPurgeRemoteData;

        private readonly string _backupPath;
        private readonly string _backupPathTemp;
        private readonly string _remoteBasePath;
        private readonly StringCollection _dbList;
        private readonly Logger _logger = new Logger();

        public EntryPoint(
            ICreatePath createPath, IClearPath clearPath, IDatabaseDump databaseDump, IZipAndMove zipAndMove, IPurgeLocal purgeLocal,
            IFtpCreateRemotePath ftpCreateRemotePath, IFtpSendFileToRemote ftpSendFileToRemote, IFtpListDirectoryOnRemote ftpListDirectoryOnRemote,
            IFtpPurgeRemoteData ftpPurgeRemoteData, string backupPath, string backupPathTemp, string remoteBasePath, StringCollection dbList)
        {
            _createPath = createPath;
            _clearPath = clearPath;
            _databaseDump = databaseDump;
            _zipAndMove = zipAndMove;
            _purgeLocal = purgeLocal;
            _ftpCreateRemotePath = ftpCreateRemotePath;
            _ftpSendFileToRemote = ftpSendFileToRemote;
            _ftpListDirectoryOnRemote = ftpListDirectoryOnRemote;
            _ftpPurgeRemoteData = ftpPurgeRemoteData;

            _backupPath = backupPath;
            _backupPathTemp = backupPathTemp;
            _remoteBasePath = remoteBasePath;

            _dbList = dbList;
        }

        public void Go()
        {
            _logger.Log("BackupDatabase START");

            _createPath.Go(_backupPath);
            _createPath.Go(_backupPathTemp);
            _clearPath.Go(_backupPathTemp);

            foreach (var database in _dbList)
            {
                try
                {
                    if (_databaseDump.Go(database))
                    {
                        var dumpFile = _databaseDump.ResultDumpFile; //20180818_DBNAME.sql

                        if (_zipAndMove.Go(dumpFile))
                        {
                            _purgeLocal.Go();
                            _ftpCreateRemotePath.Go();
                            _ftpSendFileToRemote.Go(_zipAndMove.NewFileName);
                        }

                        var yearFolderList = _ftpListDirectoryOnRemote.Go(true, _remoteBasePath);
                        _ftpPurgeRemoteData.Go(yearFolderList);
                    }
                    else
                        _logger.Log(_databaseDump.Error);
                }
                catch (Exception ex)
                {
                    _logger.Log(ex);
                }
            }

            _logger.Log("BackupDatabase FINISH");
        }
    }
}
