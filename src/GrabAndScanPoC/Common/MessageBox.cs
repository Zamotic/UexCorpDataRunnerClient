using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabAndScanPoC.Common;
public class MessageBox : IMessageBox
{
    public string Message { get; set; }
    public string Caption { get; set; }
    public MessageBoxButton Button { get; set; }
    public MessageBoxImage Image { get; set; }

    public MessageBox(string message, string caption, MessageBoxImage image = MessageBoxImage.None, MessageBoxButton button = MessageBoxButton.Ok)
    {
        Message = message;
        Caption = caption;
        Image = image;
        Button = button;
    }
    
}
