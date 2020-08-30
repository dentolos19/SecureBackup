using System.IO;
using System.Windows;
using System.Windows.Threading;
using SecureBackup.Core;
using SecureBackup.Graphics;

namespace SecureBackup
{

    public partial class App
    {

        private void InitializeApp(object sender, StartupEventArgs args)
        {
            if (!Directory.Exists(Constants.BackupsPath))
                Directory.CreateDirectory(Constants.BackupsPath);
            new WnMain().Show();
        }

        private void HandleException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            new WnException(args.Exception).ShowDialog();
            args.Handled = true;
        }

    }

}