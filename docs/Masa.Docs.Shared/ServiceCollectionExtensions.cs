﻿using Masa.Blazor.Docs;

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
            });
        });

        services.AddMasaBlazorDocs();

        return masaBlazorBuilder;
    }
}
