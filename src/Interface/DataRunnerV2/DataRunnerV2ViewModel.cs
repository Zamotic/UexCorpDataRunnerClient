using CommunityToolkit.Mvvm.Messaging;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Application.DataRunnerV2;
using UexCorpDataRunner.Domain.Common;
using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Interface.MessengerMessages;

namespace UexCorpDataRunner.Interface.DataRunnerV2;

public partial class DataRunnerV2ViewModel : ViewModelBase
{
    private readonly IMessenger _Messenger;
    private readonly IUexDataServiceV2 _DataService;
    private readonly ISettingsService _SettingsService;
    private readonly IDataSubmitter _DataSubmitter;
    private readonly ITerminalCommodityBuilder _TerminalCommodityBuilder;

    private bool _IsSelectedTerminalFocused = false;
    public bool IsSelectedTerminalFocused
    {
        get => _IsSelectedTerminalFocused;
        set => SetProperty(ref _IsSelectedTerminalFocused, value);
    }

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
                _ = UpdatePlanetListAsync(SelectedSystem.Id);
                _ = UpdateTerminalListAsync(SelectedSystem.Id);
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
            //SetSystemListCVS(true);
        }
    }

    private Planet? _SelectedPlanet = null;
    public Planet? SelectedPlanet
    {
        get => _SelectedPlanet;
        set
        {
            SetProperty(ref _SelectedPlanet, value);
            if (SelectedPlanet != null) 
            { 
                SelectedMoon = null;
                SelectedSpaceStation = null;
                SelectedOutpost = null;
                SelectedCity = null;
            }
        }
    }

    private IReadOnlyCollection<Moon> _MoonList = new List<Moon>();
    public IReadOnlyCollection<Moon> MoonList
    {
        get => _MoonList;
        set
        {
            SetProperty(ref _MoonList, value);
        }
    }

    private Moon? _SelectedMoon = null;
    public Moon? SelectedMoon
    {
        get => _SelectedMoon;
        set
        {
            SetProperty(ref _SelectedMoon, value);
        }
    }

    private IReadOnlyCollection<SpaceStation> _SpaceStationList = new List<SpaceStation>();
    public IReadOnlyCollection<SpaceStation> SpaceStationList
    {
        get => _SpaceStationList;
        set
        {
            SetProperty(ref _SpaceStationList, value);
        }
    }

    private SpaceStation? _SelectedSpaceStation = null;
    public SpaceStation? SelectedSpaceStation
    {
        get => _SelectedSpaceStation;
        set
        {
            SetProperty(ref _SelectedSpaceStation, value);
        }
    }

    private IReadOnlyCollection<Outpost> _OutpostList = new List<Outpost>();
    public IReadOnlyCollection<Outpost> OutpostList
    {
        get => _OutpostList;
        set
        {
            SetProperty(ref _OutpostList, value);
        }
    }

    private Outpost? _SelectedOutpost = null;
    public Outpost? SelectedOutpost
    {
        get => _SelectedOutpost;
        set
        {
            SetProperty(ref _SelectedOutpost, value);
        }
    }

    private IReadOnlyCollection<City> _CityList = new List<City>();
    public IReadOnlyCollection<City> CityList
    {
        get => _CityList;
        set
        {
            SetProperty(ref _CityList, value);
        }
    }

    private City? _SelectedCity = null;
    public City? SelectedCity
    {
        get => _SelectedCity;
        set
        {
            SetProperty(ref _SelectedCity, value);
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

    private IList<CommodityWrapper> _commodities = new List<CommodityWrapper>();
    public IList<CommodityWrapper> Commodities
    {
        get => _commodities;
        set
        {
            SetProperty(ref _commodities, value);
        }
    }

    private IList<Status> _StatusList = new List<Status>()
    {
        new Status() { Id = 0, Name = "0 - No Value Submitted" },
        new Status() { Id = 1, Name = "1 - Out of Stock" },
        new Status() { Id = 2, Name = "2 - Very Low" },
        new Status() { Id = 3, Name = "3 - Low" },
        new Status() { Id = 4, Name = "4 - Medium" },
        new Status() { Id = 5, Name = "5 - High" },
        new Status() { Id = 6, Name = "6 - Very High" },
        new Status() { Id = 7, Name = "7 - Max Inventory" },
    };
    public IList<Status> StatusList
    {
        get => _StatusList;
        //set
        //{
        //    SetProperty(ref _StatusList, value);
        //}
    }

    private int _SelectedTabItemIndex = 0;
    public int SelectedTabItemIndex
    {
        get => _SelectedTabItemIndex;
        set => SetProperty(ref _SelectedTabItemIndex, value);
    }

    public string? SelectedSearchStyle
    {
        get => _SettingsService?.Settings?.SelectedSearchStyle;
    }

    public DataRunnerV2ViewModel(IMessenger messenger, IUexDataServiceV2 dataService, ISettingsService settingsService, IDataSubmitter dataSubmitter, ITerminalCommodityBuilder terminalCommodityBuilder)
    {
        IsEnabled = true;
        _Messenger = messenger;
        _SettingsService = settingsService;
        _DataService = dataService;
        _DataSubmitter = dataSubmitter;
        _TerminalCommodityBuilder = terminalCommodityBuilder;

        _Messenger.Register<ShowUserInterfaceMessage>(this, ShowUserInterfaceMessageHandler);
        _Messenger.Register<CloseSettingsInterfaceMessage>(this, CloseSettingsInterfaceMessageHandler);
        _Messenger.Register<CloseTransmissionStatusMessage>(this, CloseTransmissionStatusMessageHandler);
    }  

    public void ShowUserInterfaceMessageHandler(object sender, ShowUserInterfaceMessage notification)
    {
        IsEnabled = false;
    }

    public async void CloseSettingsInterfaceMessageHandler(object sender, CloseSettingsInterfaceMessage notification)
    {
        if(_SettingsService?.Settings?.SelectedSiteVersion == SiteVersion.Version1Value)
        {
            return;
        }

        if (notification != null)
        {
            //    if(notification.ShowTemporaryCommoditiesChanged == true)
            //    {
            //        await UpdateCommoditiesForTradeport(SelectedTradeport?.Code);
            //    }

            //SetNotificationPanelText();
        }
        OnPropertyChanged(nameof(SelectedSearchStyle));
        IsEnabled = true;
    }

    public void CloseTransmissionStatusMessageHandler(object sender, CloseTransmissionStatusMessage notification)
    {
        if (_SettingsService?.Settings?.SelectedSiteVersion == SiteVersion.Version1Value)
        {
            return;
        }
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
    }

    public async Task UpdatePlanetListAsync(int starSystemId)
    {
        if (starSystemId < 1)
        {
            return;
        }

        if (_IsViewModelLoaded == false)
        {
            return;
        }

        PlanetList = await _DataService.GetAllPlanetsAsync(starSystemId);
        MoonList = await _DataService.GetAllMoonsByStarSystemIdAsync(starSystemId);
        SpaceStationList = await _DataService.GetAllSpaceStationsByStarSystemIdAsync(starSystemId);
        OutpostList = await _DataService.GetAllOutpostsByStarSystemIdAsync(starSystemId);
        CityList = await _DataService.GetAllCitiesByStarSystemIdAsync(starSystemId);

        SelectedPlanet = null;
        SelectedMoon = null;
        SelectedSpaceStation = null;
        SelectedOutpost = null;
        SelectedCity = null;
    }
    //public async Task SetCurrentTradeportAsync(string tradeportCode)
    //{
    //    var newTradeport = await _DataService.GetTradeportAsync(tradeportCode);
    //    CurrentTradeport = newTradeport;
    //}
}
