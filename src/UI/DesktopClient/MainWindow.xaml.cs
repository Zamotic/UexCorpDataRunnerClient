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
using UexCorpDataRunner.Application.MessengerMessages;
using CommunityToolkit.Mvvm.Messaging;

namespace UexCorpDataRunner.DesktopClient;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    IMessenger _Messenger;

    public MainWindow(IMessenger messenger)
    {
        _Messenger = messenger;
        InitializeComponent();
    }

    private void Window_LocationChanged(object sender, EventArgs e)
    {
        _Messenger.Send(new WindowMovedMessage());
    }
}
