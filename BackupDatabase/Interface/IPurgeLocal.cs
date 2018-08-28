namespace BackupDatabase.Interface
{
    public interface IPurgeLocal
    {
        int LocalRetention { get; set; }
        string LocalPath { get; set; }

        void Go();
    }
}
