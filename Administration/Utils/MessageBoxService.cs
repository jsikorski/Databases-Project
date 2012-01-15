using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MessageBox = Microsoft.Windows.Controls.MessageBox;

namespace Administration.Utils
{
    public static class MessageBoxService
    {
        public static void ShowError(Exception exception)
        {

            App.Current.Dispatcher.Invoke(
                new Func<string, string, MessageBoxButton, MessageBoxImage, MessageBoxResult>(MessageBox.Show),
                exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
