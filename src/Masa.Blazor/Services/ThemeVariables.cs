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
                --m-border-opacity: {BorderOpacity};
                  --m-idle-opacity: {IdleOpacity};
                  --m-hover-opacity: {HoverOpacity};
                  --m-focus-opacity: {FocusOpacity};
                  --m-disabled-opacity: {DisabledOpacity};
                  --m-activated-opacity: {ActivatedOpacity};
                  --m-highlight-opacity: {HighlightOpacity};
                  --m-high-emphasis-opacity: {HighEmphasisOpacity};
                  --m-medium-emphasis-opacity: {MediumEmphasisOpacity};
                  --m-low-emphasis-opacity: {LowEmphasisOpacity};
                """;
    }
}