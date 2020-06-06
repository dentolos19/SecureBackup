using System.Windows;
using SecureBackup.Core;
using SecureBackup.Graphics;

namespace SecureBackup
{

    public partial class App
    {

        private void Initialize(object sender, StartupEventArgs args)
        {
            Utilities.SetAppTheme(Utilities.GetRandomAccent());
            new WnMain().Show();
        }

    }

}