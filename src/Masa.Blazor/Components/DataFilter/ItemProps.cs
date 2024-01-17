namespace Masa.Blazor
{
    public class ItemProps<TItem>
    {
        public ItemProps(int index, TItem item)
        {
            Index = index;
            Item = item;
        }

        public int Index { get; }

        public TItem Item { get; }
    }
}
