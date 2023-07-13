namespace Masa.Blazor.Playground.Pages;

public class MShowTransitionElement : MToggleTransitionElement
{
    protected override string? ComputedStyle
    {
        get
        {
            if (!LazyValue)
            {
                return string.Join(";", base.ComputedStyle, "display:none").TrimStart(';');
            }

            return base.ComputedStyle;
        }
    }
}
