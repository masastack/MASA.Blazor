using System.Reflection;
using MASA.Blazor.Doc.Highlight;
using MASA.Blazor.Doc.Routing;
using MASA.Blazor.Doc.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMasaBlazorDocs(this IServiceCollection services)
        {
            services.AddMasaBlazor();

            services.AddScoped<DemoService>();
            services.AddScoped<IPrismHighlighter, PrismHighlighter>();

            return services;
        }
    }
}
