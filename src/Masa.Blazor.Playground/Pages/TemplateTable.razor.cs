using GraphQL;
using GraphQL.Client.Abstractions;
using Masa.Blazor.Components.TemplateTable;
using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.Playground.Pages;

public partial class TemplateTable
{
    [Inject] private HttpClient HttpClient { get; set; } = null!;

    [Inject] private IPopupService PopupService { get; set; } = default!;

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
                  level
                }
                """);

            var request = new GraphQLRequest
            {
                Query = formatQuery
            };

            var response = await GraphQLClient.SendQueryAsync<ItemsProviderResult>(request);

            if (response.Errors?.Length > 0)
            {
                await PopupService.EnqueueSnackbarAsync("Error: " + response.Errors[0].Message, AlertTypes.Error);
            }

            return response.Data;
        };
    }

    private async Task Save(Sheet sheet)
    {
        await HttpClient.PostAsJsonAsync("http://localhost:5297/sheet", sheet);
    }
}