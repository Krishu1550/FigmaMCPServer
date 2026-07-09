using FigmaMcp.Application.UseCases.BuildUiModel;
using FigmaMcp.Application.UseCases.GetFigmaDesign;
using FigmaMcp.Application.UseCases.ParseFigmaUrl;
using Microsoft.Extensions.DependencyInjection;

namespace FigmaMcp.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Use Cases
            services.AddTransient<ParseFigmaUrlHandler>();
            services.AddTransient<GetFigmaDesignHandler>();
            services.AddTransient<BuildUiModelHandler>();

            return services;
        }
    }
}
