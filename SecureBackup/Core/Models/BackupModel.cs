using System.IO;
using System.Xml.Serialization;

namespace SecureBackup.Core.Models
{

    public class BackupModel
    {

        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(BackupModel));

        public string BackupName { get; set; }

        public string FromLocation { get; set; }

        public string Data { get; set; }

        public string BackupTime { get; set; }

        public void Save(string outputPath)
        {
            using var stream = new FileStream(outputPath, FileMode.Create);
            Serializer.Serialize(stream, this);
        }

        public static BackupModel Load(string inputPath)
        {
            using var stream = new FileStream(inputPath, FileMode.Open);
            return (BackupModel)Serializer.Deserialize(stream);
        }

    }

}