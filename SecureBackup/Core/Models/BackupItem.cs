namespace SecureBackup.Core.Models
{

    public class BackupItem
    {

        public BackupItem(string name, string location)
        {
            Name = name;
            Location = location;
        }

        public string Name { get; }

        public string Location { get; }

    }

}