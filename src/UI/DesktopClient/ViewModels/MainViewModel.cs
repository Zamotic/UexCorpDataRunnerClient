
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using UexCorpDataRunner.DesktopClient.Core;
using UexCorpDataRunner.DesktopClient.Notifications;

namespace UexCorpDataRunner.DesktopClient.ViewModels;

public class MainViewModel : ViewModelBase
{
    public readonly IMessenger _Messenger;

    public MainViewModel(IMessenger messenger)
    {
        IsEnabled = true;
        _Messenger = messenger;
        _Messenger.Register<ShowUserInterfaceNotification>(this, ShowUserInterfaceNotified);
    }

    public ICommand HideUserInterfaceCommand => new RelayCommand(HideUserInterfaceCommandExecute);
    private void HideUserInterfaceCommandExecute()
    {
        IsEnabled = false;
        _Messenger.Send(new HideUserInterfaceNotification());
    }

    public void ShowUserInterfaceNotified(ShowUserInterfaceNotification notification)
    {
        IsEnabled = true;
    }
}
