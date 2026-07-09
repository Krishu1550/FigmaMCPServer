using FigmaMcp.Application.Interfaces;
using FigmaMcp.Infrastructure.Caching;
using FigmaMcp.Infrastructure.Figma;
using FigmaMcp.Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FigmaMcp.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration config)
        {
            // HTTP CLIENTS
            services.AddHttpClient<FigmaHttpClient>(client =>
            {
                // BaseAddress MUST end with a trailing slash and relative request
                // URIs MUST NOT start with '/', otherwise Uri combination drops
                // the "/v1" path segment (e.g. requests would hit
                // https://api.figma.com/files/... instead of .../v1/files/...).
                var baseUrl = config["Figma:BaseUrl"]!;
                client.BaseAddress =
                    new Uri(baseUrl.EndsWith('/') ? baseUrl : baseUrl + "/");
            });

            // SERVICES
            services.AddSingleton<FigmaApiClient>();
            services.AddSingleton<IFigmaUrlParser, FigmaUrlParser>();

            // INTERFACE IMPLEMENTATIONS
            services.AddSingleton<IFigmaService, FigmaService>();
            services.AddSingleton<IImageExporter, FigmaImageExporter>();
            services.AddSingleton<IDesignParser, FigmaDesignParser>();
            services.AddSingleton<IUiModelBuilder, UiModelBuilder>();

            // CACHE
            services.AddMemoryCache();
            services.AddSingleton<MemoryCacheService>();

            // LOGGING WRAPPER
            services.AddSingleton<AppLogger>();

            return services;
        }
    }
}