#nullable enable

namespace Masa.Blazor.Presets;

public partial class PBlockText
{
    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Primary { get; set; }

    [Parameter]
    public string? PrimaryClass { get; set; }

    [Parameter]
    public RenderFragment? PrimaryContent { get; set; }

    [Parameter]
    public string? PrimaryStyle { get; set; }

    [Parameter]
    public string? Secondary { get; set; }

    [Parameter]
    public string? SecondaryClass { get; set; }

    [Parameter]
    public RenderFragment? SecondaryContent { get; set; }

    [Parameter]
    public string? SecondaryStyle { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public IEnumerable<BlockTextTag>? Tags { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Tags ??= Enumerable.Empty<BlockTextTag>();
    }
}