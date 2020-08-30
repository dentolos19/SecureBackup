using System;
using System.Windows;
using SecureBackup.Core;

namespace SecureBackup.Graphics
{

    public partial class WnException
    {

        public WnException(Exception error)
        {
            InitializeComponent();
            MessageText.Text = error.Message;
            StackTraceText.Text = error.StackTrace ?? "The error info doesn't contain any stack trace data.";
        }

        private void Restart(object sender, RoutedEventArgs args)
        {
            Utilities.RestartApp();
        }

        private void Exit(object sender, RoutedEventArgs args)
        {
            Application.Current.Shutdown();
        }

    }

}