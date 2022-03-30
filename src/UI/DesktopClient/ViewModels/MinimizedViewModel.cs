using System;
using System.Windows.Input;
using UexCorpDataRunner.DesktopClient.Core;
using UexCorpDataRunner.DesktopClient.Models;

namespace UexCorpDataRunner.DesktopClient.ViewModels;

public class MinimizedViewModel : ViewModelBase, IMinimizedVewModel
{
    public event ShowUserInterfaceEventHandler? ShowUserInterfaceClicked;

    public readonly ViewModelMessenger _ViewModelMessenger;

    public MinimizedViewModel(ViewModelMessenger viewModelMessenger)
    {
        _ViewModelMessenger = viewModelMessenger;
    }

    protected virtual void OnShowUserInterfaceClicked(EventArgs e)
    {
        if (ShowUserInterfaceClicked is null)
        {
            return;
        }

        _ViewModelMessenger.Send(new ShowUserInterfaceClickedModel());

        ShowUserInterfaceEventHandler handler = ShowUserInterfaceClicked;
        handler?.Invoke(this, e);

    }

    public ICommand ShowUserInterfaceCommand => new RelayCommand(ShowUserInterfaceCommandExecute);
    private void ShowUserInterfaceCommandExecute()
    {
        OnShowUserInterfaceClicked(new EventArgs());
    }
}
