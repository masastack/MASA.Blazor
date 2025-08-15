namespace Masa.Blazor;

public partial class MCell : MasaComponentBase
{
    [CascadingParameter]
    private MCellGroup? CellGroup { get; set; }

    [Parameter] public string? Title { get; set; }

    [Parameter] public string? Subtitle { get; set; }

    [Parameter] public string? Value { get; set; }

    [Parameter] public string? PrependIcon { get; set; }

    [Parameter] public string? PrependAvatar { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public RenderFragment? TitleContent { get; set; }

    [Parameter] public RenderFragment? SubtitleContent { get; set; }

    [Parameter] public RenderFragment? PrependIconContent { get; set; }

    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

    [Parameter] public string? Href { get; set; }

    [Parameter] public bool HideDivider { get; set; }

    [Parameter]
    [MasaApiParameter("surface")]
    public string? BackgroundColor { get; set; } = "surface";

    [Parameter] public bool Ripple { get; set; } = true;

    private static Block _block = new("m-cell");
    private readonly ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

    private bool ShowRightArrow => OnClick.HasDelegate || Href != null;

    private bool AppendDivider => !HideDivider && CellGroup != null && CellGroup.AppendDivider(this);
    
    private bool ComputedRipple => ShowRightArrow && Ripple;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        CellGroup?.AddCell(this);
    }

    protected override IEnumerable<string?> BuildComponentClass()
    {
        var css = _modifierBuilder.AddBackgroundColor(BackgroundColor).Build();
        return [css, ..base.BuildComponentClass()];
    }
}