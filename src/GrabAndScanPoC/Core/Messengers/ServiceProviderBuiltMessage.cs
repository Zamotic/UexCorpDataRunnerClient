using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabAndScanPoC.Core.Messengers;
public class ServiceProviderBuiltMessage
{
    public IServiceProvider ServiceProvider { get; }
    public ServiceProviderBuiltMessage(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }
}
