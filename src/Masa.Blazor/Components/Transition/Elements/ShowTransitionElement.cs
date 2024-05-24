namespace Masa.Blazor;

public class ShowTransitionElement : ToggleableTransitionElement
{
    protected override string ComputedStyle
    {
        get
        {
            if (LazyValue) return base.ComputedStyle;

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(base.ComputedStyle);
            stringBuilder.Append(" display:none;");
            return stringBuilder.ToString().Trim();
        }
    }
}