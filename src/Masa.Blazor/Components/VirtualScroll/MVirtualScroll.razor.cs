using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MVirtualScroll<TItem> : MasaComponentBase, IMeasurable
{
    [Parameter] public StringNumber? Height { get; set; }

    [Parameter] public StringNumber? MaxHeight { get; set; }

    [Parameter] public StringNumber? MaxWidth { get; set; }

    [Parameter] public StringNumber? MinHeight { get; set; }

    [Parameter] public StringNumber? MinWidth { get; set; }

    [Parameter] public StringNumber? Width { get; set; }

    [Parameter] public ICollection<TItem>? Items { get; set; }

    [Parameter] public RenderFragment<TItem>? ItemContent { get; set; }

    [Parameter] public RenderFragment? FooterContent { get; set; }

    [Parameter, MasaApiParameter(50)] public float ItemSize { get; set; } = 50;

    [Parameter, MasaApiParameter(3)] public int OverscanCount { get; set; } = 3;

    private Block _block = new("m-virtual-scroll");

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _block.ToString();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return StyleBuilder.Create().AddMeasurable(this).GenerateCssStyles();
    }
}