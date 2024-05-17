using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Interface.MessengerMessages;
using CommunityToolkit.Mvvm.Messaging;
using UexCorpDataRunner.Extensions;

namespace UexCorpDataRunner.Presentation.Settings;

/// <summary>
/// Interaction logic for SettingsView.xaml
/// </summary>
public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
    }

    //public void ShowSettingsInterfaceNotified(ShowSettingsInterfaceMessage notification)
    //public void ShowSettingsInterfaceMessageHandler(object sender, ShowSettingsInterfaceMessage notification)
    //{
    //    Window window = System.Windows.Application.Current.MainWindow;
    //    window.WindowStyle = WindowStyle.SingleBorderWindow;
    //    window.Width = 425;
    //    window.Height = 700;
    //    window.ResizeMode = ResizeMode.CanResize;
    //    window.Topmost = true;
    //}

    private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        IMessenger? messenger = StartupExtensions.ServiceProvider?.GetService<IMessenger>();
        //messenger?.Register<ShowSettingsInterfaceMessage>(this, ShowSettingsInterfaceMessageHandler);
    }

    List<Key> lastPressedCharacters = new List<Key>();
    private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        switch(e.Key)
        {
            case Key.Up:
            case Key.Down:
            case Key.Left:
            case Key.Right:
            case Key.B:
            case Key.A:
                //lastPressedCharacters.Add(e.Key);
                //CheckIfLastPressedCharactersAreKonamiCode();
                break;
            default:
                lastPressedCharacters.Clear();
                break;
        }
    }

    //List<Key> KonamiCodeList = new List<Key> { Key.Up, Key.Up, Key.Down, Key.Down, Key.Left, Key.Right, Key.Left, Key.Right, Key.B, Key.A };
    //private bool CheckIfLastPressedCharactersAreKonamiCode()
    //{
    //    Interface.Settings.SettingsViewModel? settingsViewModel = this.DataContext as Interface.Settings.SettingsViewModel;

    //    if(settingsViewModel is null)
    //    {
    //        return false;
    //    }
        
    //    if (lastPressedCharacters.Skip(lastPressedCharacters.Count - 10).Take(10).IsAMatch(KonamiCodeList) == false)
    //    {
    //        if(lastPressedCharacters.Count > 10)
    //        {
    //            lastPressedCharacters.RemoveAt(0);
    //        }
    //        return false;
    //    }

    //    settingsViewModel.WasKonamiCodeActivated = true;
    //    return true;
    //}
}
