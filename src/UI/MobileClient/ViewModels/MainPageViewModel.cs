using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.DesktopClient.Services;

namespace UexCorpDataRunner.DesktopClient.ViewModels
{
    public class MainPageViewModel
    {
        readonly INavigationService _NavigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            _NavigationService = navigationService;
        }
    }
}
