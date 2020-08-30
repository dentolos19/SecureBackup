using System;
using System.IO;

namespace SecureBackup.Core
{

    public static class Constants
    {

        public static string BackupsPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, "Backups");

    }

}