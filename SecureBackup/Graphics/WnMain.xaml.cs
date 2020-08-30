using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using SecureBackup.Core;
using SecureBackup.Core.Bindings;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;
using AdonisMessageBoxButton = AdonisUI.Controls.MessageBoxButton;
using AdonisMessageBoxResult = AdonisUI.Controls.MessageBoxResult;
using MessageBoxImage = AdonisUI.Controls.MessageBoxImage;

namespace SecureBackup.Graphics
{

    public partial class WnMain
    {

        public WnMain()
        {
            InitializeComponent();
            RefreshBackup(null, null);
            UpdateSelection(null, null);
        }

        private void CreateBackup(object sender, RoutedEventArgs args)
        {
            // TODO
        }

        private void DeleteBackup(object sender, RoutedEventArgs args)
        {
            var result = AdonisMessageBox.Show("Are you sure that you want to delete this backup? This is irreversible!", "SecureBackup", AdonisMessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != AdonisMessageBoxResult.Yes)
                return;
            var item = (BackupItemBinding)BackupList.SelectedItem;
            File.Delete(item.DataLocation);
            RefreshBackup(null, null);
        }

        private void RestoreBackup(object sender, RoutedEventArgs args)
        {
            // TODO
        }

        private void RefreshBackup(object sender, RoutedEventArgs args)
        {
            BackupList.Items.Clear();
            foreach (var backupPath in Directory.GetFiles(Constants.BackupsPath, "*.sbu"))
                BackupList.Items.Add(BackupItemBinding.Create(backupPath));
        }

        private void ImportBackup(object sender, RoutedEventArgs args)
        {
            var dialog = new OpenFileDialog { Filter = "SecureBackup|*.sbu" };
            if (dialog.ShowDialog() == true)
            {
                File.Copy(dialog.FileName, Path.Combine(Constants.BackupsPath, Utilities.GenerateAlphanumeric(8) + ".sbu"));
                RefreshBackup(null, null);
            }
        }

        private void ExportBackup(object sender, RoutedEventArgs args)
        {
            var backup = (BackupItemBinding)BackupList.SelectedItem;
            var dialog = new SaveFileDialog { Filter = "SecureBackup|*.sbu", FileName = backup.BackupName };
            if (dialog.ShowDialog() == true)
                File.Copy(backup.DataLocation, dialog.FileName, true);
        }

        private void UpdateSelection(object sender, SelectionChangedEventArgs args)
        {
            if (BackupList.SelectedItem == null)
            {
                DeleteButton.IsEnabled = false;
                RestoreButton.IsEnabled = false;
                ExportButton.IsEnabled = false;
            }
            else
            {
                DeleteButton.IsEnabled = true;
                RestoreButton.IsEnabled = true;
                ExportButton.IsEnabled = true;
            }
        }

    }

}