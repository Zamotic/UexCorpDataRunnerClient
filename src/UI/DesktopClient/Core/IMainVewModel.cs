using System;

namespace UexCorpDataRunner.DesktopClient.Core;

public delegate void HideUserInterfaceClickedEventHandler(object sender, EventArgs e);
public interface IMainVewModel
{
    event HideUserInterfaceClickedEventHandler? HideUserInterfaceClicked;
}
