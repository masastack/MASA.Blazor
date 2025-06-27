using System.Net.Http.Headers;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

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
        builder.Services.AddScoped<GraphQLHttpClient>(_ =>
        {
            var client = new GraphQLHttpClient(endPoint,
                new SystemTextJsonSerializer(new JsonSerializerOptions(JsonSerializerDefaults.Web)));

            if (bearerToken is not null)
            {
                client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", bearerToken);
            }

            return client;
        });

        return builder;
    }
}