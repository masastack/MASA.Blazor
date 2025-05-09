using Masa.Blazor.Docs;

namespace Masa.Docs.Shared;

public static class ServiceCollectionExtensions
{
    public static IMasaBlazorBuilder AddMasaDocs(this IServiceCollection services, string baseUri,
        string mode = BlazorMode.Server)
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
        services.AddScoped<ExpiryLocalStorage>();
        services.AddScoped<GithubService>();

        var masaBlazorBuilder = services.AddMasaBlazor(options =>
        {
            options.ConfigureTheme(theme =>
            {
                theme.Themes.Light.UserDefined["basil"] = new ColorPairing("#FFFBE6", "#356859");
                theme.Themes.Dark.UserDefined["basil"] = new ColorPairing("#FFFBE6", "#356859");

                theme.Themes.Add("masa-light", false, custom =>
                {
                    custom.Primary = "#4318ff";
                    custom.Secondary = "#5e5c71";
                    custom.Accent = "#006c4f";
                    custom.Error = "#ba1a1a";
                    custom.Surface = "#fcf8fd";
                    custom.OnSurface = "#1c1b1f";
                    custom.SurfaceDim = "#ddd9dd";
                    custom.SurfaceBright = "#fcf8fd";
                    custom.SurfaceContainer = "#f1ecf1";
                    custom.SurfaceContainerLow = "#f6f2f7";
                    custom.SurfaceContainerLowest = "#ffffff";
                    custom.SurfaceContainerHigh = "#ebe7ec";
                    custom.SurfaceContainerHighest = "#e5e1e6";
                    custom.InversePrimary = "#c5c0ff";
                    custom.InverseSurface = "#313034";
                    custom.InverseOnSurface = "#f4eff4";
                });
                
                theme.Themes.Add("masa-dark", true, custom =>
                {
                    custom.Primary = "#c5c0ff";
                    custom.OnPrimary = "#2400a2";
                    custom.Secondary = "#c7c4dc";
                    custom.OnSecondary = "#302e42";
                    custom.Accent = "#67dbaf";
                    custom.OnAccent = "#003827";
                    custom.Error = "#ffb4ab";
                    custom.OnError = "#690005";
                    custom.Surface = "#131316";
                    custom.OnSurface = "#c9c5ca";
                    custom.SurfaceDim = "#131316";
                    custom.SurfaceBright = "#3a393c";
                    custom.SurfaceContainer = "#201f23";
                    custom.SurfaceContainerLow = "#1c1b1f";
                    custom.SurfaceContainerLowest = "#0e0e11";
                    custom.SurfaceContainerHigh = "#2a292d";
                    custom.SurfaceContainerHighest = "#353438";
                    custom.InversePrimary = "#4f33ff";
                    custom.InverseSurface = "#e5e1e6";
                    custom.InverseOnSurface = "#1c1b1f";
                });
                
                theme.Themes.Add("greenery", false, custom =>
                {
                    custom.Primary = "#386a1f";
                    custom.Secondary = "#55624c";
                    custom.Accent = "#386666";
                    custom.Error = "#ba1a1a";
                    custom.Surface = "#f8faf0";
                    custom.OnSurface = "#1a1c18";
                    custom.SurfaceDim = "#d9dbd1";
                    custom.SurfaceBright = "#f8faf0";
                    custom.SurfaceContainer = "#edefe5";
                    custom.SurfaceContainerLow = "#f3f5eb";
                    custom.SurfaceContainerLowest = "#ffffff";
                    custom.SurfaceContainerHigh = "#e7e9df";
                    custom.SurfaceContainerHighest = "#e1e3da";
                    custom.InversePrimary = "#9dd67d";
                    custom.InverseSurface = "#2f312d";
                    custom.InverseOnSurface = "#f1f1ea";
                });
                
                theme.Themes.Add("camel", true, custom =>
                {
                    custom.Primary = "#ffb68a";
                    custom.OnPrimary = "#522300";
                    custom.Secondary = "#e5bfa9";
                    custom.OnSecondary = "#432b1c";
                    custom.Accent = "#cbc992";
                    custom.OnAccent = "#333209";
                    custom.Error = "#ffb4ab";
                    custom.OnError = "#690005";
                    custom.Surface = "#1a120d";
                    custom.OnSurface = "#f0dfd7";
                    custom.SurfaceDim = "#1a120d";
                    custom.SurfaceBright = "#413732";
                    custom.SurfaceContainer = "#261e19";
                    custom.SurfaceContainerLow = "#221a15";
                    custom.SurfaceContainerLowest = "#140d08";
                    custom.SurfaceContainerHigh = "#312823";
                    custom.SurfaceContainerHighest = "#3d332d";
                    custom.InversePrimary = "#8c4f26";
                    custom.InverseSurface = "#f0dfd7";
                    custom.InverseOnSurface = "#382e29";
                });
            });
            options.ConfigureIcons(IconSet.MaterialDesignIcons, aliases =>
            {
                aliases.UserDefined["masaBlazor"] = new SvgPath(
                    "M16 2H6V6H2V10H6V14H2V18H6V22H16C19.3137 22 22 19.3137 22 16C22 14.4633 21.4223 13.0615 20.4722 12C21.4223 10.9385 22 9.53671 22 8C22 4.68629 19.3137 2 16 2ZM6 18H16C17.1046 18 18 17.1046 18 16C18 14.8954 17.1046 14 16 14H6V18ZM6 10H16C17.1046 10 18 9.10457 18 8C18 6.89543 17.1046 6 16 6H6V10Z",
                    new Dictionary<string, object>()
                    {
                        { "fill-rule", "evenodd" },
                        { "clip-rule", "evenodd" }
                    });
                aliases.UserDefined["masaFramework"] = new SvgPath[]
                {
                    new("M12 2H22V12H12V2Z", new Dictionary<string, object?>() { { "fill-opacity", "0.5" } }),
                    new("M11.9999 2H2V21.9998H11.9999V2Z"),
                    new(
                        "M16.9999 21.9999C19.7613 21.9999 21.9999 19.7613 21.9999 16.9999C21.9999 14.2386 19.7613 12 16.9999 12C14.2386 12 12 14.2386 12 16.9999C12 19.7613 14.2386 21.9999 16.9999 21.9999Z"),
                };
                aliases.UserDefined["masaStack"] = new SvgPath[]
                {
                    new("M7 2C4.23858 2 2 4.23857 2 7C2 9.76142 4.23858 12 7 12H12V7C12 4.23857 9.76142 2 7 2Z"),
                    new(
                        "M2 7C2 7 2 7 2 7V17C2 19.7614 4.23858 22 7 22C9.76142 22 12 19.7614 12 17L12 12H7C4.23858 12 2 9.76142 2 7Z",
                        new Dictionary<string, object?>() { { "fill-opacity", "0.5" } }),
                    new("M22 7C22 4.23857 19.7614 2 17 2C14.2386 2 12 4.23857 12 7V12H17C19.7614 12 22 9.76142 22 7Z",
                        new Dictionary<string, object?>() { { "fill-opacity", "0.5" } }),
                    new(
                        "M12 7V17C12 19.7614 14.2386 22 17 22C19.7614 22 22 19.7614 22 17V7.00926C21.995 9.76642 19.7583 12 17 12H12L12 7Z"),
                };
                aliases.UserDefined["wechat"] = new SvgPath(
                    "M9.5,4C5.36,4 2,6.69 2,10C2,11.89 3.08,13.56 4.78,14.66L4,17L6.5,15.5C7.39,15.81 8.37,16 9.41,16C9.15,15.37 9,14.7 9,14C9,10.69 12.13,8 16,8C16.19,8 16.38,8 16.56,8.03C15.54,5.69 12.78,4 9.5,4M6.5,6.5A1,1 0 0,1 7.5,7.5A1,1 0 0,1 6.5,8.5A1,1 0 0,1 5.5,7.5A1,1 0 0,1 6.5,6.5M11.5,6.5A1,1 0 0,1 12.5,7.5A1,1 0 0,1 11.5,8.5A1,1 0 0,1 10.5,7.5A1,1 0 0,1 11.5,6.5M16,9C12.69,9 10,11.24 10,14C10,16.76 12.69,19 16,19C16.67,19 17.31,18.92 17.91,18.75L20,20L19.38,18.13C20.95,17.22 22,15.71 22,14C22,11.24 19.31,9 16,9M14,11.5A1,1 0 0,1 15,12.5A1,1 0 0,1 14,13.5A1,1 0 0,1 13,12.5A1,1 0 0,1 14,11.5M18,11.5A1,1 0 0,1 19,12.5A1,1 0 0,1 18,13.5A1,1 0 0,1 17,12.5A1,1 0 0,1 18,11.5Z");
                aliases.UserDefined["languageJavascript"] = new SvgPath(
                    "M3,3H21V21H3V3M7.73,18.04C8.13,18.89 8.92,19.59 10.27,19.59C11.77,19.59 12.8,18.79 12.8,17.04V11.26H11.1V17C11.1,17.86 10.75,18.08 10.2,18.08C9.62,18.08 9.38,17.68 9.11,17.21L7.73,18.04M13.71,17.86C14.21,18.84 15.22,19.59 16.8,19.59C18.4,19.59 19.6,18.76 19.6,17.23C19.6,15.82 18.79,15.19 17.35,14.57L16.93,14.39C16.2,14.08 15.89,13.87 15.89,13.37C15.89,12.96 16.2,12.64 16.7,12.64C17.18,12.64 17.5,12.85 17.79,13.37L19.1,12.5C18.55,11.54 17.77,11.17 16.7,11.17C15.19,11.17 14.22,12.13 14.22,13.4C14.22,14.78 15.03,15.43 16.25,15.95L16.67,16.13C17.45,16.47 17.91,16.68 17.91,17.26C17.91,17.74 17.46,18.09 16.76,18.09C15.93,18.09 15.45,17.66 15.09,17.06L13.71,17.86Z");
                aliases.UserDefined["nuget"] = new SvgPath(
                    "M17.67 21.633a3.995 3.995 0 1 1 0-7.99 3.995 3.995 0 0 1 0 7.99m-7.969-9.157a2.497 2.497 0 1 1 0-4.994 2.497 2.497 0 0 1 0 4.994m8.145-7.795h-6.667a6.156 6.156 0 0 0-6.154 6.155v6.667a6.154 6.154 0 0 0 6.154 6.154h6.667A6.154 6.154 0 0 0 24 17.503v-6.667a6.155 6.155 0 0 0-6.154-6.155M3.995 2.339a1.998 1.998 0 1 1-3.996 0 1.998 1.998 0 0 1 3.996 0");
                aliases.UserDefined["materialDesign"] = new SvgPath(
                    "M21,12C21,9.97 20.33,8.09 19,6.38V17.63C20.33,15.97 21,14.09 21,12M17.63,19H6.38C7.06,19.55 7.95,20 9.05,20.41C10.14,20.8 11.13,21 12,21C12.88,21 13.86,20.8 14.95,20.41C16.05,20 16.94,19.55 17.63,19M11,17L7,9V17H11M17,9L13,17H17V9M12,14.53L15.75,7H8.25L12,14.53M17.63,5C15.97,3.67 14.09,3 12,3C9.91,3 8.03,3.67 6.38,5H17.63M5,17.63V6.38C3.67,8.09 3,9.97 3,12C3,14.09 3.67,15.97 5,17.63M23,12C23,15.03 21.94,17.63 19.78,19.78C17.63,21.94 15.03,23 12,23C8.97,23 6.38,21.94 4.22,19.78C2.06,17.63 1,15.03 1,12C1,8.97 2.06,6.38 4.22,4.22C6.38,2.06 8.97,1 12,1C15.03,1 17.63,2.06 19.78,4.22C21.94,6.38 23,8.97 23,12Z");
            });
        });

        services.AddMasaBlazorDocs();

        return masaBlazorBuilder;
    }
}