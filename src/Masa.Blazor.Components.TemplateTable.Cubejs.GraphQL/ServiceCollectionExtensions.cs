using Masa.Blazor.Components.TemplateTable.Core;
using Masa.Blazor.Components.TemplateTable.Cubejs.GraphQL;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Using Cube.js as the data source for Masa.Blazor.Components.TemplateTable.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IMasaBlazorBuilder WithCubejs(this IMasaBlazorBuilder builder)
    {
        builder.Services.AddScoped<IGraphQLClient, CubejsGraphQLClient>();
        return builder;
    }
}