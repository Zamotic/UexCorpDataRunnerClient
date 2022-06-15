using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System;
using UexCorpDataRunner.Presentation.DataRunner;
using UexCorpDataRunner.Presentation.Minimized;
using UexCorpDataRunner.Presentation.Settings;
using UexCorpDataRunner.Interface.MessengerMessages;

namespace UexCorpDataRunner.Presentation;
public static class StartupExtensions
{
    private static MessengerSubscriber? _MessengerSubscriber;
    public static IServiceProvider? ServiceProvider { get; private set; }
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        SubscribeToServiceProviderBuilt(services);

        services.AddSingleton<TransmissionStatusView>();
        services.AddSingleton<DataRunnerView>();
        services.AddSingleton<MinimizedView>();
        services.AddSingleton<SettingsView>();

        return services;
    }

    public static void SubscribeToServiceProviderBuilt(IServiceCollection services)
    {
        if(services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        var sp = services.BuildServiceProvider();
        var messenger = sp.GetService<IMessenger>();

        if (messenger is null)
        {
            throw new Exception("No service of type IMessenger could be found.");
        }
        _MessengerSubscriber = new MessengerSubscriber(messenger);
    }

    private class MessengerSubscriber
    {
        public MessengerSubscriber(IMessenger messenger)
        {
            messenger?.Register<ServiceProviderBuiltMessage>(this, ServiceProviderBuiltMessageHandler);
        }

        private void ServiceProviderBuiltMessageHandler(object sender, ServiceProviderBuiltMessage serviceProviderBuiltMessage)
        {
            ServiceProvider = serviceProviderBuiltMessage.ServiceProvider;
        }
    }
}
