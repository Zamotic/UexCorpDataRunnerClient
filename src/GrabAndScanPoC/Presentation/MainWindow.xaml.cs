using CommunityToolkit.Mvvm.Messaging;
using GrabAndScanPoC.Common;
using GrabAndScanPoC.Core.Messengers;
using System.Windows;

namespace GrabAndScanPoC.Presentation;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    IMessenger _messenger;
    IMessageBoxService _messageBoxService;

    public MainWindow(IMessenger messenger, IMessageBoxService messageboxService)
    {
        InitializeComponent();
        _messenger = messenger;
        _messageBoxService = messageboxService;

        _messenger.Register<MessageBoxMessage>(this, new MessageHandler<object, MessageBoxMessage>(MessageBoxMessageReceived));
    }

    private void MessageBoxMessageReceived(object recipient, MessageBoxMessage message)
    {
        _messageBoxService.LaunchMessageBox(message.MessageBox.Message, message.MessageBox.Caption, message.MessageBox.Image, message.MessageBox.Button);
    }
}