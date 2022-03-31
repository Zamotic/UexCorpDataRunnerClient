using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using UexCorpDataRunner.DesktopClient.Core;
using UexCorpDataRunner.DesktopClient.Notifications;

namespace UexCorpDataRunner.DesktopClient.ViewModels;

public class MinimizedViewModel : ViewModelBase
{
    public readonly IMessenger _Messenger;

    public MinimizedViewModel(IMessenger messenger)
    {
        _Messenger = messenger;
        _Messenger.Register<HideUserInterfaceNotification>(this, HideUserInterfaceNotified);
    }

    public ICommand ShowUserInterfaceCommand => new RelayCommand(ShowUserInterfaceCommandExecute);
    private void ShowUserInterfaceCommandExecute()
    {
        IsEnabled = false;
        _Messenger.Send(new ShowUserInterfaceNotification());
    }

    private void HideUserInterfaceNotified(HideUserInterfaceNotification notification)
    {
        IsEnabled = true;
    }
}
