namespace Masa.Blazor;

public partial class MSortable<TItem> : MSortableProviderBase<TItem>
{
    [Parameter] public RenderFragment<TItem>? ChildContent { get; set; }

    [Parameter] [MasaApiParameter("div")] public string? Tag { get; set; } = "div";

    [Parameter] [MasaApiParameter("div")] public string? ItemTag { get; set; } = "div";

    private Block _block = new("m-sortable");

    protected override string ContainerSelector => Ref.GetSelector();
}