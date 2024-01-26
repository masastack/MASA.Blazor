namespace Masa.Docs.Shared.Pages;

public partial class Home : ComponentBase
{
    [Parameter]
    [SupplyParameterFromQuery]
    public string Product { get; set; } = "stack";

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Product ??= "stack";
    }

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
        轻松地通过编程的方式改变应用程序的颜色。重新构建默认样式表并根据您的特定需求自定义框架的各个方面。
        在 Material Design 2 颜色规则的基础上，引入了 Material Design 3 中 `On[Rule]` 的规则的支持。
        """;

    private string _blazorUtilityClassesIntro =
        """
        MASA Blazor provides a set of utility classes that can be used to quickly build a UI,
        including: [Border radius](/blazor/styles-and-animations/border-radius),
        [Colors](/blazor/styles-and-animations/color), 
        [Display helpers](/blazor/styles-and-animations/display-helpers),
        [Elevation](/blazor/styles-and-animations/elevation),
        [Flex](/blazor/styles-and-animations/flex),
        [Float](/blazor/styles-and-animations/floats),
        [Overflow](/blazor/styles-and-animations/overflow),
        [Spacing](/blazor/styles-and-animations/spacing),
        [Text and typography](/blazor/styles-and-animations/text-and-typography).
        """;

    private string _blazorComponentsIntro =
        """
        MASA Blazor 提供基础组件、预置组件、JS模块和实验性组件。
        """;
}
