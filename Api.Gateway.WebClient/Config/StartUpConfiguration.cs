using Api.Gateways.Proxies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Gateway.WebClient.Config
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddAppsettingBinding(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<ApiUrls>(opts => configuration.GetSection("ApiUrls").Bind(opts));
            return service;
        }
    }
}
