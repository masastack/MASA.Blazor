using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Masa.Blazor.Docs;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMasaBlazorDocs(this IServiceCollection services)
    {
        services.AddScoped<BlazorDocService>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);

        return services;
    }
}
