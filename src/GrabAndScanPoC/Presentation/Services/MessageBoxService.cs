using System.Windows;
using CMN = GrabAndScanPoC.Common;

namespace GrabAndScanPoc.Presentation.Services;
public class MessageBoxService : CMN.IMessageBoxService
{
    public void LaunchMessageBox(string message, string caption, CMN.MessageBoxImage image = CMN.MessageBoxImage.Information, CMN.MessageBoxButton button = CMN.MessageBoxButton.Ok)
    {
        MessageBoxButton convertedButton = ConvertMessageBoxButton(button);
        MessageBoxImage convertedImage = ConvertMessageBoxImage(image);

        MessageBox.Show(message, caption, convertedButton, convertedImage);
    }

    private MessageBoxImage ConvertMessageBoxImage(CMN.MessageBoxImage image)
    {
        return image switch
        {
            CMN.MessageBoxImage.Error => MessageBoxImage.Error,
            CMN.MessageBoxImage.Information => MessageBoxImage.Information,
            CMN.MessageBoxImage.None => MessageBoxImage.None,
            CMN.MessageBoxImage.Question => MessageBoxImage.Question,
            CMN.MessageBoxImage.Warning => MessageBoxImage.Warning,
            _ => throw new NotImplementedException(),
        };
    }

    private MessageBoxButton ConvertMessageBoxButton(CMN.MessageBoxButton button)
    {
        return button switch
        {
            CMN.MessageBoxButton.Ok => MessageBoxButton.OK,
            CMN.MessageBoxButton.OkCancel => MessageBoxButton.OKCancel,
            CMN.MessageBoxButton.YesNo => MessageBoxButton.YesNo,
            CMN.MessageBoxButton.YesNoCancel => MessageBoxButton.YesNoCancel,
            _ => throw new NotImplementedException(),
        };
    }


}
