namespace BlazorComponent;

public class SelectListItemProps<TItem>
{
    public SelectListItemProps(TItem item, Func<Task> onClick)
    {
        Item = item;
        OnClick = onClick;
    }

    public TItem Item { get; }

    public Func<Task> OnClick { get; }
}
