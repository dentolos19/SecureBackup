using System.IO;
using System.Xml.Serialization;

namespace SecureBackup.Core.Models
{

    public class BackupPackage
    {

        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(BackupPackage));

        public string Password { get; set; }

        public byte[] Data { get; set; }

        public void Save(string output)
        {
            var originalPassword = Password;
            Password = Utilities.ToHexString(Password);
            using var stream = new FileStream(output, FileMode.Create);
            Serializer.Serialize(stream, this);
            Password = originalPassword;
        }

        public static BackupPackage Load(string input)
        {
            using var stream = new FileStream(input, FileMode.Open);
            var result = (BackupPackage) Serializer.Deserialize(stream);
            result.Password = Utilities.FromHexString(result.Password);
            return result;
        }

    }

}