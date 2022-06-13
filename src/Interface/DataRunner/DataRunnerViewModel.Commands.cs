using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using UexCorpDataRunner.Application.MessengerMessages;

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
        SelectedSystem = null;
        SelectedTradeport = null;
        SelectedSatellite = null;
        SelectedPlanet = null;
    }

    public ICommand ClearSelectedPlanetCommand => new RelayCommand(ClearSelectedPlanetCommandExecute, ClearSelectedPlanetCommandCanExecute);
    private bool ClearSelectedPlanetCommandCanExecute()
    {
        return SelectedPlanet is not null;
    }
    private void ClearSelectedPlanetCommandExecute()
    {
        SelectedTradeport = null;
        SelectedSatellite = null;
        SelectedPlanet = null;
    }

    public ICommand ClearSelectedSatelliteCommand => new RelayCommand(ClearSelectedSatelliteCommandExecute, ClearSelectedSatelliteCommandCanExecute);
    private bool ClearSelectedSatelliteCommandCanExecute()
    {
        return SelectedSatellite is not null;
    }
    private void ClearSelectedSatelliteCommandExecute()
    {
        SelectedTradeport = null;
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
    }
}


