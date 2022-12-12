namespace Masa.Docs.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMasaDocs(this IServiceCollection services, string baseUri, string mode = BlazorMode.Server)
    {
        BlazorMode.Current = mode;

        string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.129 Safari/537.36 Edg/81.0.416.68";
        services.AddHttpClient("masa-docs", httpClient =>
        {
            httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
            httpClient.BaseAddress = new Uri(baseUri);
        });
        services.AddHttpClient("github", httpClient =>
        {
            httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
            httpClient.BaseAddress = new Uri("https://api.github.com/");
        });

        services.AddScoped<DocService>();
        services.AddScoped<AppService>();

        services.AddMemoryCache();

        ApiGenerator.ApiGenerator.Run();

        return services;
    }
}
