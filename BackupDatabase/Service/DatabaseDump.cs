using BackupDatabase.Interface;
using System;
using System.Diagnostics;

namespace BackupDatabase.Service
{
    public class DatabaseDump : IDatabaseDump
    {
        public string ResultDumpFile { get; set; }

        public string DbUser { get; set; }
        public string DbPassword { get; set; }
        public string MySqlServerPath { get; set; }
        public DateTime DumpFileStamp { get; set; }
        public string BackupPath { get; set; }
        public string Error { get; set; }

        public bool Go(string database)
        {
            ResultDumpFile = string.Format("{0}_{1}.sql",
                DumpFileStamp.ToString("yyyyMMdd"),
                database);

            string arguments = string.Format("/c CD/&CD {0}& mysqldump -u{1} -p{2} {3} > {4}Temp\\{5}",
                MySqlServerPath,
                DbUser,
                DbPassword,
                database,
                BackupPath,
                ResultDumpFile);

            var cmd = new ProcessStartInfo("cmd", arguments)
            {
                UseShellExecute = false,
                RedirectStandardError = true,
                CreateNoWindow = false
            };

            Process proc;
            proc = Process.Start(cmd);
            proc.WaitForExit();

            string line = "";
            while (!proc.StandardError.EndOfStream)
            {
                line += proc.StandardError.ReadLine();
            }

            if (line != "mysqldump: [Warning] Using a password on the command line interface can be insecure.")
            {
                Error = line;
                return false;
            }

            return true;
        }
    }
}
