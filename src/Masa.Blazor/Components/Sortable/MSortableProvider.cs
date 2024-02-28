namespace Masa.Blazor;

public class MSortableProvider<TItem> : MSortableProviderBase<TItem>
{
    [Parameter] [EditorRequired] public string Container { get; set; } = null!;

    [Parameter] public RenderFragment<SortableItemContext<TItem>>? ItemContent { get; set; }

    protected override string ContainerSelector => Container;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Container.ThrowIfNull(ComponentName);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.AddContent(0, sb =>
        {
            var i = 0;
            foreach (var item in Items)
            {
                Dictionary<string, object?> attrs = new()
                {
                    { "data-id", ItemKey(item) }
                };
                var context = new SortableItemContext<TItem>(item, attrs);
                sb.AddContent(i++, ItemContent?.Invoke(context));
            }
        });
    }
}