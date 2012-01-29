using System;
using System.Windows;
using MessageBox = Microsoft.Windows.Controls.MessageBox;

namespace Common.Utils
{
    public static class MessageBoxService
    {
        public static void ShowError(Exception exception)
        {
            Application.Current.Dispatcher.Invoke(
                new Func<string, string, MessageBoxButton, MessageBoxImage, MessageBoxResult>(MessageBox.Show),
                exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowError(string message)
        {
            Application.Current.Dispatcher.Invoke(
                new Func<string, string, MessageBoxButton, MessageBoxImage, MessageBoxResult>(MessageBox.Show),
                message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static MessageBoxResult ShowConfirmationMessage()
        {
            return (MessageBoxResult)Application.Current.Dispatcher.Invoke(
                new Func<string, string, MessageBoxButton, MessageBoxImage, MessageBoxResult>(MessageBox.Show),
                "Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
    }
}
