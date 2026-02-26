using Masa.Blazor.Components.TemplateTable;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds a GraphQL client for the Template Table component.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="endPoint">The GraphQL endpoint URL.</param>
    /// <param name="bearerToken">The Bearer token for authentication.</param>
    /// <returns></returns>
    public static IMasaBlazorBuilder AddGraphQLClientForTemplateTable(this IMasaBlazorBuilder builder, string endPoint,
        string? bearerToken = null)
    {
        if (!builder.Services.Any(t => t.ServiceType == typeof(ITemplateTableComponent)))
        {
            builder.Services.AddSingleton<ITemplateTableComponent, DefaultTemplateTableComponent>();
        }
        builder.Services.AddScoped<GraphQL.Client.Abstractions.IGraphQLClient>(_ =>
        {
            var client = new GraphQLHttpClient(endPoint,
                new SystemTextJsonSerializer(new JsonSerializerOptions(JsonSerializerDefaults.Web)));

            if (bearerToken is not null)
            {
                client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", bearerToken);
            }

            return (GraphQL.Client.Abstractions.IGraphQLClient)client;
        });

        return builder;
    }

    public static IMasaBlazorBuilder AddGraphQLClientForTemplateTable(this IMasaBlazorBuilder builder, Func<IServiceProvider, GraphQL.Client.Abstractions.IGraphQLClient> func)
    {
        ArgumentNullException.ThrowIfNull(func);
        if (!builder.Services.Any(t => t.ServiceType == typeof(ITemplateTableComponent)))
        {
            builder.Services.AddSingleton<ITemplateTableComponent, DefaultTemplateTableComponent>();
        }
        builder.Services.AddScoped(func);
        return builder;
    }

    public static Task<IMasaBlazorBuilder> AddTemplateTableI18nAsync(this IMasaBlazorBuilder builder, string hostPath)
    {
        ArgumentNullException.ThrowIfNull(hostPath);
        var i18nPath = $"{hostPath}/_content/Masa.Blazor.Components.TemplateTable/i18n";
        return builder.AddI18nForWasmAsync(i18nPath);
    }
}