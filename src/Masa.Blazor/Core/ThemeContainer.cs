namespace Masa.Blazor.Core;

public abstract class ThemeContainer : Container
{
    [CascadingParameter(Name = "MasaBlazorCascadingTheme")]
    public string CascadingTheme { get; set; } = null!;

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [Parameter] public string? Theme { get; set; }

    public bool IsDark => ComputedTheme == "dark";

    public string ComputedTheme
    {
        get
        {
            if (Theme != null)
            {
                return Theme;
            }

            if (Dark)
            {
                return "dark";
            }

            if (Light)
            {
                return "light";
            }

            return CascadingTheme;
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<string>>(0);
        builder.AddAttribute(1, "Value", ComputedTheme);
        builder.AddAttribute(2, "Name", "MasaBlazorCascadingTheme");
        builder.AddAttribute(3, "ChildContent", (RenderFragment)base.BuildRenderTree);
        builder.CloseComponent();
    }
}