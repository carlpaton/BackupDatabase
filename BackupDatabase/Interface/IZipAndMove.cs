namespace BackupDatabase.Interface
{
    public interface IZipAndMove
    {
        string TempPath { get; set; }
        string BackupPath { get; set; }
        string NewFileName { get; set; }

        bool Go(string backupName);
    }
}
