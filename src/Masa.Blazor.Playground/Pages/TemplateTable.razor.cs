using GraphQL;
using Masa.Blazor.Components.TemplateTable;
using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.Playground.Pages;

public partial class TemplateTable
{
    [Inject] private HttpClient HttpClient { get; set; } = null!;

    [Inject] private IPopupService PopupService { get; set; } = default!;

    private SheetProvider? _sheetProvider;
    private UserViewsProvider? _userViewsProvider;
    private ItemsProvider? _itemsProvider;
    private bool _sheetRequested;

    protected override async Task OnInitializedAsync()
    {
        _sheetProvider = async req =>
        {
            var request = new GraphQLRequest(req.Query);
            return await GraphQLClient.SendQueryAsync<SheetProviderResult>(request);
        };

        _userViewsProvider = async () => await HttpClient.GetFromJsonAsync<List<View>>("http://localhost:5297/views/cyx");

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
            
            //TODO: 和sheetprovider一样把异常放到组件内处理
            var response = await GraphQLClient.SendQueryAsync<ItemsProviderResult>(request);
            await Task.Delay(500); // TODO: just for testing, remove it if implemented

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