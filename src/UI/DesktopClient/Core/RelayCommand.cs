using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Input;

/*******************************
 * Note: This was borrowed from Josh Smith (http://msdn.microsoft.com/en-us/magazine/dd419663.aspx#id0090030)
 *******************************/

namespace UexCorpDataRunner.DesktopClient.Core;

public class RelayCommand<T> : ICommand
{
    #region Fields
    readonly Action<T?> _execute;
    readonly Func<T?, bool> _canExecute;
    #endregion // Fields

    #region Constructors
    public RelayCommand(Action<T?> execute)
        : this(execute, null)
    {
    }

    public RelayCommand(Action<T?>? execute, Func<T?, bool>? canExecute)
    {
        if (execute == null)
            throw new ArgumentNullException(nameof(execute));

        _execute = execute;

        if (canExecute is null) 
        {
            _canExecute = (T) => {
                return true;
            };
        }
        _canExecute = (T) => {
            return true;
        };
    }
    #endregion // Constructors

    #region ICommand Members
    [DebuggerStepThrough]
    public bool CanExecute(object? parameter)
    {
        if (_canExecute is null) 
        {
            return true;
        }

        if (parameter is null) { return true; }

        return _canExecute((T)parameter);
    }

    public event EventHandler? CanExecuteChanged 
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public void Execute(object? parameter)
    {
        _execute((T?)parameter);
    }

    public void RaiseCanExecuteChanged()
    {
        CommandManager.InvalidateRequerySuggested();
    }
    #endregion // ICommand Members
}

public class RelayCommand : ICommand
{
    #region Fields
    readonly Action _execute;
    readonly Func<bool> _canExecute;
    #endregion // Fields

    #region Constructors
    public RelayCommand(Action execute)
        : this(execute, null)
    {
    }

    public RelayCommand(Action execute, Func<bool>? canExecute)
    {
        if (execute == null)
            throw new ArgumentNullException(nameof(execute));

        _execute = execute;

        if (canExecute is null)
        {
            _canExecute = () => {
                return true;
            };
        }
        _canExecute = () => {
            return true;
        };
    }
    #endregion // Constructors

    #region ICommand Members
    [DebuggerStepThrough]
    public bool CanExecute(object? parameter)
    {
        if (_canExecute is null)
        {
            return true;
        }

        if (parameter is null) { return true; }

        return _canExecute();
    }

    public event EventHandler? CanExecuteChanged {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public void Execute(object? parameter)
    {
        _execute();
    }

    public void RaiseCanExecuteChanged()
    {
        CommandManager.InvalidateRequerySuggested();
    }
    #endregion // ICommand Members
}
