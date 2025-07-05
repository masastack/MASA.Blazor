namespace Masa.Blazor;

public class ThemeVariables
{
    public float BorderOpacity { get; set; }
    public float IdleOpacity { get; set; }
    public float HoverOpacity { get; set; }
    public float FocusOpacity { get; set; }
    public float DisabledOpacity { get; set; }
    public float ActivatedOpacity { get; set; }
    public float HighlightOpacity { get; set; }
    public float HighEmphasisOpacity { get; set; }
    public float MediumEmphasisOpacity { get; set; }
    public float LowEmphasisOpacity { get; set; }

    public override string ToString()
    {
        return $"""
                  --m-border-opacity: {BorderOpacity.ToString(CultureInfo.InvariantCulture)};
                  --m-idle-opacity: {IdleOpacity.ToString(CultureInfo.InvariantCulture)};
                  --m-hover-opacity: {HoverOpacity.ToString(CultureInfo.InvariantCulture)};
                  --m-focus-opacity: {FocusOpacity.ToString(CultureInfo.InvariantCulture)};
                  --m-disabled-opacity: {DisabledOpacity.ToString(CultureInfo.InvariantCulture)};
                  --m-activated-opacity: {ActivatedOpacity.ToString(CultureInfo.InvariantCulture)};
                  --m-highlight-opacity: {HighlightOpacity.ToString(CultureInfo.InvariantCulture)};
                  --m-high-emphasis-opacity: {HighEmphasisOpacity.ToString(CultureInfo.InvariantCulture)};
                  --m-medium-emphasis-opacity: {MediumEmphasisOpacity.ToString(CultureInfo.InvariantCulture)};
                  --m-low-emphasis-opacity: {LowEmphasisOpacity.ToString(CultureInfo.InvariantCulture)};
                """;
    }
}