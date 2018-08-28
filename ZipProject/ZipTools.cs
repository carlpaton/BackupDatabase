using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace ZipProject
{
    public static class ZipTools
    {
        /// <summary>
        /// Create a zip file from the specified file using the specified zip file name
        /// </summary>
        /// <param name="fileToZip"></param>
        /// <param name="zipFileName"></param>
        /// <param name="targetFolder"></param>
        /// <param name="password"></param>
        public static void CreateZipFile(string fileToZip, string zipFileName, string targetFolder, string password)
        {
            using (FileStream fs = File.Create(Path.Combine(targetFolder, zipFileName))) // Create the zip file
            {
                using (ZipOutputStream zs = new ZipOutputStream(fs)) // Create a new zip stream with the above file
                {
                    zs.SetLevel(5); // Set the level of compression (0-9 where 9 is the highest)                    
                    zs.Password = password; // Password if required, pass null if you want it blank

                    AddFile(fileToZip, targetFolder, zs);

                    zs.IsStreamOwner = true; // Forces 'zipStream.Close()' to also 'Close' the underlying stream
                    zs.Close();
                }
            }
        }

        /// <summary>
        /// Create a zip file using the fileToZip's name
        /// </summary>
        /// <param name="fileToZip"></param>
        /// <param name="targetFolder"></param>
        /// <param name="password"></param>
        public static void CreateZipFile(string fileToZip, string targetFolder, string password)
        {
            int fileOffset = fileToZip.Length - 4;
            string sub = fileToZip.Substring(0, fileOffset);
            sub = string.Format("{0}.zip", sub);

            using (FileStream fs = File.Create(Path.Combine(targetFolder, sub))) // Create the zip file
            {
                using (ZipOutputStream zs = new ZipOutputStream(fs)) // Create a new zip stream with the above file
                {
                    zs.SetLevel(5); // Set the level of compression (0-9 where 9 is the highest)                    
                    zs.Password = password; // Password if required, pass null if you want it blank

                    AddFile(fileToZip, targetFolder, zs);

                    zs.IsStreamOwner = true; // Forces 'zipStream.Close()' to also 'Close' the underlying stream
                    zs.Close();
                }
            }
        }

        /// <summary>
        /// Add the file (CSV or whatever) to a zip file created in 'CreateZipFile'
        /// </summary>
        /// <param name="fileToZip"></param>
        /// <param name="targetFolder"></param>
        /// <param name="zs"></param>
        private static void AddFile(string fileToZip, string targetFolder, ZipOutputStream zs)
        {
            FileInfo fi = new FileInfo(Path.Combine(targetFolder, fileToZip));

            //int folderOffset = targetFolder.Length; // This is used with 'Substring' to get each file as 'entryName'
            //string entryName = fileToZip.Substring(folderOffset); // Get the file name to go into the zip            
            //entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction
            ZipEntry ze = new ZipEntry(fileToZip);
            ze.DateTime = fi.LastWriteTime; // Set the Modified date

            /* *** Optional Voodoo
					* newEntry.AESKeySize = 256; // Triggers AES encryption. Allowable values are 0 (off), 128 or 256.
					* zs.UseZip64 = UseZip64.Off; // Allow the zip to be un-zipped by legacy products (WinXP built-in extractor / Winzip 8)
					* newEntry.Size = fi.Length; // Allow the zip to be un-zipped by legacy products (WinXP built-in extractor / Winzip 8)
					*/

            zs.UseZip64 = UseZip64.Off; // Switch UseZip64 off
            ze.Size = fi.Length; // Set the size

            zs.PutNextEntry(ze);
            byte[] buffer = new byte[4096]; // Zip the file in buffered chunks
            using (FileStream sr = File.OpenRead(Path.Combine(targetFolder, fileToZip)))
            {
                StreamUtils.Copy(sr, zs, buffer);
            }
            zs.CloseEntry();
        }

        /// <summary>
        /// Zips an entire folder
        /// </summary>
        /// <param name="outPathname">
        /// Path for your zip file, example 'C:\temp\myfile.zip'
        /// </param>
        /// <param name="password">
        /// null
        /// </param>
        /// <param name="folderNameToZip">
        /// Folder to zip (will include all sub directorys), example 'c:\myfiles\'
        /// </param>
        public static void CreateZipFileFromFolder(string outPathname, string password, string folderNameToZip)
        {
            using (FileStream fsOut = File.Create(outPathname))
            {
                using (ZipOutputStream zipStream = new ZipOutputStream(fsOut))
                {
                    zipStream.SetLevel(5); //0-9, 9 being the highest level of compression

                    zipStream.Password = password;  // optional. Null is the same as not setting. Required if using AES.

                    // This setting will strip the leading part of the folder path in the entries, to
                    // make the entries relative to the starting folder.
                    // To include the full path for each entry up to the drive root, assign folderOffset = 0.
                    int folderOffset = folderNameToZip.Length + (folderNameToZip.EndsWith("\\") ? 0 : 1);

                    CompressFolder(folderNameToZip, zipStream, folderOffset);

                    zipStream.IsStreamOwner = true; // Makes the Close also Close the underlying stream
                    zipStream.Close();
                }

                fsOut.Close();
            }
        }
        private static void CompressFolder(string path, ZipOutputStream zipStream, int folderOffset)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string filename in files)
            {
                FileInfo fi = new FileInfo(filename);

                string entryName = filename.Substring(folderOffset); // Makes the name in zip based on the folder
                entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction

                ZipEntry newEntry = new ZipEntry(entryName);
                newEntry.DateTime = fi.LastWriteTime; // Note the zip format stores 2 second granularity

                // Specifying the AESKeySize triggers AES encryption. Allowable values are 0 (off), 128 or 256.
                // A password on the ZipOutputStream is required if using AES.
                //   newEntry.AESKeySize = 256;

                // To permit the zip to be unpacked by built-in extractor in WinXP and Server2003, WinZip 8, Java, and other older code,
                // you need to do one of the following: Specify UseZip64.Off, or set the Size.
                // If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, you do not need either,
                // but the zip will be in Zip64 format which not all utilities can understand.
                //   zipStream.UseZip64 = UseZip64.Off;
                newEntry.Size = fi.Length;

                zipStream.PutNextEntry(newEntry);

                // Zip the file in buffered chunks
                // the "using" will close the stream even if an exception occurs
                byte[] buffer = new byte[4096];
                using (FileStream streamReader = File.OpenRead(filename))
                {
                    StreamUtils.Copy(streamReader, zipStream, buffer);
                }
                zipStream.CloseEntry();
            }
            string[] folders = Directory.GetDirectories(path);
            foreach (string folder in folders)
            {
                CompressFolder(folder, zipStream, folderOffset);
            }
        }
    }
}
