namespace Masa.Blazor;

public partial class MSortable<TItem> : MSortableProviderBase<TItem>
{
    [Parameter] public RenderFragment<TItem>? ChildContent { get; set; }

    [Parameter] [MasaApiParameter("div")] public string? Tag { get; set; } = "div";

    [Parameter] [MasaApiParameter("div")] public string? ItemTag { get; set; } = "div";

    [Parameter] public override string? Handle { get; set; }

    private static Block _block = new("m-sortable");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

    protected override string? ContainerSelector => Ref.TryGetSelector(out var selector) ? selector : null;

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder.Add(Disabled).Build();
    }
}