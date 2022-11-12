using Masa.Docs.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Masa.Docs.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMasaDocs(this IServiceCollection services, string baseUri)
    {
        services.AddHttpClient("masa-docs", c =>
        {
            c.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.129 Safari/537.36 Edg/81.0.416.68");
            c.BaseAddress = new Uri(baseUri);
        });

        services.AddScoped<DocService>();
        services.AddScoped<AppService>();

        ApiGenerator.ApiGenerator.Run();

        return services;
    }
}
