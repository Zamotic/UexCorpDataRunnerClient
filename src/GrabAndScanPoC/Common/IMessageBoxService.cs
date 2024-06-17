using System.Windows;

namespace GrabAndScanPoC.Common;

public interface IMessageBoxService
{
    /// <summary>Displays a message box that has a message, title bar caption, button, and image; and that returns a result.</summary>           
    void LaunchMessageBox(string message, string caption, MessageBoxImage image = MessageBoxImage.Information, MessageBoxButton button = MessageBoxButton.Ok);
}
