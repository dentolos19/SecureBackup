namespace SecureBackup.Core.Models
{

    public class BackupItem
    {

        public string Name { get; }

        public string Location { get; }

        public BackupItem(string name, string location)
        {
            Name = name;
            Location = location;
        }

    }

}