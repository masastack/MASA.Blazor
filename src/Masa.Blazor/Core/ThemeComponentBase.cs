namespace Masa.Blazor.Core;

public abstract class ThemeComponentBase : MasaComponentBase
{
    [CascadingParameter(Name = "IsDark")]
    public bool CascadingIsDark { get; set; }

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
}