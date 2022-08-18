using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Interface.MessengerMessages;

namespace UexCorpDataRunner.Interface.DataRunner;
public class ReleaseNotesViewModel : ViewModelBase
{
    IMessenger _messenger;

    private string _ReleaseNotes = 
    "Version 1.0.817\n" +
        "   - Added Release Notes Popup\n" +
        "   - Added Theme Support (Light/Dark)\n" +
        "   - Changed HttpClient handling to a more standard usage\n" +
        "       - (Note: This could also improve API communication performance)\n";
    public string ReleaseNotes
    {
        get => _ReleaseNotes;
        set => SetProperty(ref _ReleaseNotes, value);
    }

    public ReleaseNotesViewModel(IMessenger messenger)
    {
        _messenger = messenger;
        _messenger.Register<ShowReleaseNotesMessage>(this, ShowReleaseNotesMessageHandler);
    }

    public ICommand CloseReleaseNotesViewCommand { get => new RelayCommand(CloseReleaseNotesViewCommandExecute); }
    private void CloseReleaseNotesViewCommandExecute()
    {
        IsEnabled = false;
    }

    public void ShowReleaseNotesMessageHandler(object sender, ShowReleaseNotesMessage notification)
    {
        IsEnabled = true;
    }
}
