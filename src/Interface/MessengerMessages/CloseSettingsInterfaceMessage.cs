using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Interface.MessengerMessages;
public class CloseSettingsInterfaceMessage
{
    public bool UserApiChanged { get; }
    public bool UserAccessCodeChanged { get; }
    public bool AlwaysOnTopChanged { get; }
    public bool ShowTemporaryCommoditiesChanged { get; }

    public CloseSettingsInterfaceMessage(bool userApiChanged, bool userAccessCodeChanged = false, bool alwaysOnTopChanged = false, bool showTemporaryCommoditiesChanged = false)
    {
        UserApiChanged = userApiChanged;
        UserAccessCodeChanged = userAccessCodeChanged;
        AlwaysOnTopChanged = alwaysOnTopChanged;
        ShowTemporaryCommoditiesChanged = showTemporaryCommoditiesChanged;
    }
}
