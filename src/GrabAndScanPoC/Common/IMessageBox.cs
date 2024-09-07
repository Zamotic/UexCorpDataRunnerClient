using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabAndScanPoC.Common;
public interface IMessageBox
{
    public string Message { get; set; }
    public string Caption { get; set; }
    public MessageBoxButton Button { get; set; }
    public MessageBoxImage Image { get; set; }
}
