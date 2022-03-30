using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.DesktopClient.Services;

namespace UexCorpDataRunner.DesktopClient.ViewModels;


public class HiddenInterfaceViewModel
{
    readonly INavigationService _NavigationService;
    public Command ShowUserInterfaceCommand => new Command(async () => await _NavigationService.NavigateToActiveInterface());

    public HiddenInterfaceViewModel(INavigationService navigationService)
    {
        _NavigationService = navigationService;
    }
}
