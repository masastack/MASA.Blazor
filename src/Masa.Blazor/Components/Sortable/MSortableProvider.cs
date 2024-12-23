namespace Masa.Blazor;

public class MSortableProvider<TItem> : MSortableProviderBase<TItem>
{
    [Parameter] public string? Container { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedOn = "v1.7.3")]
    public ElementReference ContainerRef { get; set; }

    [Parameter] public RenderFragment<SortableItemContext<TItem>>? ItemContent { get; set; }

    protected override string? ContainerSelector
    {
        get
        {
            if (Container is not null && ContainerRef.Id is not null)
            {
                throw new InvalidOperationException(
                    $"{nameof(MSortableProvider<TItem>)} requires one of {nameof(Container)} or {nameof(ContainerRef)}, but both were provided.");
            }

            return Container ?? (ContainerRef.TryGetSelector(out var selector) ? selector : null);
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.AddContent(0, sb =>
        {
            foreach (var item in Items)
            {
                Dictionary<string, object?> attrs = new()
                {
                    { "data-id", ItemKey(item) }
                };
                var context = new SortableItemContext<TItem>(item, attrs);
                sb.AddContent(1, ItemContent?.Invoke(context));
            }
        });
    }
}