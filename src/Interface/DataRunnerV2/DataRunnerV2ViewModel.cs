using CommunityToolkit.Mvvm.Messaging;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Application.DataRunner;
using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Interface.MessengerMessages;

namespace UexCorpDataRunner.Interface.DataRunner;

public partial class DataRunnerV2ViewModel : ViewModelBase
{
    private readonly IMessenger _Messenger;
    private readonly IUexDataServiceV2 _DataService;
    private readonly ISettingsService _SettingsService;
    //private readonly IPriceReportSubmitter _PriceReportSubmitter;
    //private readonly ITradeportCommodityBuilder _TradeportCommodityBuilder;

    //private bool _IsNotificationPanelVisible = false;
    //public bool IsNotificationPanelVisible { get => _IsNotificationPanelVisible; set => SetProperty(ref _IsNotificationPanelVisible, value); }
    //private string _NotificationPanelText = string.Empty;
    //public string NotificationPanelText { get => _NotificationPanelText; set => SetProperty(ref _NotificationPanelText, value); }

    //private IReadOnlyCollection<Commodity>? _commodityList;

    private IReadOnlyCollection<StarSystem> _SystemList = new List<StarSystem>();
    public IReadOnlyCollection<StarSystem> SystemList
    {
        get => _SystemList;
        set
        {
            SetProperty(ref _SystemList, value);
            //SetSystemListCVS(true);
        }
    }

    private StarSystem? _SelectedSystem = null;
    public StarSystem? SelectedSystem
    {
        get => _SelectedSystem;
        set
        {
            SetProperty(ref _SelectedSystem, value);
            if (SelectedSystem != null)
            {
                _ = UpdateTerminalListAsync(SelectedSystem.Id);
            }
        }
    }

    private IReadOnlyCollection<Terminal> _TerminalList = new List<Terminal>();
    public IReadOnlyCollection<Terminal> TerminalList
    {
        get => _TerminalList;
        set
        {
            SetProperty(ref _TerminalList, value);
        }
    }

    //private IReadOnlyCollection<Planet> _PlanetList = new List<Planet>();
    //public IReadOnlyCollection<Planet> PlanetList
    //{
    //    get => _PlanetList;
    //    set
    //    {
    //        SetProperty(ref _PlanetList, value);
    //    }
    //}

    //private readonly CollectionViewSource _PlanetListCVS = new CollectionViewSource();
    //public ICollectionView PlanetListCVS
    //{
    //    get
    //    {
    //        return _PlanetListCVS.View;
    //    }
    //}
    //private void SetPlanetListCVS(bool resetSource = false)
    //{
    //    var targetCVS = _PlanetListCVS;
    //    if (targetCVS is null)
    //    {
    //        return;
    //    }

    //    using (targetCVS.DeferRefresh())
    //    {
    //        if (resetSource)
    //        {
    //            targetCVS.Source = PlanetList;
    //            targetCVS.SortDescriptions.Clear();
    //            targetCVS.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
    //        }

    //        if (targetCVS.Source is null)
    //        {
    //            targetCVS.Source = PlanetList;
    //            targetCVS.SortDescriptions.Clear();
    //            targetCVS.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
    //        }

    //        targetCVS.Filter += (s, e) =>
    //        {
    //            Planet? planet = e.Item as Planet;
    //            if (planet is null)
    //            {
    //                e.Accepted = false;
    //                return;
    //            }
    //            if (SelectedSystem is null)
    //            {
    //                e.Accepted = false;
    //                return;
    //            }
    //            if (planet.System is null)
    //            {
    //                e.Accepted = false;
    //                return;
    //            }
    //            if (planet.IsAvailable is false)
    //            {
    //                e.Accepted = false;
    //                return;
    //            }
    //            e.Accepted = planet.System.Equals(SelectedSystem.Code);
    //        };

    //    }
    //    OnPropertyChanged(nameof(PlanetListCVS));
    //}

    //private Planet? _SelectedPlanet = null;
    //public Planet? SelectedPlanet
    //{
    //    get => _SelectedPlanet;
    //    set
    //    {
    //        SetProperty(ref _SelectedPlanet, value);
    //        //SetSatelliteListCVS();
    //        //SetTradeportListCVS();
    //    }
    //}

    //private IReadOnlyCollection<Satellite> _SatelliteList = new List<Satellite>();
    //public IReadOnlyCollection<Satellite> SatelliteList
    //{
    //    get => _SatelliteList;
    //    set => SetProperty(ref _SatelliteList, value);
    //}

    //private readonly CollectionViewSource _SatelliteListCVS = new CollectionViewSource();
    //public ICollectionView SatelliteListCVS
    //{
    //    get
    //    {
    //        return _SatelliteListCVS.View;
    //    }
    //}
    //private void SetSatelliteListCVS(bool resetSource = false)
    //{
    //    var targetCVS = _SatelliteListCVS;
    //    if (targetCVS is null)
    //    {
    //        return;
    //    }

    //    using (targetCVS.DeferRefresh())
    //    {
    //        if (resetSource)
    //        {
    //            targetCVS.Source = SatelliteList;
    //            targetCVS.SortDescriptions.Clear();
    //            targetCVS.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
    //        }

    //        if (targetCVS.Source is null)
    //        {
    //            targetCVS.Source = SatelliteList;
    //            targetCVS.SortDescriptions.Clear();
    //            targetCVS.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
    //        }

    //        targetCVS.Filter += (s, e) =>
    //        {
    //            if (SelectedPlanet is null)
    //            {
    //                e.Accepted = false;
    //                return;
    //            }
    //            Satellite? satellite = e.Item as Satellite;
    //            if (satellite is null)
    //            {
    //                e.Accepted = false;
    //                return;
    //            }
    //            if (satellite.Planet is null)
    //            {
    //                e.Accepted = false;
    //                return;
    //            }
    //            if (satellite.IsAvailable is false)
    //            {
    //                e.Accepted = false;
    //                return;
    //            }
    //            e.Accepted = satellite.Planet.Equals(SelectedPlanet.Code);
    //        };

    //    }
    //    OnPropertyChanged(nameof(SatelliteListCVS));
    //}

    //private Satellite? _SelectedSatellite = null;
    //public Satellite? SelectedSatellite
    //{
    //    get => _SelectedSatellite;
    //    set
    //    {
    //        SetProperty(ref _SelectedSatellite, value);
    //        //SetTradeportListCVS();
    //    }
    //}

    //private IReadOnlyCollection<Tradeport> _TradeportList = new List<Tradeport>();
    //public IReadOnlyCollection<Tradeport> TradeportList
    //{
    //    get => _TradeportList;
    //    set => SetProperty(ref _TradeportList, value);
    //}

    //private readonly CollectionViewSource _TradeportListCVS = new CollectionViewSource();
    //public ICollectionView TradeportListCVS
    //{
    //    get
    //    {
    //        return _TradeportListCVS.View;
    //    }
    //}
    //private void SetTradeportListCVS(bool resetSource = false)
    //{
    //    var targetCVS = _TradeportListCVS;
    //    if (targetCVS is null)
    //    {
    //        return;
    //    }

    //    using (targetCVS.DeferRefresh())
    //    {
    //        if (resetSource)
    //        {
    //            targetCVS.Source = TradeportList;
    //            targetCVS.SortDescriptions.Clear();
    //            targetCVS.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
    //        }

    //        if (targetCVS.Source is null)
    //        {
    //            targetCVS.Source = TradeportList;
    //            targetCVS.SortDescriptions.Clear();
    //            targetCVS.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
    //        }

    //        targetCVS.Filter += (s, e) =>
    //        {
    //            if (SelectedPlanet is null)
    //            {
    //                e.Accepted = false;
    //                return;
    //            }
    //            Tradeport? tradeport = e.Item as Tradeport;
    //            if (tradeport is null)
    //            {
    //                e.Accepted = false;
    //                return;
    //            }
    //            if (SelectedSatellite is null)
    //            {
    //                e.Accepted = ((tradeport.Planet?.Equals(SelectedPlanet.Code) == true) && (string.IsNullOrWhiteSpace(tradeport.Satellite) == true));
    //                return;
    //            }
    //            if (tradeport.Satellite is null)
    //            {
    //                e.Accepted = false;
    //                return;
    //            }
    //            e.Accepted = tradeport.Satellite.Equals(SelectedSatellite.Code);
    //        };

    //    }
    //    OnPropertyChanged(nameof(TradeportListCVS));
    //}

    //private Tradeport? _SelectedTradeport = null;
    //public Tradeport? SelectedTradeport
    //{
    //    get => _SelectedTradeport;
    //    set
    //    {
    //        SetProperty(ref _SelectedTradeport, value);
    //        if(SelectedTradeport != null)
    //        {
    //            //_ = SetCurrentTradeportAsync(SelectedTradeport.Code);
    //            _ = UpdateCommoditiesForTradeport(SelectedTradeport.Code);
    //            SelectedTabItemIndex = 0;
    //        }
    //    }
    //}

    //private Tradeport? _CurrentTradeport = null;
    //public Tradeport? CurrentTradeport
    //{
    //    get => _CurrentTradeport;
    //    set
    //    {
    //        SetProperty(ref _CurrentTradeport, value);
    //        //if(CurrentTradeport != null)
    //        //{
    //        //    //SetCurrentTradeportBuyListCVS(true);
    //        //    //SetCurrentTradeportSellListCVS(trueS
    //        //}
    //    }
    //}

    //private IList<CommodityWrapper> _commodities = new List<CommodityWrapper>();
    //public IList<CommodityWrapper> Commodities
    //{
    //    get => _commodities;
    //    set
    //    {
    //        SetProperty(ref _commodities, value);
    //        //SetCommodityListsCVS(true);
    //    }
    //}
    //private ObservableCollection<CommodityWrapper> _buyableCommodities = new ObservableCollection<CommodityWrapper>();
    //public ObservableCollection<CommodityWrapper> BuyableCommodities
    //{
    //    get => _buyableCommodities;
    //    set => SetProperty(ref _buyableCommodities, value);
    //}
    //private ObservableCollection<CommodityWrapper> _sellableCommodities = new ObservableCollection<CommodityWrapper>();
    //public ObservableCollection<CommodityWrapper> SellableCommodities
    //{
    //    get => _sellableCommodities;
    //    set => SetProperty(ref _sellableCommodities, value);
    //}
    //private readonly CollectionViewSource _buyableCommodityListCVS = new CollectionViewSource();
    //public ICollectionView BuyableCommodityListCVS
    //{
    //    get
    //    {
    //        return _buyableCommodityListCVS.View;
    //    }
    //}
    //private readonly CollectionViewSource _sellableCommodityListCVS = new CollectionViewSource();
    //public ICollectionView SellableCommodityListCVS
    //{
    //    get
    //    {
    //        return _sellableCommodityListCVS.View;
    //    }
    //}
    //private void SetCommodityListsCVS(bool resetSource = false)
    //{
    //    var targetBuyCVS = _buyableCommodityListCVS;
    //    var targetSellCVS = _sellableCommodityListCVS;
    //    if (targetBuyCVS is null)
    //    {
    //        return;
    //    }
    //    if (targetSellCVS is null)
    //    {
    //        return;
    //    }

    //    using (targetBuyCVS.DeferRefresh())
    //    {
    //        if (resetSource)
    //        {
    //            targetBuyCVS.Source = Commodities;
    //            targetBuyCVS.SortDescriptions.Clear();
    //            //targetBuyCVS.SortDescriptions.Add(new SortDescription(nameof(CommodityWrapper.Kind), ListSortDirection.Ascending));
    //            targetBuyCVS.SortDescriptions.Add(new SortDescription(nameof(CommodityWrapper.Name), ListSortDirection.Ascending));
    //        }

    //        if (targetBuyCVS.Source is null)
    //        {
    //            targetBuyCVS.Source = TradeportList;
    //            targetBuyCVS.SortDescriptions.Clear();
    //            //targetBuyCVS.SortDescriptions.Add(new SortDescription(nameof(CommodityWrapper.Kind), ListSortDirection.Ascending));
    //            targetBuyCVS.SortDescriptions.Add(new SortDescription(nameof(CommodityWrapper.Name), ListSortDirection.Ascending));
    //        }

    //        targetBuyCVS.Filter += (s, e) =>
    //        {
    //            CommodityWrapper? commodity = e.Item as CommodityWrapper;
    //            if (commodity is null)
    //            {
    //                e.Accepted = false;
    //                return;
    //            }
    //            e.Accepted = commodity.Operation == OperationType.Buy;
    //        };
    //    }

    //    using (targetSellCVS.DeferRefresh())
    //    {
    //        if (resetSource)
    //        {
    //            targetSellCVS.Source = Commodities;
    //            targetSellCVS.SortDescriptions.Clear();
    //            targetSellCVS.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
    //        }

    //        if (targetSellCVS.Source is null)
    //        {
    //            targetSellCVS.Source = TradeportList;
    //            targetSellCVS.SortDescriptions.Clear();
    //            targetSellCVS.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
    //        }

    //        targetSellCVS.Filter += (s, e) =>
    //        {
    //            CommodityWrapper? commodity = e.Item as CommodityWrapper;
    //            if (commodity is null)
    //            {
    //                e.Accepted = false;
    //                return;
    //            }
    //            e.Accepted = commodity.Operation == OperationType.Sell;
    //        };
    //    }
    //    OnPropertyChanged(nameof(BuyableCommodityListCVS));
    //    OnPropertyChanged(nameof(SellableCommodityListCVS));
    //}

    //private int _SelectedTabItemIndex = 0;
    //public int SelectedTabItemIndex
    //{
    //    get => _SelectedTabItemIndex;
    //    set => SetProperty(ref _SelectedTabItemIndex, value);
    //}

    public DataRunnerV2ViewModel(IMessenger messenger, IUexDataServiceV2 dataService, ISettingsService settingsService)//, IPriceReportSubmitter priceReportSubmitter, ITradeportCommodityBuilder tradeportCommodityBuilder)
    {
        IsEnabled = true;
        _Messenger = messenger;
        _SettingsService = settingsService;
        _DataService = dataService;
        //_PriceReportSubmitter = priceReportSubmitter;
        //_TradeportCommodityBuilder = tradeportCommodityBuilder;

        _Messenger.Register<ShowUserInterfaceMessage>(this, ShowUserInterfaceMessageHandler);
        _Messenger.Register<CloseSettingsInterfaceMessage>(this, CloseSettingsInterfaceMessageHandler);
        _Messenger.Register<CloseTransmissionStatusMessage>(this, CloseTransmissionStatusMessageHandler);
    }  

    public void ShowUserInterfaceMessageHandler(object sender, ShowUserInterfaceMessage notification)
    {
        IsEnabled = true;
    }

    public async void CloseSettingsInterfaceMessageHandler(object sender, CloseSettingsInterfaceMessage notification)
    {
        if (notification != null)
        {
            //    if(notification.ShowTemporaryCommoditiesChanged == true)
            //    {
            //        await UpdateCommoditiesForTradeport(SelectedTradeport?.Code);
            //    }

            //SetNotificationPanelText();
        }
        IsEnabled = true;
    }

    public void CloseTransmissionStatusMessageHandler(object sender, CloseTransmissionStatusMessage notification)
    {
        //ClearSelectedTradeportCommandExecute();
        IsEnabled = true;
    }

    public async Task UpdateTerminalListAsync(int starSystemId)
    {
        if (starSystemId < 1)
        {
            return;
        }

        if (_IsViewModelLoaded == false)
        {
            return;
        }

        TerminalList = await _DataService.GetAllTerminalsAsync(starSystemId);
        //SetPlanetListCVS(true);
        //SelectedPlanet = null;
        //SatelliteList = await _DataService.GetAllSatellitesAsync(systemCode);
        ////SetSatelliteListCVS(true);
        //TradeportList = await _DataService.GetAllTradeportsAsync(systemCode);
        //SetTradeportListCVS(true);
    }

    //public async Task UpdatePlanetListAsync(string systemCode)
    //{
    //    if(string.IsNullOrEmpty(systemCode))
    //    {
    //        return;
    //    }

    //    if(_IsViewModelLoaded == false)
    //    {
    //        return;
    //    }

    //    PlanetList = await _DataService.GetAllPlanetsAsync(systemCode);
    //    //SetPlanetListCVS(true);
    //    SelectedPlanet = null;
    //    SatelliteList = await _DataService.GetAllSatellitesAsync(systemCode);
    //    //SetSatelliteListCVS(true);
    //    TradeportList = await _DataService.GetAllTradeportsAsync(systemCode);
    //    //SetTradeportListCVS(true);
    //}
    //public async Task SetCurrentTradeportAsync(string tradeportCode)
    //{
    //    var newTradeport = await _DataService.GetTradeportAsync(tradeportCode);
    //    CurrentTradeport = newTradeport;
    //}

    //public async Task UpdateCommoditiesForTradeport(string? tradeportCode)
    //{
    //    if (string.IsNullOrWhiteSpace(tradeportCode) == true)
    //    {
    //        return;
    //    }

    //    if(_commodityList is null)
    //    {
    //        return;
    //    }

    //    ClearCommodities();

    //    IList<CommodityWrapper> commodities = await _TradeportCommodityBuilder.BuildCommodityListAsync(tradeportCode, _commodityList);

    //    Commodities = commodities;
    //}
}
