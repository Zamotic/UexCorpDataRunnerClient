using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Interface.MessengerMessages;
using UexCorpDataRunner.Domain.Services;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using UexCorpDataRunner.Domain.DataRunner;
using System.Windows.Data;
using UexCorpDataRunner.Application.DataRunner;
using System.Windows.Controls;

namespace UexCorpDataRunner.Interface.DataRunner;

public partial class DataRunnerViewModel : ViewModelBase
{
    public readonly IMessenger _Messenger;
    public readonly IUexDataService _DataService;

    private IReadOnlyCollection<Commodity>? _commodityList;

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

            targetCVS.Filter += (s, e) =>
            {
                Domain.DataRunner.System? system = e.Item as Domain.DataRunner.System;
                if (system is null)
                {
                    e.Accepted = false;
                    return;
                }
                e.Accepted = system.IsAvailable;
            };
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
                if (planet.IsAvailable is false)
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
            SetTradeportListCVS();
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
                if (SelectedPlanet is null)
                {
                    e.Accepted = false;
                    return;
                }
                Satellite? satellite = e.Item as Satellite;
                if (satellite is null)
                {
                    e.Accepted = false;
                    return;
                }
                if (satellite.Planet is null)
                {
                    e.Accepted = false;
                    return;
                }
                if (satellite.IsAvailable is false)
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
                if (SelectedPlanet is null)
                {
                    e.Accepted = false;
                    return;
                }
                Tradeport? tradeport = e.Item as Tradeport;
                if (tradeport is null)
                {
                    e.Accepted = false;
                    return;
                }
                if (SelectedSatellite is null)
                {
                    e.Accepted = ((tradeport.Planet?.Equals(SelectedPlanet.Code) == true) && (string.IsNullOrWhiteSpace(tradeport.Satellite) == true));
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
                //_ = SetCurrentTradeportAsync(SelectedTradeport.Code);
                _ = UpdateCommoditiesForTradeport(SelectedTradeport.Code);
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
            //if(CurrentTradeport != null)
            //{
            //    //SetCurrentTradeportBuyListCVS(true);
            //    //SetCurrentTradeportSellListCVS(true);
            //}
        }
    }

    private ObservableCollection<CommodityWrapper> _buyableCommodities = new ObservableCollection<CommodityWrapper>();
    public ObservableCollection<CommodityWrapper> BuyableCommodities
    {
        get => _buyableCommodities;
        set => SetProperty(ref _buyableCommodities, value);
    }
    private ObservableCollection<CommodityWrapper> _sellableCommodities = new ObservableCollection<CommodityWrapper>();
    public ObservableCollection<CommodityWrapper> SellableCommodities
    {
        get => _sellableCommodities;
        set => SetProperty(ref _sellableCommodities, value);
    }

    public DataRunnerViewModel(IMessenger messenger, IUexDataService dataService)
    {
        IsEnabled = true;
        _Messenger = messenger;
        _DataService = dataService;

        _Messenger.Register<ShowUserInterfaceMessage>(this, ShowUserInterfaceMessageHandler);
        _Messenger.Register<CloseSettingsInterfaceMessage>(this, CloseSettingsInterfaceMessageHandler);
        _Messenger.Register<CloseTransmissionStatusMessage>(this, CloseTransmissionStatusMessageHandler);
    }  


    public void ShowUserInterfaceMessageHandler(object sender, ShowUserInterfaceMessage notification)
    {
        IsEnabled = true;
    }

    public void CloseSettingsInterfaceMessageHandler(object sender, CloseSettingsInterfaceMessage notification)
    {
        IsEnabled = true;
    }

    public void CloseTransmissionStatusMessageHandler(object sender, CloseTransmissionStatusMessage notification)
    {
        ClearSelectedTradeportCommandExecute();
        IsEnabled = true;
    }

    public async Task UpdatePlanetListAsync(string systemCode)
    {
        if(string.IsNullOrEmpty(systemCode))
        {
            return;
        }

        if(_isViewModelLoaded == false)
        {
            return;
        }

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

    public async Task UpdateCommoditiesForTradeport(string tradeportCode)
    {
        if(_commodityList is null)
        {
            return;
        }

        BuyableCommodities.Clear();
        SellableCommodities.Clear();

        var currentTradeport = await _DataService.GetTradeportAsync(tradeportCode);
        foreach (var tradeListingValue in currentTradeport.Prices)
        {
            if(_commodityList.Any(x => x.Code.Equals(tradeListingValue.Code)) == false)
            {
                continue;
            }
            var locatedCommodity = _commodityList.First(x => x.Code.Equals(tradeListingValue.Code));

            if(tradeListingValue.Operation == OperationType.Buy)
            {
                BuyableCommodities.Add(new CommodityWrapper(locatedCommodity, tradeListingValue));
                continue;
            }

            SellableCommodities.Add(new CommodityWrapper(locatedCommodity, tradeListingValue));
        }
    }
}
