namespace Masa.Blazor;

public class ThemeVariables
{
    public float IdleOpacity { get; set; } = 0.04f;
    public float HoverOpacity { get; set; } = 0.04f;
    public float FocusOpacity { get; set; } = 0.12f;
    public float DisabledOpacity { get; set; } = 0.38f;
    public float SelectedOpacity { get; set; } = 0.08f;
    public float ActivatedOpacity { get; set; } = 0.12f;
    public float HighlightOpacity { get; set; } = 0.32f;
    public float HighEmphasisOpacity { get; set; } = 0.87f;
    public float MediumEmphasisOpacity { get; set; } = 0.6f;
    public float LowEmphasisOpacity { get; set; } = 0.38f;

    public override string ToString()
    {
        return $"""
                --m-idle-opacity: {IdleOpacity};
                --m-hover-opacity: {HoverOpacity};
                --m-focus-opacity: {FocusOpacity};
                --m-disabled-opacity: {DisabledOpacity};
                --m-selected-opacity: {SelectedOpacity};
                --m-activated-opacity: {ActivatedOpacity};
                --m-highlight-opacity: {HighlightOpacity};
                --m-high-emphasis-opacity: {HighEmphasisOpacity};
                --m-medium-emphasis-opacity: {MediumEmphasisOpacity};
                --m-low-emphasis-opacity: {LowEmphasisOpacity};
                """;
    }
}