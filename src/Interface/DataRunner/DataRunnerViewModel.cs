﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Application.MessengerMessages;
using UexCorpDataRunner.Domain.Services;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using UexCorpDataRunner.Domain.DataRunner;
using System.Windows.Data;

namespace UexCorpDataRunner.Interface.DataRunner;

public class DataRunnerViewModel : ViewModelBase
{
    public readonly IMessenger _Messenger;
    public readonly IUexDataService _DataService;

    private IReadOnlyCollection<Domain.DataRunner.System> _SystemList = new List<Domain.DataRunner.System>();
    public IReadOnlyCollection<Domain.DataRunner.System> SystemList
    {
        get => _SystemList;
        set
        {
            SetProperty(ref _SystemList, value);
            SetSystemListCVS(true);
        }
    }

    private readonly CollectionViewSource _SystemListCVS = new CollectionViewSource();
    public ICollectionView SystemListCVS
    {
        get
        {
            return _SystemListCVS.View;
        }
    }
    private void SetSystemListCVS(bool resetSource = false)
    {
        var targetCVS = _SystemListCVS;
        if (targetCVS is null)
        {
            return;
        }

        using (targetCVS.DeferRefresh())
        {
            if (resetSource)
            {
                targetCVS.Source = SystemList;

                targetCVS.SortDescriptions.Clear();
                targetCVS.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            }

            if (targetCVS.Source is null)
            {
                targetCVS.Source = SystemList;

                targetCVS.SortDescriptions.Clear();
                targetCVS.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            }
        }
        OnPropertyChanged(nameof(SystemListCVS));
    }

    private Domain.DataRunner.System? _SelectedSystem = null;
    public Domain.DataRunner.System? SelectedSystem
    {
        get => _SelectedSystem;
        set
        {
            SetProperty(ref _SelectedSystem, value);
            if(SelectedSystem != null)
            {
                _ = UpdatePlanetListAsync(SelectedSystem.Code);
            }
        }
    }

    private IReadOnlyCollection<Planet> _PlanetList = new List<Planet>();
    private IReadOnlyCollection<Planet> PlanetList
    {
        get => _PlanetList;
        set
        {
            SetProperty(ref _PlanetList, value);
        }
    }

    private readonly CollectionViewSource _PlanetListCVS = new CollectionViewSource();
    public ICollectionView PlanetListCVS
    {
        get
        {
            return _PlanetListCVS.View;
        }
    }
    private void SetPlanetListCVS(bool resetSource = false)
    {
        var targetCVS = _PlanetListCVS;
        if (targetCVS is null)
        {
            return;
        }

        using (targetCVS.DeferRefresh())
        {
            if (resetSource)
            {
                targetCVS.Source = PlanetList;
                targetCVS.SortDescriptions.Clear();
                targetCVS.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            }

            if (targetCVS.Source is null)
            {
                targetCVS.Source = PlanetList;
                targetCVS.SortDescriptions.Clear();
                targetCVS.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            }

            targetCVS.Filter += (s, e) =>
            {
                Planet? planet = e.Item as Planet;
                if (planet is null)
                {
                    e.Accepted = false;
                    return;
                }
                if (SelectedSystem is null)
                {
                    e.Accepted = false;
                    return;
                }
                if (planet.System is null)
                {
                    e.Accepted = false;
                    return;
                }
                e.Accepted = planet.System.Equals(SelectedSystem.Code);
            };

        }
        OnPropertyChanged(nameof(PlanetListCVS));
    }

    private Planet? _SelectedPlanet = null;
    public Planet? SelectedPlanet
    {
        get => _SelectedPlanet;
        set
        {
            SetProperty(ref _SelectedPlanet, value);
            SetSatelliteListCVS();
        }
    }

    private IReadOnlyCollection<Satellite> _SatelliteList = new List<Satellite>();
    private IReadOnlyCollection<Satellite> SatelliteList
    {
        get => _SatelliteList;
        set => SetProperty(ref _SatelliteList, value);
    }

    private readonly CollectionViewSource _SatelliteListCVS = new CollectionViewSource();
    public ICollectionView SatelliteListCVS
    {
        get
        {
            return _SatelliteListCVS.View;
        }
    }
    private void SetSatelliteListCVS(bool resetSource = false)
    {
        var targetCVS = _SatelliteListCVS;
        if (targetCVS is null)
        {
            return;
        }

        using (targetCVS.DeferRefresh())
        {
            if (resetSource)
            {
                targetCVS.Source = SatelliteList;
                targetCVS.SortDescriptions.Clear();
                targetCVS.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            }

            if (targetCVS.Source is null)
            {
                targetCVS.Source = SatelliteList;
                targetCVS.SortDescriptions.Clear();
                targetCVS.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            }

            targetCVS.Filter += (s, e) =>
            {
                Satellite? satellite = e.Item as Satellite;
                if (satellite is null)
                {
                    e.Accepted = false;
                    return;
                }
                if (SelectedPlanet is null)
                {
                    e.Accepted = false;
                    return;
                }
                if (satellite.Planet is null)
                {
                    e.Accepted = false;
                    return;
                }
                e.Accepted = satellite.Planet.Equals(SelectedPlanet.Code);
            };

        }
        OnPropertyChanged(nameof(SatelliteListCVS));
    }

    private Satellite? _SelectedSatellite = null;
    public Satellite? SelectedSatellite
    {
        get => _SelectedSatellite;
        set
        {
            SetProperty(ref _SelectedSatellite, value);
            SetTradeportListCVS();
        }
    }

    private IReadOnlyCollection<Tradeport> _TradeportList = new List<Tradeport>();
    private IReadOnlyCollection<Tradeport> TradeportList
    {
        get => _TradeportList;
        set => SetProperty(ref _TradeportList, value);
    }

    private readonly CollectionViewSource _TradeportListCVS = new CollectionViewSource();
    public ICollectionView TradeportListCVS
    {
        get
        {
            return _TradeportListCVS.View;
        }
    }
    private void SetTradeportListCVS(bool resetSource = false)
    {
        var targetCVS = _TradeportListCVS;
        if (targetCVS is null)
        {
            return;
        }

        using (targetCVS.DeferRefresh())
        {
            if (resetSource)
            {
                targetCVS.Source = TradeportList;
                targetCVS.SortDescriptions.Clear();
                targetCVS.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            }

            if (targetCVS.Source is null)
            {
                targetCVS.Source = TradeportList;
                targetCVS.SortDescriptions.Clear();
                targetCVS.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            }

            targetCVS.Filter += (s, e) =>
            {
                Tradeport? tradeport = e.Item as Tradeport;
                if (tradeport is null)
                {
                    e.Accepted = false;
                    return;
                }
                if (SelectedSatellite is null)
                {
                    e.Accepted = false;
                    return;
                }
                if (tradeport.Satellite is null)
                {
                    e.Accepted = false;
                    return;
                }
                e.Accepted = tradeport.Satellite.Equals(SelectedSatellite.Code);
            };

        }
        OnPropertyChanged(nameof(TradeportListCVS));
    }

    private Tradeport? _SelectedTradeport = null;
    public Tradeport? SelectedTradeport
    {
        get => _SelectedTradeport;
        set
        {
            SetProperty(ref _SelectedTradeport, value);
            if(SelectedTradeport != null)
            {
                _ = SetCurrentTradeportAsync(SelectedTradeport.Code);
            }
        }
    }

    private Tradeport? _CurrentTradeport = null;
    public Tradeport? CurrentTradeport
    {
        get => _CurrentTradeport;
        set
        {
            SetProperty(ref _CurrentTradeport, value);
            if(CurrentTradeport != null)
            {
                SetCurrentTradeportBuyListCVS(true);
                SetCurrentTradeportSellListCVS(true);
            }
        }
    }

    private readonly CollectionViewSource _CurrentTradeportBuyListCVS = new CollectionViewSource();
    public ICollectionView CurrentTradeportBuyListCVS
    {
        get
        {
            return _CurrentTradeportBuyListCVS.View;
        }
    }
    private void SetCurrentTradeportBuyListCVS(bool resetSource = false)
    {
        var targetCVS = _CurrentTradeportBuyListCVS;
        if (targetCVS is null)
        {
            return;
        }

        using (targetCVS.DeferRefresh())
        {
            if (resetSource)
            {
                targetCVS.Source = CurrentTradeport?.Prices;
                targetCVS.SortDescriptions.Clear();
                targetCVS.SortDescriptions.Add(new SortDescription("Key", ListSortDirection.Ascending));
            }

            if (targetCVS.Source is null)
            {
                targetCVS.Source = CurrentTradeport?.Prices;
                targetCVS.SortDescriptions.Clear();
                targetCVS.SortDescriptions.Add(new SortDescription("Key", ListSortDirection.Ascending));
            }

            targetCVS.Filter += (s, e) =>
            {
                if(e.Item is KeyValuePair<string, TradeListing> == false)
                {
                    e.Accepted = false;
                    return;
                }

                TradeListing? tradeListing = ((KeyValuePair<string, TradeListing>)e.Item).Value;
                if (tradeListing is null)
                {
                    e.Accepted = false;
                    return;
                }
                e.Accepted = tradeListing.Operation.Equals(Domain.DataRunner.OperationType.Buy);
            };
        }
        OnPropertyChanged(nameof(CurrentTradeportBuyListCVS));
    }
    private readonly CollectionViewSource _CurrentTradeportSellListCVS = new CollectionViewSource();
    public ICollectionView CurrentTradeportSellListCVS
    {
        get
        {
            return _CurrentTradeportSellListCVS.View;
        }
    }
    private void SetCurrentTradeportSellListCVS(bool resetSource = false)
    {
        var targetCVS = _CurrentTradeportSellListCVS;
        if (targetCVS is null)
        {
            return;
        }

        using (targetCVS.DeferRefresh())
        {
            if (resetSource)
            {
                targetCVS.Source = CurrentTradeport?.Prices;
                targetCVS.SortDescriptions.Clear();
                targetCVS.SortDescriptions.Add(new SortDescription("Key", ListSortDirection.Ascending));
            }

            if (targetCVS.Source is null)
            {
                targetCVS.Source = CurrentTradeport?.Prices;
                targetCVS.SortDescriptions.Clear();
                targetCVS.SortDescriptions.Add(new SortDescription("Key", ListSortDirection.Ascending));
            }

            targetCVS.Filter += (s, e) =>
            {
                if (e.Item is KeyValuePair<string, TradeListing> == false)
                {
                    e.Accepted = false;
                    return;
                }

                TradeListing? tradeListing = ((KeyValuePair<string, TradeListing>)e.Item).Value;
                if (tradeListing is null)
                {
                    e.Accepted = false;
                    return;
                }
                e.Accepted = tradeListing.Operation.Equals(Domain.DataRunner.OperationType.Sell);
            };
        }
        OnPropertyChanged(nameof(CurrentTradeportSellListCVS));
    }

    public DataRunnerViewModel(IMessenger messenger, IUexDataService dataService)
    {
        IsEnabled = true;
        _Messenger = messenger;
        _DataService = dataService;

        _Messenger.Register<ShowUserInterfaceMessage>(this, ShowUserInterfaceMessageHandler);
        _Messenger.Register<CloseSettingsInterfaceMessage>(this, CloseSettingsInterfaceMessageHandler);
    }

    public ICommand ViewModelLoadedCommand => new RelayCommand<object>(async (sender) => await ViewModelLoadedCommandExecuteAsync(sender));
    public async Task ViewModelLoadedCommandExecuteAsync(object? sender)
    {
        SystemList = await _DataService.GetAllSystemsAsync();
        SelectedSystem = null;
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

    public async Task UpdatePlanetListAsync(string systemCode)
    {
        PlanetList = await _DataService.GetAllPlanetsAsync(systemCode);
        SetPlanetListCVS(true);
        SelectedPlanet = null;
        SatelliteList = await _DataService.GetAllSatellitesAsync(systemCode);
        SetSatelliteListCVS(true);
        TradeportList = await _DataService.GetAllTradeportsAsync(systemCode);
        SetTradeportListCVS(true);
    }
    public async Task SetCurrentTradeportAsync(string tradeportCode)
    {
        var newTradeport = await _DataService.GetTradeportAsync(tradeportCode);
        CurrentTradeport = newTradeport;
    }
}
