using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Interface.MessengerMessages;
public class CloseSettingsInterfaceMessage
{
    public bool UserAccessCodeChanged { get; }
    public bool AlwaysOnTopChanged { get; }
    public bool ShowTemporaryCommoditiesChanged { get; }
    public bool UserSecretKeyChanged { get; }

    public CloseSettingsInterfaceMessage(bool userAccessCodeChanged, bool alwaysOnTopChanged = false, bool showTemporaryCommoditiesChanged = false, bool userSecretKeyChanged = false)
    {
        UserAccessCodeChanged = userAccessCodeChanged;
        AlwaysOnTopChanged = alwaysOnTopChanged;
        ShowTemporaryCommoditiesChanged = showTemporaryCommoditiesChanged;
        UserSecretKeyChanged = userSecretKeyChanged;
    }
}
