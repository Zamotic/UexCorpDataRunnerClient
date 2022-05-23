using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Application.ViewModels.Bindables;
using UexCorpDataRunner.Application.MessengerMessages;
using UexCorpDataRunner.Domain.Models;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace UexCorpDataRunner.Application.ViewModels;

public class MainViewModel : ViewModelBase
{
    public readonly IMessenger _Messenger;

    private IList<BindableCommodity>? _BindableCommodities;
    public IList<BindableCommodity>? BindableCommodities
    {
        get => _BindableCommodities;
        set => SetProperty(ref _BindableCommodities, value);
    }

    public MainViewModel(IMessenger messenger)
    {
        IsEnabled = true;
        _Messenger = messenger;
        _Messenger.Register<ShowUserInterfaceMessage>(this, ShowUserInterfaceMessageHandler);
        _Messenger.Register<CloseSettingsInterfaceMessage>(this, CloseSettingsInterfaceMessageHandler);
        BindableCommodities = new List<BindableCommodity>()
        {
            new BindableCommodity(new Commodity()
            {
                Name = "Aluminum",
                Code = "ALUM",
                BuyPrice = 1.11m,
                SellPrice = 0m,
                Kind = "Metals",
                DateModified = DateTime.Now,
            }),
            new BindableCommodity(new Commodity()
            {
                Name = "Diamond",
                Code = "DIAM",
                BuyPrice = 5.85m,
                SellPrice = 0m,
                Kind = "Metals",
                DateAdded = DateTime.MinValue,
                DateModified = DateTime.Now,
            })
        };
    }

    public ICommand HideUserInterfaceCommand => new RelayCommand(HideUserInterfaceCommandExecute);
    private void HideUserInterfaceCommandExecute()
    {
        IsEnabled = false;
        _Messenger.Send(new HideUserInterfaceMessage());
    }

    public ICommand ShowSettingsInterfaceCommand => new RelayCommand(ShowSettingsInterfaceCommandExecute);
    private void ShowSettingsInterfaceCommandExecute()
    {
        IsEnabled = false;
        _Messenger.Send(new ShowSettingsInterfaceMessage());
    }

    //public void ShowUserInterfaceNotified(ShowUserInterfaceMessage notification)
    public void ShowUserInterfaceMessageHandler(object sender, ShowUserInterfaceMessage notification)
    {
        IsEnabled = true;
    }

    //public void CloseSettingsInterfaceNotified(CloseSettingsInterfaceMessage notification)
    public void CloseSettingsInterfaceMessageHandler(object sender, CloseSettingsInterfaceMessage notification)
    {
        IsEnabled = true;
    }
}
