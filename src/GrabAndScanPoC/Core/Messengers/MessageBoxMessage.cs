using GrabAndScanPoC.Common;

namespace GrabAndScanPoC.Core.Messengers;
public class MessageBoxMessage
{
    public IMessageBox MessageBox { get; set; }
    public MessageBoxMessage(IMessageBox messageBox)
    {
        MessageBox = messageBox;
    }
    public MessageBoxMessage(string message, string caption, MessageBoxImage messageBoxImage = MessageBoxImage.None, MessageBoxButton messageBoxButton = MessageBoxButton.Ok)
    {
        MessageBox = new MessageBox(message, caption, messageBoxImage, messageBoxButton);
    }
}
