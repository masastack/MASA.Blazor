using Masa.Blazor.Components.TemplateTable.Contracts;

namespace TemplateTableSample.UI.Services;

public class SheetService(HttpClient httpClient, ILogger<SheetService> logger)
{
    public Task<Sheet?> GetSheetAsync()
    {
        return httpClient.GetFromJsonAsync<Sheet>("/sheet");
    }

    public Task<IList<View>?> GetUserViewsAsync(string user)
    {
        return httpClient.GetFromJsonAsync<IList<View>>($"/views/{user}");
    }

    public Task AddUserViewAsync(string user, View view)
    {
        return httpClient.PostAsJsonAsync($"/views/{user}", view);
    }

    public Task UserViewDeleteAsync(string user, View view)
    {
        return httpClient.DeleteAsync($"/views/{user}/{view.Id}");
    }

    public Task SaveAsync(Sheet sheet)
    {
        return httpClient.PostAsJsonAsync("/sheet", sheet);
    }
}