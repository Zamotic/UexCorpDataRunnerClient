using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UexCorpDataRunner.DesktopClient.Core;
using UexCorpDataRunner.DesktopClient.Models;

namespace UexCorpDataRunner.DesktopClient.ViewModels;

public class MainViewModel : ViewModelBase, IMainVewModel
{
    public event HideUserInterfaceClickedEventHandler? HideUserInterfaceClicked;

    public readonly ViewModelMessenger _ViewModelMessenger;

    public string TestValue { get; set; } = "Test Value";

    private bool _IsMinimizedEnabled = false;
    public bool IsMinimizedEnabled 
    { 
        get => _IsMinimizedEnabled;
        set => SetProperty(ref _IsMinimizedEnabled, value);
    }

    public MainViewModel(ViewModelMessenger viewModelMessenger)
    {
        _ViewModelMessenger = viewModelMessenger;
        _ViewModelMessenger.Register<ShowUserInterfaceClickedModel>(this, (o) => { IsMinimizedEnabled = false; });
    }


    public ICommand HideUserInterfaceCommand => new RelayCommand(HideUserInterfaceCommandExecute);
    private void HideUserInterfaceCommandExecute()
    {
        IsMinimizedEnabled = true;
        OnHideUserInterfaceClicked(new EventArgs());
    }

    protected virtual void OnHideUserInterfaceClicked(EventArgs e)
    {
        if (HideUserInterfaceClicked is null)
        {
            return;
        }

        HideUserInterfaceClickedEventHandler handler = HideUserInterfaceClicked;
        handler?.Invoke(this, e);
    }

}
