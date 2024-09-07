using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Application.DataRunner;
using UexCorpDataRunner.Domain.Common;
using UexCorpDataRunner.Domain.DataRunner;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Interface.MessengerMessages;

namespace UexCorpDataRunner.Interface.DataRunner;

public partial class DataRunnerViewModel : ViewModelBase
{
    private readonly IMessenger _Messenger;
    private readonly IUexDataService _DataService;
    private readonly ISettingsService _SettingsService;
    private readonly IPriceReportSubmitter _PriceReportSubmitter;
    private readonly ITradeportCommodityBuilder _TradeportCommodityBuilder;

    private bool _IsNotificationPanelVisible = false;
    public bool IsNotificationPanelVisible { get => _IsNotificationPanelVisible; set => SetProperty(ref _IsNotificationPanelVisible, value); }
    private string _NotificationPanelText = string.Empty;
    public string NotificationPanelText { get => _NotificationPanelText; set => SetProperty(ref _NotificationPanelText, value); }

    private IReadOnlyCollection<Commodity>? _commodityList;

    private IReadOnlyCollection<Domain.DataRunner.StarSystem> _SystemList = new List<Domain.DataRunner.StarSystem>();
    public IReadOnlyCollection<Domain.DataRunner.StarSystem> SystemList
    {
        get => _SystemList;
        set
        {
            SetProperty(ref _SystemList, value);
        }
    }

    private Domain.DataRunner.StarSystem? _SelectedSystem = null;
    public Domain.DataRunner.StarSystem? SelectedSystem
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
    public IReadOnlyCollection<Planet> PlanetList
    {
        get => _PlanetList;
        set
        {
            SetProperty(ref _PlanetList, value);
        }
    }

    private Planet? _SelectedPlanet = null;
    public Planet? SelectedPlanet
    {
        get => _SelectedPlanet;
        set
        {
            SetProperty(ref _SelectedPlanet, value);
        }
    }

    private IReadOnlyCollection<Satellite> _SatelliteList = new List<Satellite>();
    public IReadOnlyCollection<Satellite> SatelliteList
    {
        get => _SatelliteList;
        set => SetProperty(ref _SatelliteList, value);
    }

    private Satellite? _SelectedSatellite = null;
    public Satellite? SelectedSatellite
    {
        get => _SelectedSatellite;
        set
        {
            SetProperty(ref _SelectedSatellite, value);
        }
    }

    private IReadOnlyCollection<Tradeport> _TradeportList = new List<Tradeport>();
    public IReadOnlyCollection<Tradeport> TradeportList
    {
        get => _TradeportList;
        set => SetProperty(ref _TradeportList, value);
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
                SelectedTabItemIndex = 0;
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
            //    //SetCurrentTradeportSellListCVS(trueS
            //}
        }
    }

    private IList<CommodityWrapper> _commodities = new List<CommodityWrapper>();
    public IList<CommodityWrapper> Commodities
    {
        get => _commodities;
        set
        {
            SetProperty(ref _commodities, value);
            //SetCommodityListsCVS(true);
        }
    }

    private int _SelectedTabItemIndex = 0;
    public int SelectedTabItemIndex
    {
        get => _SelectedTabItemIndex;
        set => SetProperty(ref _SelectedTabItemIndex, value);
    }

    private bool _IsSelectedTradeportFocused = false;
    public bool IsSelectedTradeportFocused
    {
        get => _IsSelectedTradeportFocused;
        set => SetProperty(ref _IsSelectedTradeportFocused, value);
    }

    public string? SelectedSearchStyle
    {
        get => _SettingsService?.Settings?.SelectedSearchStyle;
    }

    public DataRunnerViewModel(IMessenger messenger, IUexDataService dataService, ISettingsService settingsService, IPriceReportSubmitter priceReportSubmitter, ITradeportCommodityBuilder tradeportCommodityBuilder)
    {
        IsEnabled = true;
        _Messenger = messenger;
        _SettingsService = settingsService;
        _DataService = dataService;
        _PriceReportSubmitter = priceReportSubmitter;
        _TradeportCommodityBuilder = tradeportCommodityBuilder;

        _Messenger.Register<ShowUserInterfaceMessage>(this, ShowUserInterfaceMessageHandler);
        _Messenger.Register<CloseSettingsInterfaceMessage>(this, CloseSettingsInterfaceMessageHandler);
        _Messenger.Register<CloseTransmissionStatusMessage>(this, CloseTransmissionStatusMessageHandler);
    }  


    public void ShowUserInterfaceMessageHandler(object sender, ShowUserInterfaceMessage notification)
    {
        if (_SettingsService?.Settings?.SelectedSiteVersion == SiteVersion.Version2Value)
        {
            return;
        }

        IsEnabled = true;
    }

    public async void CloseSettingsInterfaceMessageHandler(object sender, CloseSettingsInterfaceMessage notification)
    {
        if (_SettingsService?.Settings?.SelectedSiteVersion == SiteVersion.Version2Value)
        {
            return;
        }
        if (notification != null)
        {
            if(notification.ShowTemporaryCommoditiesChanged == true)
            {
                await UpdateCommoditiesForTradeport(SelectedTradeport?.Code);
            }

            SetNotificationPanelText();
        }
        OnPropertyChanged(nameof(SelectedSearchStyle));
        IsEnabled = true;
    }

    public void CloseTransmissionStatusMessageHandler(object sender, CloseTransmissionStatusMessage notification)
    {
        if (_SettingsService?.Settings?.SelectedSiteVersion == SiteVersion.Version2Value)
        {
            return;
        }
        ClearSelectedTradeportCommandExecute();
        IsEnabled = true;
    }

    public async Task UpdatePlanetListAsync(string systemCode)
    {

        if(string.IsNullOrEmpty(systemCode))
        {
            return;
        }

        if(_IsViewModelLoaded == false)
        {
            return;
        }

        PlanetList = await _DataService.GetAllPlanetsAsync(systemCode);
        SatelliteList = await _DataService.GetAllSatellitesAsync(systemCode);
        TradeportList = await _DataService.GetAllTradeportsAsync(systemCode);
        //SelectedPlanet = null;
        //SelectedSatellite = null;
        //SelectedTradeport = null;
    }
    public async Task SetCurrentTradeportAsync(string tradeportCode)
    {
        var newTradeport = await _DataService.GetTradeportAsync(tradeportCode);
        CurrentTradeport = newTradeport;
    }

    public async Task UpdateCommoditiesForTradeport(string? tradeportCode)
    {
        if (string.IsNullOrWhiteSpace(tradeportCode) == true)
        {
            return;
        }

        if(_commodityList is null)
        {
            return;
        }

        ClearCommodities();

        IList<CommodityWrapper> commodities = await _TradeportCommodityBuilder.BuildCommodityListAsync(tradeportCode, _commodityList);

        Commodities = commodities;
    }
}
