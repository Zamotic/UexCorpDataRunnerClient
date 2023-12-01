using CommunityToolkit.Mvvm.Messaging;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Application.DataRunnerV2;
using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Interface.MessengerMessages;
using System.Linq;

namespace UexCorpDataRunner.Interface.DataRunnerV2;

public partial class DataRunnerV2ViewModel : ViewModelBase
{
    private readonly IMessenger _Messenger;
    private readonly IUexDataServiceV2 _DataService;
    private readonly ISettingsService _SettingsService;
    //private readonly IPriceReportSubmitter _PriceReportSubmitter;
    private readonly ITerminalCommodityBuilder _TerminalCommodityBuilder;

    private bool _IsSelectedTerminalFocused = false;
    public bool IsSelectedTerminalFocused
    {
        get => _IsSelectedTerminalFocused;
        set => SetProperty(ref _IsSelectedTerminalFocused, value);
    }

    //private bool _IsNotificationPanelVisible = false;
    //public bool IsNotificationPanelVisible { get => _IsNotificationPanelVisible; set => SetProperty(ref _IsNotificationPanelVisible, value); }
    //private string _NotificationPanelText = string.Empty;
    //public string NotificationPanelText { get => _NotificationPanelText; set => SetProperty(ref _NotificationPanelText, value); }

    private IReadOnlyCollection<Commodity>? _commodityList;

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

    bool _SettingTerminalList = false;
    private IReadOnlyCollection<Terminal> _TerminalList = new List<Terminal>();
    public IReadOnlyCollection<Terminal> TerminalList
    {
        get => _TerminalList;
        set
        {
            _SettingTerminalList = true;
            SetProperty(ref _TerminalList, value);
            _SettingTerminalList = false;
        }
    }

    private Terminal? _SelectedTerminal = null;
    public Terminal? SelectedTerminal
    {
        get => _SelectedTerminal;
        set
        {
            SetProperty(ref _SelectedTerminal, value);
            if (SelectedTerminal is not null)
            {
                _ = UpdateCommoditiesForTerminal(SelectedTerminal.Id);
                SelectedTabItemIndex = 0;
            }
            if (SelectedTerminal is null)
            {
                ClearCommodities();
            }
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

    private IList<CommodityWrapper> _commodities = new List<CommodityWrapper>();
    public IList<CommodityWrapper> Commodities
    {
        get => _commodities;
        set
        {
            SetProperty(ref _commodities, value);
        }
    }

    private int _SelectedTabItemIndex = 0;
    public int SelectedTabItemIndex
    {
        get => _SelectedTabItemIndex;
        set => SetProperty(ref _SelectedTabItemIndex, value);
    }

    public DataRunnerV2ViewModel(IMessenger messenger, IUexDataServiceV2 dataService, ISettingsService settingsService/*, IPriceReportSubmitter priceReportSubmitter*/, ITerminalCommodityBuilder terminalCommodityBuilder)
    {
        IsEnabled = true;
        _Messenger = messenger;
        _SettingsService = settingsService;
        _DataService = dataService;
        //_PriceReportSubmitter = priceReportSubmitter;
        _TerminalCommodityBuilder = terminalCommodityBuilder;

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

        TerminalList = await _DataService.GetTerminalsAsync(starSystemId);
        SelectedTerminal = null;
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
}
