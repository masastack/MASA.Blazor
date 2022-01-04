using System.Reflection;
using BlazorComponent.Components;
using MASA.Blazor.Doc;
using MASA.Blazor.Doc.Highlight;
using MASA.Blazor.Doc.Services;
using Microsoft.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMasaBlazorDocs(this IServiceCollection services,string baseUri)
        {
            services.AddMasaBlazor();
            services.AddHttpClient<DemoService>(c => 
            {
                c.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.129 Safari/537.36 Edg/81.0.416.68");
                c.BaseAddress=new Uri(baseUri);
            });
            services.AddScoped<IPrismHighlighter, PrismHighlighter>();

            return services;
        }
    }
}
