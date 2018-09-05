# Backup Database (Over View)

This is a collaborative project that is made up of the following modules.

* BackupDatabase

This extracts a database dump, supported database are MySQL. (We will add MSSQL, PSQL ect)

* FtpProject

Classes interact with a remote FTP Service (Delete, List, Make Directory, Send)

This pushes the file to a remote FTP location to a folder YYYY/MM. Files olders than the retention period are purged.

* LoggerProject

Simple logger to event viewer.

* Tests

Initial integration tests

* FileZip

This compresses the database file, the name will be yyyyMMdd.zip

# Flow

1. Bring up a Ubuntu virtual machine

https://carlpaton.co.za/virtual-box/

2. Install FTP
https://help.ubuntu.com/lts/serverguide/ftp-server.html.en

sudo apt install vsftpd

sudo nano /etc/vsftpd.conf
write_enable=YES

sudo systemctl restart vsftpd.service

3. Connect to the FTP server with Filezilla to test

Ensure you connect on port 21 and not 22 (SFTP)

4. With MySQL Workbench restore the sample databases

* sample1
* sample2

5. Clone the code from GIT

* https://github.com/charleyza/BackupDatabase.git
* master branch

6. Open BackupDatabase.sln and restore nuget

* Configure the Config.settings
* RemoteFtpServer, DbList

7. Rebuild the solution

* From command line executed /bin/BackupDatabase.exe
