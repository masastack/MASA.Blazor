﻿using Masa.Blazor.Doc.Highlight;
using Masa.Blazor.Doc.Services;
using Masa.Blazor.Doc.Utils;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMasaBlazorDocs(this IServiceCollection services, string baseUri, string mode = BlazorMode.Server)
        {
            BlazorMode.Current = mode;

            services.AddHttpClient<DemoService>(c =>
            {
                c.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.129 Safari/537.36 Edg/81.0.416.68");
                c.BaseAddress = new Uri(baseUri);
            });

            services.AddHttpClient<CrawlService>(c =>
            {
                c.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.129 Safari/537.36 Edg/81.0.416.68");
                c.BaseAddress = new Uri(baseUri);
            });

            services.AddScoped<IPrismHighlighter, PrismHighlighter>();
            services.AddHttpClient("doc", httpClient =>
            {
                httpClient.BaseAddress = new Uri(baseUri);
            });

            return services;
        }
    }
}
