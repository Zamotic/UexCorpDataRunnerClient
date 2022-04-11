using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using UexCorpDataRunner.DesktopClient.Core;
using UexCorpDataRunner.DesktopClient.Model;
using UexCorpDataRunner.DesktopClient.Notifications;

namespace UexCorpDataRunner.DesktopClient.ViewModels;

public class MainViewModel : ViewModelBase
{
    public readonly IMessenger _Messenger;

    private IList<BindableCommodityPrice>? _BindableCommodityPrices;
    public IList<BindableCommodityPrice>? BindableCommodityPrices
    {
        get => _BindableCommodityPrices;
        set => SetProperty(ref _BindableCommodityPrices, value);
    }

    public MainViewModel(IMessenger messenger)
    {
        IsEnabled = true;
        _Messenger = messenger;
        _Messenger.Register<ShowUserInterfaceNotification>(this, ShowUserInterfaceNotified);
        _Messenger.Register<CloseSettingsInterfaceNotification>(this, CloseSettingsInterfaceNotified);
        BindableCommodityPrices = new List<BindableCommodityPrice>()
        {
            new BindableCommodityPrice(new CommodityPrice()
            {
                Name = "Aluminum",
                OldPrice = 1.11m,
                MinPrice = 1.10m,
                MaxPrice = 1.20m,
                BestPrice = 1.10m,
                BestLocation = "HDMS-Perlman, Magda"
            }),
            new BindableCommodityPrice(new CommodityPrice()
            {
                Name = "Diamond",
                OldPrice = 5.85m,
                MinPrice = 5.85m,
                MaxPrice = 6.27m,
                BestPrice = 5.85m,
                BestLocation = "HDMS-Hahn, Magda"
            })
        };
    }

    public ICommand HideUserInterfaceCommand => new RelayCommand(HideUserInterfaceCommandExecute);
    private void HideUserInterfaceCommandExecute()
    {
        IsEnabled = false;
        _Messenger.Send(new HideUserInterfaceNotification());
    }

    public ICommand ShowSettingsInterfaceCommand => new RelayCommand(ShowSettingsInterfaceCommandExecute);
    private void ShowSettingsInterfaceCommandExecute()
    {
        IsEnabled = false;
        _Messenger.Send(new ShowSettingsInterfaceNotification());
    }

    public void ShowUserInterfaceNotified(ShowUserInterfaceNotification notification)
    {
        IsEnabled = true;
    }

    public void CloseSettingsInterfaceNotified(CloseSettingsInterfaceNotification notification)
    {
        IsEnabled = true;
    }
}
