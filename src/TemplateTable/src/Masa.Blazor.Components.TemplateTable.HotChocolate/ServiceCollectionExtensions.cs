using Masa.Blazor.Components.TemplateTable.Abstractions;
using Masa.Blazor.Components.TemplateTable.HotChocolate;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Using HotChocolate as the data source for Masa.Blazor.Components.TemplateTable.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IMasaBlazorBuilder WithHotChocolate(this IMasaBlazorBuilder builder)
    {
        builder.Services.AddScoped<IGraphQLClient, HotChocolateGraphQLClient>();
        return builder;
    }
}