using GraphQL;
using Masa.Blazor.Components.TemplateTable;

namespace Masa.Blazor.Playground.Pages;

public partial class TemplateTable
{
    private SheetProvider? _sheetProvider;
    private ItemsProvider? _itemsProvider;
    private bool _sheetRequested;

    protected override async Task OnInitializedAsync()
    {
        _sheetProvider = async req =>
        {
            var request = new GraphQLRequest(req.Query);
            var response = await GraphQLClient.SendQueryAsync<SheetProviderResult>(request);
            return response.Data;
        };

        _itemsProvider = async req =>
        {
            var formatQuery = req.FormatQuery(
                """
                users {
                  name
                  age
                  avatar
                  done
                  birthday
                  favoriteBooks
                }
                """);

            var request = new GraphQLRequest
            {
                Query = formatQuery
            };

            var response = await GraphQLClient.SendQueryAsync<ItemsProviderResult>(request);
            return response.Data;
        };
    }
}