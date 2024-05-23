namespace BlazorComponent
{
    public class SelectSelectionProps<TItem>
    {
        public SelectSelectionProps(TItem item, int index, bool selected, bool disabled)
        {
            Item = item;
            Index = index;
            Selected = selected;
            Disabled = disabled;
        }

        public TItem Item { get; }

        public int Index { get; }

        public bool Selected { get; }

        public bool Disabled { get; }
    }
}
