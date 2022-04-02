using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.DesktopClient.Core;
public abstract class BindableBase : INotifyPropertyChanged
{
    #region     INotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string? propertyName)
    {
        if (PropertyChanged is null)
        {
            return;
        }

        PropertyChangedEventHandler handler = PropertyChanged;
        handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
    {
        if (Equals(storage, value)) return false;
        storage = value;

        OnPropertyChanged(propertyName);
        return true;
    }

    protected virtual bool SetProperty<T>(Action<T> storage, T value, [CallerMemberName] string propertyName = "")
    {
        storage.Invoke(value);
        OnPropertyChanged(propertyName);
        return true;
    }
    #endregion  INotifyPropertyChanged
}
