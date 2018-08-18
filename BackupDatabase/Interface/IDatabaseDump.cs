using System;

namespace BackupDatabase.Interface
{
    public interface IDatabaseDump
    {

        string ResultDumpFile { get; set; }

        
        string MySqlServerPath { get; set; }
        string BackupPath { get; set; }
        string Error { get; set; }
        DateTime DumpFileStamp { get; set; }

        bool DoDump();
    }
}
