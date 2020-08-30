using System;
using System.IO;
using System.IO.Compression;
using System.Windows;
using Ookii.Dialogs.Wpf;
using SecureBackup.Core;
using SecureBackup.Core.Models;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;
using AdonisMessageBoxButton = AdonisUI.Controls.MessageBoxButton;
using AdonisMessageBoxImage = AdonisUI.Controls.MessageBoxImage;

namespace SecureBackup.Graphics
{

    public partial class WnCreate
    {

        public WnCreate()
        {
            InitializeComponent();
        }

        private void Browse(object sender, RoutedEventArgs args)
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
                SourceText.Text = dialog.SelectedPath;
        }

        private void Continue(object sender, RoutedEventArgs args)
        {
            if (string.IsNullOrEmpty(NameText.Text))
            {
                AdonisMessageBox.Show("You must enter a name for your backup!", "SecureBackup", AdonisMessageBoxButton.OK, AdonisMessageBoxImage.Exclamation);
                return;
            }
            if (PasswordText.Password.Length < 8)
            {
                AdonisMessageBox.Show("Your password must be at least 8 characters long!", "SecureBackup", AdonisMessageBoxButton.OK, AdonisMessageBoxImage.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(SourceText.Text))
            {
                AdonisMessageBox.Show("You must select a source for your backup!", "SecureBackup", AdonisMessageBoxButton.OK, AdonisMessageBoxImage.Exclamation);
                return;
            }
            var archivePath = Path.Combine(Path.GetTempPath(), Utilities.GenerateAlphanumeric(16) + ".sbz");
            ZipFile.CreateFromDirectory(SourceText.Text, archivePath);
            var backup = new BackupModel();
            backup.BackupName = NameText.Text;
            backup.FromLocation = SourceText.Text;
            backup.BackupTime = DateTime.Now;
            backup.Data = Convert.ToBase64String(File.ReadAllBytes(archivePath));
            backup.Save(Path.Combine(Constants.BackupsPath, Utilities.GenerateAlphanumeric(16) + ".sbu"));
            File.Delete(archivePath);
            DialogResult = true;
            Close();
        }

    }

}