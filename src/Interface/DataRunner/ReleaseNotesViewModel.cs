using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Reflection;
using System.Windows.Input;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Interface.MessengerMessages;

namespace UexCorpDataRunner.Interface.DataRunner;
public class ReleaseNotesViewModel : ViewModelBase
{
    IMessenger _messenger;
    private const string ResourceName = "UexCorpDataRunner.Interface.ReleaseNotes.txt";

    public string ReleaseNotes
    {
        get => GetReleaseNotesFromAssembly();
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

    private string GetReleaseNotesFromAssembly()
    {
        var assembly = Assembly.GetExecutingAssembly();

        if (assembly is null)
        {
            return string.Empty;
        }

        using (Stream? stream = assembly.GetManifestResourceStream(ResourceName))
        {
            if(stream is null)
            {
                return string.Empty;
            }
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result;
            }
        }
    }
}
