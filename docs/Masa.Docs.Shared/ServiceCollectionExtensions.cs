using Masa.Blazor.Docs;

namespace Masa.Docs.Shared;

public static class ServiceCollectionExtensions
{
    public static IMasaBlazorBuilder AddMasaDocs(this IServiceCollection services, string baseUri, string mode = BlazorMode.Server)
    {
        BlazorMode.Current = mode;

        var userAgent =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.129 Safari/537.36 Edg/81.0.416.68";
        services.AddHttpClient("masa-docs", httpClient =>
        {
            httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
            httpClient.BaseAddress = new Uri(baseUri);
        });

        services.AddScoped<DocService>();
        services.AddScoped<AppService>();
        services.AddSingleton<GithubService>();

        services.AddMemoryCache();

        var masaBlazorBuilder = services.AddMasaBlazor(options =>
        {
            options.ConfigureTheme(theme =>
            {
                theme.Dark = false;

                theme.Themes.Light.Primary = "#4f33ff";
                theme.Themes.Light.Secondary = "#C7C4DC";
                theme.Themes.Light.Error = "#ba1a1a";
                theme.Themes.Light.UserDefined["Tertiary"] = "#00966f";
                theme.Themes.Light.UserDefined["Neutral"] = "#929094";
                theme.Themes.Light.UserDefined["NeutralVariant"] = "#928f99";

                theme.Themes.Dark.Primary = "#c5c0ff";
                theme.Themes.Dark.Secondary = "#C7C4DC";
                theme.Themes.Dark.Error = "#ffb4ab";
                theme.Themes.Dark.UserDefined["Tertiary"] = "#68dbaf";
                theme.Themes.Dark.UserDefined["Neutral"] = "#929094";
                theme.Themes.Dark.UserDefined["NeutralVariant"] = "#928f99";
            });
            options.ConfigureIcons(IconSet.MaterialDesignIcons, aliases =>
            {
                aliases.UserDefined["masaBlazor"] = new SvgPath(
                    "M16 2H6V6H2V10H6V14H2V18H6V22H16C19.3137 22 22 19.3137 22 16C22 14.4633 21.4223 13.0615 20.4722 12C21.4223 10.9385 22 9.53671 22 8C22 4.68629 19.3137 2 16 2ZM6 18H16C17.1046 18 18 17.1046 18 16C18 14.8954 17.1046 14 16 14H6V18ZM6 10H16C17.1046 10 18 9.10457 18 8C18 6.89543 17.1046 6 16 6H6V10Z",
                    new Dictionary<string, object?>()
                    {
                        { "fill-rule", "evenodd" },
                        { "clip-rule", "evenodd" }
                    });
                aliases.UserDefined["masaFramework"] = new SvgPath[]
                {
                    new("M12 2H22V12H12V2Z", new Dictionary<string, object?>() {  { "fill-opacity", "0.5" } }),
                    new("M11.9999 2H2V21.9998H11.9999V2Z"),
                    new("M16.9999 21.9999C19.7613 21.9999 21.9999 19.7613 21.9999 16.9999C21.9999 14.2386 19.7613 12 16.9999 12C14.2386 12 12 14.2386 12 16.9999C12 19.7613 14.2386 21.9999 16.9999 21.9999Z"),
                };
                aliases.UserDefined["masaStack"] = new SvgPath[]
                {
                    new("M7 2C4.23858 2 2 4.23857 2 7C2 9.76142 4.23858 12 7 12H12V7C12 4.23857 9.76142 2 7 2Z"),
                    new("M2 7C2 7 2 7 2 7V17C2 19.7614 4.23858 22 7 22C9.76142 22 12 19.7614 12 17L12 12H7C4.23858 12 2 9.76142 2 7Z",
                        new Dictionary<string, object?>() {  { "fill-opacity", "0.5" } }),
                    new("M22 7C22 4.23857 19.7614 2 17 2C14.2386 2 12 4.23857 12 7V12H17C19.7614 12 22 9.76142 22 7Z",
                        new Dictionary<string, object?>() {  { "fill-opacity", "0.5" } }),
                    new("M12 7V17C12 19.7614 14.2386 22 17 22C19.7614 22 22 19.7614 22 17V7.00926C21.995 9.76642 19.7583 12 17 12H12L12 7Z"),
                };
                aliases.UserDefined["wechat"] = new SvgPath(
                    "M9.5,4C5.36,4 2,6.69 2,10C2,11.89 3.08,13.56 4.78,14.66L4,17L6.5,15.5C7.39,15.81 8.37,16 9.41,16C9.15,15.37 9,14.7 9,14C9,10.69 12.13,8 16,8C16.19,8 16.38,8 16.56,8.03C15.54,5.69 12.78,4 9.5,4M6.5,6.5A1,1 0 0,1 7.5,7.5A1,1 0 0,1 6.5,8.5A1,1 0 0,1 5.5,7.5A1,1 0 0,1 6.5,6.5M11.5,6.5A1,1 0 0,1 12.5,7.5A1,1 0 0,1 11.5,8.5A1,1 0 0,1 10.5,7.5A1,1 0 0,1 11.5,6.5M16,9C12.69,9 10,11.24 10,14C10,16.76 12.69,19 16,19C16.67,19 17.31,18.92 17.91,18.75L20,20L19.38,18.13C20.95,17.22 22,15.71 22,14C22,11.24 19.31,9 16,9M14,11.5A1,1 0 0,1 15,12.5A1,1 0 0,1 14,13.5A1,1 0 0,1 13,12.5A1,1 0 0,1 14,11.5M18,11.5A1,1 0 0,1 19,12.5A1,1 0 0,1 18,13.5A1,1 0 0,1 17,12.5A1,1 0 0,1 18,11.5Z");
            });
        });

        services.AddMasaBlazorDocs();

        return masaBlazorBuilder;
    }
}
