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
            if (!Directory.Exists(Constants.BackupsPath))
                Directory.CreateDirectory(Constants.BackupsPath);
            new WnMain().Show();
        }

    }

}