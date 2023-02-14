using Microsoft.Extensions.DependencyInjection;

namespace Masa.Blazor.Docs;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMasaBlazorDocs(this IServiceCollection services)
    {
        services.AddScoped<BlazorDocService>();

        return services;
    }
}
