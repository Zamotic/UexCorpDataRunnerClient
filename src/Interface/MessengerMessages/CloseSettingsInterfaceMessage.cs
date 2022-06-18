using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Interface.MessengerMessages;
public class CloseSettingsInterfaceMessage
{
    public bool ReloadData { get; }
    public CloseSettingsInterfaceMessage(bool reloadData)
    {
        ReloadData = reloadData;
    }
}
