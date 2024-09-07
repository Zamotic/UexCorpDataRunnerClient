using CommunityToolkit.Mvvm.ComponentModel;

namespace UexCorpDataRunner.Application.Common;
public abstract class ViewModelBase : ObservableObject
{
    protected bool _IsEnabled = false;
    public bool IsEnabled
    {
        get => _IsEnabled;
        set => SetProperty(ref _IsEnabled, value);
    }
}
