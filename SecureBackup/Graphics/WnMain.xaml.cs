using System.IO;
using System.IO.Compression;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Ookii.Dialogs.Wpf;
using SecureBackup.Core;
using SecureBackup.Core.Models;

namespace SecureBackup.Graphics
{

    public partial class WnMain
    {

        public WnMain()
        {
            InitializeComponent();
        }

        private void UpdateSelected(object sender, SelectionChangedEventArgs args)
        {
            BnDeleteBackup.IsEnabled = LvBackups.SelectedItem != null;
        }

        private void OpenBackup(object sender, MouseButtonEventArgs args)
        {
            if (LvBackups.SelectedItem == null)
                return;
            MessageBox.Show("This function is not implemented yet.", "SecureBackup", MessageBoxButton.OK, MessageBoxImage.Information);
            // TODO: Implement "OpenBackup" Method Functions.
        }

        private void DeleteBackup(object sender, RoutedEventArgs args)
        {
            if (BnDeleteBackup.IsEnabled == false)
                return;
            if (MessageBox.Show("Are you sure that you want to delete this backup permanently.", "SecureBackup", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;
            var item = (BackupItem)LvBackups.SelectedItem;
            LvBackups.Items.Remove(LvBackups.SelectedItem);
            File.Delete(item.Location);
        }

        private async void CreateBackup(object sender, RoutedEventArgs args)
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == false)
                return;
            var name = await this.ShowInputAsync("SecureBackup", "Enter a new name for your new backup.");
            if (name.Length !> 0)
            {
                MessageBox.Show("You must enter a name! Backup creation is cancelled.", "SecureBackup", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            var password = await this.ShowInputAsync("SecureBackup", "Enter a new password for your new backup.");
            if (password.Length !> 7)
            {
                MessageBox.Show("Your password must be longer than 7 characters! Backup creation is cancelled.", "SecureBackup", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            ZipFile.CreateFromDirectory(dialog.SelectedPath, Constants.NewBackupTempFilePath);
            var backup = new BackupPackage(password, await File.ReadAllBytesAsync(Constants.NewBackupTempFilePath));
            File.Delete(Constants.NewBackupTempFilePath);
            backup.Save(Path.Combine(Constants.BackupsFolderPath, $"{name}.sbu"));
            RefreshBackups();
        }

        private void LoadBackups(object sender, RoutedEventArgs rgs)
        {
            RefreshBackups();
        }

        private void RefreshBackups()
        {
            LvBackups.Items.Clear();
            var backups = Directory.GetFiles(Constants.BackupsFolderPath);
            foreach (var backup in backups)
                LvBackups.Items.Add(new BackupItem(Path.GetFileName(backup), backup));
        }

        private void Exit(object sender, RoutedEventArgs args)
        {
            Application.Current.Shutdown();
        }

    }

}