using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using System;
using UexCorpDataRunner.Interface.MessengerMessages;

namespace AvaloniaClient;
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