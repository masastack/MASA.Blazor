namespace Masa.Blazor.Services.Internals;

public class InternalHttpClient : IDisposable
{
    private readonly HttpClient _httpClient = new();

    public async Task<Stream> GetStreamAsync(string requestUri)
    {
        return await _httpClient.GetStreamAsync(requestUri);
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
