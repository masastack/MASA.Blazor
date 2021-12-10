using System.Reflection;
using BlazorComponent.Components;
using MASA.Blazor.Doc;
using MASA.Blazor.Doc.Highlight;
using MASA.Blazor.Doc.Routing;
using MASA.Blazor.Doc.Services;
using MASA.Blazor.Doc.Utils;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMasaBlazorDocs(this IServiceCollection services)
        {
            services.AddMasaBlazor();

            services.AddScoped<I18n>();
            services.AddScoped<GlobalConfigs>();
            services.AddScoped<CookieStorage>();

            services.AddScoped<DemoService>();
            services.AddScoped<IPrismHighlighter, PrismHighlighter>();

            return services;
        }
    }
}
