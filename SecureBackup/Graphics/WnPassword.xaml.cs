using System.Windows;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;
using AdonisMessageBoxButton = AdonisUI.Controls.MessageBoxButton;
using AdonisMessageBoxImage = AdonisUI.Controls.MessageBoxImage;

namespace SecureBackup.Graphics
{

    public partial class WnPassword
    {

        public string PasswordResult { get; private set; }

        public WnPassword()
        {
            InitializeComponent();
        }

        private void Continue(object sender, RoutedEventArgs args)
        {
            if (PasswordText.Password.Length < 8)
            {
                AdonisMessageBox.Show("The password must be at least 8 characters long!", "SecureBackup", AdonisMessageBoxButton.OK, AdonisMessageBoxImage.Exclamation);
                return;
            }
            PasswordResult = PasswordText.Password;
            DialogResult = true;
            Close();
        }

    }

}