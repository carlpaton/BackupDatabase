# Backup Database

This is a collaborative project that is made up of the following modules.

* BackupDatabase

This extracts a database dump, supported database are MySQL. (We will add MSSQL, PSQL ect)

* FileZip

This compresses the database file, the name will be yyyyMMdd.zip

* FTPManager

This pushes the file to a remote FTP location to a folder YYYY/MM
Files olders than the retention period are purged.

* Tests

Unit and integration tests.