using System;

namespace UexCorpDataRunner.DesktopClient.Core;

public delegate void ShowUserInterfaceEventHandler(object sender, EventArgs e);
public interface IMinimizedVewModel
{
    event ShowUserInterfaceEventHandler? ShowUserInterfaceClicked;
}
