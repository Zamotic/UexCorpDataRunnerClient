using GrabAndScanPoC.Imaging.TextRetrieval;
using Microsoft.Extensions.DependencyInjection;

namespace GrabAndScanPoC.Imaging;
public static class StartupExtensions
{
    public static IServiceCollection AddImaging(this IServiceCollection services)
    {
        services.AddScoped<ITextExtractor, CommodityDataTextExtractor>();

        return services;
    }
}