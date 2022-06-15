using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using UexCorpDataRunner.Interface.MessengerMessages;

namespace UexCorpDataRunner.Interface.DataRunner;
public partial class DataRunnerViewModel
{
    bool _isViewModelLoaded = false;
    public ICommand ViewModelLoadedCommand => new RelayCommand<object>(async (sender) => await ViewModelLoadedCommandExecuteAsync(sender));
    public async Task ViewModelLoadedCommandExecuteAsync(object? sender)
    {
        SystemList = await _DataService.GetAllSystemsAsync();
        SelectedSystem = null;
        _commodityList = await _DataService.GetAllCommoditiesAsync();
        _isViewModelLoaded = true;
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

    public ICommand MouseDoubleClickBehaviorCommand => new RelayCommand<object?>(MouseDoubleClickBehaviorCommandExecute);
    private void MouseDoubleClickBehaviorCommandExecute(object? sender)
    {
        if (sender is null)
        {
            return;
        }

        TextBox? textBox = sender as TextBox;
        if (textBox is null)
        {
            return;
        }
    }

    public ICommand ClearSelectedSystemCommand => new RelayCommand(ClearSelectedSystemCommandExecute, ClearSelectedSystemCommandCanExecute);
    private bool ClearSelectedSystemCommandCanExecute()
    {
        return SelectedSystem is not null;
    }
    private void ClearSelectedSystemCommandExecute()
    {
        ClearSelectedPlanetCommandExecute();
        SelectedSystem = null;
    }

    public ICommand ClearSelectedPlanetCommand => new RelayCommand(ClearSelectedPlanetCommandExecute, ClearSelectedPlanetCommandCanExecute);
    private bool ClearSelectedPlanetCommandCanExecute()
    {
        return SelectedPlanet is not null;
    }
    private void ClearSelectedPlanetCommandExecute()
    {
        ClearSelectedSatelliteCommandExecute();
        SelectedPlanet = null;
    }

    public ICommand ClearSelectedSatelliteCommand => new RelayCommand(ClearSelectedSatelliteCommandExecute, ClearSelectedSatelliteCommandCanExecute);
    private bool ClearSelectedSatelliteCommandCanExecute()
    {
        return SelectedSatellite is not null;
    }
    private void ClearSelectedSatelliteCommandExecute()
    {
        ClearSelectedTradeportCommandExecute();
        SelectedSatellite = null;
    }

    public ICommand ClearSelectedTradeportCommand => new RelayCommand(ClearSelectedTradeportCommandExecute, ClearSelectedTradeportCommandCanExecute);
    private bool ClearSelectedTradeportCommandCanExecute()
    {
        return SelectedTradeport is not null;
    }
    private void ClearSelectedTradeportCommandExecute()
    {
        SelectedTradeport = null;
        ResetCommoditiesCommandExecute();
    }

    public ICommand ResetCommoditiesCommand => new RelayCommand(ResetCommoditiesCommandExecute);
    private void ResetCommoditiesCommandExecute()
    {
        BuyableCommodities.Clear();
        SellableCommodities.Clear();
    }

    public ICommand SubmitCommoditiesCommand => new RelayCommand(SubmitCommoditiesCommandExecute, SubmitCommoditiesCommandCanExecute);
    private bool SubmitCommoditiesCommandCanExecute()
    {
        if (SelectedTradeport is null)
        {
            return false;
        }
        if (BuyableCommodities.Any() == false)
        {
            return false;
        }
        if (SellableCommodities.Any() == false)
        {
            return false;
        }
        if (BuyableCommodities.All(x => x.CurrentPrice.HasValue == false))
        {
            if (SellableCommodities.All(x => x.CurrentPrice.HasValue == false))
            {
                return false;
            }
        }
        return true;
    }
    private void SubmitCommoditiesCommandExecute()
    {
        _Messenger.Send(new ShowTransmissionStatusMessage(BuyableCommodities, SellableCommodities));
    }
}


