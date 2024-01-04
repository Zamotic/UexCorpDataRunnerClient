using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Interface.MessengerMessages;

namespace UexCorpDataRunner.Interface.DataRunnerV2;
public partial class DataRunnerV2ViewModel
{
    bool _IsViewModelLoaded = false;
    public IRelayCommand ViewModelLoadedCommand => new RelayCommand<object>(async (sender) => await ViewModelLoadedCommandExecuteAsync(sender));
    public async Task ViewModelLoadedCommandExecuteAsync(object? sender)
    {
        if(_IsViewModelLoaded == true)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(_SettingsService?.Settings?.UserAccessCode) == true)
        {
            ShowSettingsInterfaceCommandExecute();
            return;
        }

        try
        {
            SystemList = await _DataService.GetAllSystemsAsync();
            SelectedSystem = null;
            _commodityList = await _DataService.GetAllCommoditiesAsync();
            _IsViewModelLoaded = true;
            if (SystemList.Where(x => x.IsAvailable).Count() == 1)
            {
                SelectedSystem = SystemList.Where(x => x.IsAvailable).First();
            }

            //SetNotificationPanelText();
            IsSelectedTerminalFocused = true;
        }
        catch //(Exception ex)
        {
            ShowSettingsInterfaceCommandExecute();
        }
    }

    //private void SetNotificationPanelText(string? overrideText = null)
    //{
    //    if(string.IsNullOrWhiteSpace(overrideText) == false)
    //    {
    //        NotificationPanelText = $"* {overrideText} *";
    //        IsNotificationPanelVisible = true;
    //    }

    //    if (string.IsNullOrWhiteSpace(_SettingsService?.Settings?.SelectedGameVersion) == true) 
    //    {
    //        return;
    //    }

    //    if (_SettingsService.Settings.SelectedGameVersion.Equals(GameVersion.PtuValue))
    //    {
    //        NotificationPanelText = "* Reminder: You are currently loading data for the PTU. *";
    //        IsNotificationPanelVisible = true;
    //        return;
    //    }
    //    NotificationPanelText = string.Empty;
    //    IsNotificationPanelVisible = false;
    //}

    public IRelayCommand HideUserInterfaceCommand => new RelayCommand(HideUserInterfaceCommandExecute);
    private void HideUserInterfaceCommandExecute()
    {
        IsEnabled = false;
        _Messenger.Send(new HideUserInterfaceMessage());
    }

    public IRelayCommand ShowSettingsInterfaceCommand => new RelayCommand(ShowSettingsInterfaceCommandExecute);
    private void ShowSettingsInterfaceCommandExecute()
    {
        IsEnabled = false;
        _Messenger.Send(new ShowSettingsInterfaceMessage());
    }

    //public IRelayCommand MouseDoubleClickBehaviorCommand => new RelayCommand<object?>(MouseDoubleClickBehaviorCommandExecute);
    //private void MouseDoubleClickBehaviorCommandExecute(object? sender)
    //{
    //    if (sender is null)
    //    {
    //        return;
    //    }

    //    TextBox? textBox = sender as TextBox;
    //    if (textBox is null)
    //    {
    //        return;
    //    }
    //}

    public IRelayCommand ClearSelectedSystemCommand => new RelayCommand(ClearSelectedSystemCommandExecute, ClearSelectedSystemCommandCanExecute);
    private bool ClearSelectedSystemCommandCanExecute()
    {
        return SelectedSystem is not null;
    }
    private void ClearSelectedSystemCommandExecute()
    {
        //ClearSelectedPlanetCommandExecute();
        SelectedSystem = null;
    }

    public IRelayCommand ClearSelectedTerminalCommand => new RelayCommand(ClearSelectedTerminalCommandExecute, ClearSelectedTerminalCommandCanExecute);
    private bool ClearSelectedTerminalCommandCanExecute()
    {
        return SelectedTerminal is not null;
    }
    private void ClearSelectedTerminalCommandExecute()
    {
        SelectedTerminal = null;
        IsSelectedTerminalFocused = true;
    }

    //public IRelayCommand ClearSelectedPlanetCommand => new RelayCommand(ClearSelectedPlanetCommandExecute, ClearSelectedPlanetCommandCanExecute);
    //private bool ClearSelectedPlanetCommandCanExecute()
    //{
    //    return SelectedPlanet is not null;
    //}
    //private void ClearSelectedPlanetCommandExecute()
    //{
    //    ClearSelectedSatelliteCommandExecute();
    //    SelectedPlanet = null;
    //}

    //public IRelayCommand ClearSelectedSatelliteCommand => new RelayCommand(ClearSelectedSatelliteCommandExecute, ClearSelectedSatelliteCommandCanExecute);
    //private bool ClearSelectedSatelliteCommandCanExecute()
    //{
    //    return SelectedSatellite is not null;
    //}
    //private void ClearSelectedSatelliteCommandExecute()
    //{
    //    ClearSelectedTradeportCommandExecute();
    //    SelectedSatellite = null;
    //}

    //public IRelayCommand ClearSelectedTradeportCommand => new RelayCommand(ClearSelectedTradeportCommandExecute, ClearSelectedTradeportCommandCanExecute);
    //private bool ClearSelectedTradeportCommandCanExecute()
    //{
    //    return SelectedTradeport is not null;
    //}
    //private void ClearSelectedTradeportCommandExecute()
    //{
    //    SelectedTradeport = null;
    //    ClearCommodities();
    //}

    public IRelayCommand ResetCommoditiesCommand => new RelayCommand(ResetCommoditiesCommandExecute);
    private void ResetCommoditiesCommandExecute()
    {
        ClearCommodities();
        if(SelectedTerminal is not null)
        {
            _ = UpdateCommoditiesForTerminal(SelectedTerminal.Id);
        }
        SelectedTabItemIndex = 0;
        //OnPropertyChanged("Commodities");
    }

    public IRelayCommand SubmitCommoditiesCommand => new AsyncRelayCommand(SubmitCommoditiesCommandExecute, SubmitCommoditiesCommandCanExecute);
    private bool SubmitCommoditiesCommandCanExecute()
    {
        if (SelectedTerminal is null)
        {
            return false;
        }
        if (Commodities.Any() == false)
        {
            return false;
        }
        if (Commodities.All(x => x.CurrentPrice.HasValue == false))
        {
            return false;
        }
        return true;
    }

    public IRelayCommand MouseDoubleClickBehaviorDoubleClicked => new RelayCommand<object?>(MouseDoubleClickBehaviorDoubleClickedExecute);
    private void MouseDoubleClickBehaviorDoubleClickedExecute(object? dataContext)
    {
        if(dataContext is null)
        {
            return;
        }

        var context = dataContext as UexCorpDataRunner.Application.DataRunnerV2.CommodityWrapper;
        if (context is null)
        {
            return;
        }

        if (context.CurrentPrice is null)
        {
            context.CurrentPrice = context.ListedPrice;
            return;
        }

        if (context.CurrentPrice is not null)
        {
            context.CurrentPrice = null;
        }
    }


    //bool _UseMultipleReportSubmission = false;
    private async Task SubmitCommoditiesCommandExecute()
    {
        if (SelectedTerminal is null)
        {
            return;
        }

        var messageQueue = new System.Collections.Concurrent.ConcurrentQueue<string>();
        _Messenger.Send(new ShowTransmissionStatusMessage(messageQueue));

        await Task.Delay(250);

        var responses = await _DataSubmitter.SubmitAllReports(Commodities, SelectedTerminal.Id, messageQueue).ConfigureAwait(false);

        bool areThereAnyFailures = responses.Any(x => x.Value == false);

        string responseMessage = $"Transmission Summary: {responses.Count(x => x.Value == true)} Succeeded, {responses.Count(x => x.Value == false)} Failed";
        _Messenger.Send(new TransmissionStatusCompleteMessage(responseMessage, !areThereAnyFailures));

        ClearSelectedTerminalCommandExecute();
    }

    public void ClearCommodities()
    {
        Commodities = new System.Collections.ObjectModel.ObservableCollection<Application.DataRunnerV2.CommodityWrapper>();
    }
}


