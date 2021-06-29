using System.Reflection;
using MASA.Blazor.Doc.Highlight;
using MASA.Blazor.Doc.Localization;
using MASA.Blazor.Doc.Routing;
using MASA.Blazor.Doc.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMasaBlazorDocs(this IServiceCollection services)
        {
            services.AddMasaBlazor();

            services.AddSingleton<RouteManager>();
            services.AddScoped<DemoService>();
            services.AddSingleton<ILanguageService>(new InAssemblyLanguageService(Assembly.GetExecutingAssembly()));
            services.AddScoped<IPrismHighlighter, PrismHighlighter>();

            return services;
        }
    }
}
