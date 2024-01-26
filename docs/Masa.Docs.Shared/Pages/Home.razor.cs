namespace Masa.Docs.Shared.Pages;

public partial class Home : ComponentBase
{
    [Parameter]
    [SupplyParameterFromQuery]
    public string Product { get; set; } = "stack";

    private string _blazorGettingStartedIntro =
        """
        To get started quickly, you can use the MASA.Template template to quickly create a project.
        ```bash
        dotnet new install MASA.Template
        ```
        ```bash
        dotnet new blazor -o BlazorApp -ai
        ```
        """;

    private string _blazorIconIntro =
        """
        Out of the box, MASA Blazor supports 5 popular icon font libraries:
        [Material Design Icons](https://materialdesignicons.com/),
        [Material Icons](https://fonts.google.com/icons),
        [Font Awesome 4](https://fontawesome.com/v4/icons/),
        [Font Awesome 5](https://fontawesome.com/v5/search?m=free)
        and [Font Awesome 6](https://fontawesome.com/v6/search?o=r&m=free).
        """;

    private string _blazorThemeIntro =
        """
        在 Material Deisgn 2 颜色规则的基础上，引入了 Material Design 3 中 `On<Rule>` 的规则的支持。
        ```cs
        builder.Services.AddMasaBlazor(options =>
        {
            options.ConfigureTheme(theme =>
            {
                theme.Themes.Light.Primary = "#4f33ff";
                theme.Themes.Light.Secondary = "#5e5c71";
                theme.Themes.Light.Accent = "#006C4F";
                theme.Themes.Light.Error = "#BA1A1A";
                theme.Themes.Light.OnSurface = "#1C1B1F";

                theme.Themes.Dark.Primary = "#C5C0FF";
                theme.Themes.Dark.Secondary = "#C7C4DC";
                theme.Themes.Dark.Accent = "#67DBAF";
                theme.Themes.Dark.Error = "#FFB4AB";
                theme.Themes.Dark.Surface = "#131316";
                theme.Themes.Dark.OnPrimary = "#2400A2";
                theme.Themes.Dark.OnSecondary = "#302E42";
                theme.Themes.Dark.OnAccent = "#003827";
                theme.Themes.Dark.OnSurface = "#C9C5CA";
            });
        });
        ```
        """;
}
