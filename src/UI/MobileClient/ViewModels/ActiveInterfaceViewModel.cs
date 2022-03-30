using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.DesktopClient.Services;

namespace UexCorpDataRunner.DesktopClient.ViewModels
{
    public class ActiveInterfaceViewModel
    {
        readonly INavigationService _NavigationService;
        public Command HideUserInterfaceCommand => new Command(async () => await _NavigationService.NavigateToHiddenInterface());

        //public Command TestCommand => new Command(TestCommandExecute, TestCommandCanExecute);
        //private bool TestCommandCanExecute()
        //{
        //    return true;
        //}
        //private void TestCommandExecute()
        //{
        //    _NavigationService.NavigateToHiddenInterface();
        //}

        public ActiveInterfaceViewModel(INavigationService navigationService)
        {
            _NavigationService = navigationService;
        }

        #region     Fields/Properties
        //public bool IsActiveInterfaceVisible { get; set; }
        #endregion  Fields/Properties

        #region     Commands
        //public ICommand 
        #endregion  Commands
    }
}
