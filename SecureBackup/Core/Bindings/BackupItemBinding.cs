using System.Globalization;
using SecureBackup.Core.Models;

namespace SecureBackup.Core.Bindings
{

    public class BackupItemBinding
    {

        public string BackupName { get; private set; }

        public string FromLocation { get; private set; }

        public string DataLocation { get; private set; }

        public string BackupTime { get; private set; }

        public static BackupItemBinding Create(string backupPath)
        {
            var backup = BackupModel.Load(backupPath);
            return new BackupItemBinding
            {
                BackupName = backup.BackupName,
                FromLocation = backup.FromLocation,
                DataLocation = backupPath,
                BackupTime = backup.BackupTime.ToString(CultureInfo.CurrentCulture)
            };
        }

    }

}