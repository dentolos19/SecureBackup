using System.IO;
using System.Windows;
using SecureBackup.Core;
using SecureBackup.Graphics;

namespace SecureBackup
{

    public partial class App
    {

        private void Initialize(object sender, StartupEventArgs args)
        {
            if (!Directory.Exists(Constants.BackupsFolderPath))
                Directory.CreateDirectory(Constants.BackupsFolderPath);
            Utilities.SetAppTheme(Utilities.GetRandomAccent());
            new WnMain().Show();
        }

    }

}