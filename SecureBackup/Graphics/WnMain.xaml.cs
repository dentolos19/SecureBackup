using System;
using System.IO;
using System.IO.Compression;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using SecureBackup.Core;
using SecureBackup.Core.Bindings;
using SecureBackup.Core.Models;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;
using AdonisMessageBoxButton = AdonisUI.Controls.MessageBoxButton;
using AdonisMessageBoxResult = AdonisUI.Controls.MessageBoxResult;
using AdonisMessageBoxImage = AdonisUI.Controls.MessageBoxImage;

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
            var dialog = new WnCreate { Owner = this };
            if (dialog.ShowDialog() == true)
                RefreshBackup(null, null);
        }

        private void DeleteBackup(object sender, RoutedEventArgs args)
        {
            var result = AdonisMessageBox.Show("Are you sure that you want to delete this backup? This is irreversible!", "SecureBackup", AdonisMessageBoxButton.YesNo, AdonisMessageBoxImage.Warning);
            if (result != AdonisMessageBoxResult.Yes)
                return;
            var item = (BackupItemBinding)BackupList.SelectedItem;
            File.Delete(item.DataLocation);
            RefreshBackup(null, null);
        }

        private void RestoreBackup(object sender, RoutedEventArgs args)
        {
            var passwordDialog = new WnPassword { Owner = this };
            if (passwordDialog.ShowDialog() != true)
                return;
            var item = (BackupItemBinding)BackupList.SelectedItem;
            var backup = BackupModel.Load(item.DataLocation);
            var unlocked = Cryptography.DecryptData(backup.Data, Utilities.FixPasswordLength(passwordDialog.PasswordResult), out var data);
            if (!unlocked)
            {
                AdonisMessageBox.Show("You entered the wrong password!", "SecureBackup", AdonisMessageBoxButton.OK, AdonisMessageBoxImage.Exclamation);
                return;
            }
            var archivePath = Path.Combine(Path.GetTempPath(), Utilities.GenerateAlphanumeric(16) + ".sbz");
            File.WriteAllBytes(archivePath, Convert.FromBase64String(data));
            var result = AdonisMessageBox.Show("Do you want to restore the backup to its original location? Choose otherwise to select a different location.", "SecurePad", AdonisMessageBoxButton.YesNo, AdonisMessageBoxImage.Question);
            if (result == AdonisMessageBoxResult.Yes)
            {
                if (!Directory.Exists(item.FromLocation))
                    Directory.CreateDirectory(item.FromLocation);
                ZipFile.ExtractToDirectory(archivePath, item.FromLocation, true);
            }
            else
            {
                var folderDialog = new VistaFolderBrowserDialog { ShowNewFolderButton = true };
                if (folderDialog.ShowDialog() == true)
                    ZipFile.ExtractToDirectory(archivePath, folderDialog.SelectedPath);
            }
            File.Delete(archivePath);
            AdonisMessageBox.Show("Restored backup! Thank you for using SecureBackup!", "SecureBackup", AdonisMessageBoxButton.OK, AdonisMessageBoxImage.Information);
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
                File.Copy(dialog.FileName, Path.Combine(Constants.BackupsPath, Utilities.GenerateAlphanumeric(16) + ".sbu"));
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