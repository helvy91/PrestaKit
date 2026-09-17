using Microsoft.Extensions.Options;
using PrestaKit;
using System.Net.Http.Headers;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection; 

public static class PrestaShopServiceCollectionExtensions
{
    public static IServiceCollection AddPrestaShopClient(
        this IServiceCollection services,
        Action<PrestaShopClientOptions> configure)
    {
        services.AddOptions<PrestaShopClientOptions>()
        .Configure(configure)
        .ValidateDataAnnotations()   
        .ValidateOnStart();

        services.AddHttpClient<IPrestaShopClient, PrestaShopClient>((sp, http) =>
        {
            var opt = sp.GetRequiredService<IOptions<PrestaShopClientOptions>>().Value;
            http.BaseAddress = opt.BaseUrl;
            http.Timeout = opt.Timeout;
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{opt.ApiKey}:")));
        });

        return services;
    }
}