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
using UexCorpDataRunner.Domain.Services;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using UexCorpDataRunner.Domain.DataRunner;

namespace UexCorpDataRunner.Interface.DataRunner;

public class DataRunnerViewModel : ViewModelBase
{
    public readonly IMessenger _Messenger;
    public readonly IUexDataService _DataService;

    private IReadOnlyList<Domain.DataRunner.System> _SystemList = new List<Domain.DataRunner.System>();
    public IReadOnlyList<Domain.DataRunner.System> SystemList
    {
        get => _SystemList;
        set => SetProperty(ref _SystemList, value);
    }

    private Domain.DataRunner.System? _SelectedSystem = null;
    public Domain.DataRunner.System? SelectedSystem
    {
        get => _SelectedSystem;
        set => SetProperty(ref _SelectedSystem, value);
    }

    private IList<Planet> _PlanetList = new List<Planet>();
    public IList<Planet> PlanetList
    {
        get => _PlanetList;
        set => SetProperty(ref _PlanetList, value);
    }

    private Planet? _SelectedPlanet = null;
    public Planet? SelectedPlanet
    {
        get => _SelectedPlanet;
        set => SetProperty(ref _SelectedPlanet, value);
    }

    private IList<Satellite> _SatelliteList = new List<Satellite>();
    public IList<Satellite> SatelliteList
    {
        get => _SatelliteList;
        set => SetProperty(ref _SatelliteList, value);
    }

    private Satellite? _SelectedSatellite = null;
    public Satellite? SelectedSatellite
    {
        get => _SelectedSatellite;
        set => SetProperty(ref _SelectedSatellite, value);
    }

    private IList<Tradeport> _TradeportList = new List<Tradeport>();
    public IList<Tradeport> TradeportList
    {
        get => _TradeportList;
        set => SetProperty(ref _TradeportList, value);
    }

    private Tradeport? _SelectedTradeport = null;
    public Tradeport? SelectedTradeport
    {
        get => _SelectedTradeport;
        set => SetProperty(ref _SelectedTradeport, value);
    }

    private IList<BindableCommodity>? _BindableCommodities;
    public IList<BindableCommodity>? BindableCommodities
    {
        get => _BindableCommodities;
        set => SetProperty(ref _BindableCommodities, value);
    }

    public DataRunnerViewModel(IMessenger messenger, IUexDataService dataService)
    {
        IsEnabled = true;
        _Messenger = messenger;
        _DataService = dataService;

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

    public ICommand ViewModelLoadedCommand => new RelayCommand<object>(async (sender) => await ViewModelLoadedCommandExecuteAsync(sender));
    public async Task ViewModelLoadedCommandExecuteAsync(object? sender)
    {
        SystemList = await _DataService.GetAllSystemsAsync();
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
