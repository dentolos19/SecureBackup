using System;
using System.IO;

namespace SecureBackup.Core
{

    public static class Constants
    {

        public static readonly string BackupsFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups");
        public static readonly string NewBackupTempFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NewBackup.tmp");

    }

}