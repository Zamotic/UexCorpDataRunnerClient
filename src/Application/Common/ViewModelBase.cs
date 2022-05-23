using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Application.Common;
public abstract class ViewModelBase : BindableBase
{
    protected bool _IsEnabled = false;
    public bool IsEnabled
    {
        get => _IsEnabled;
        set => SetProperty(ref _IsEnabled, value);
    }
}
