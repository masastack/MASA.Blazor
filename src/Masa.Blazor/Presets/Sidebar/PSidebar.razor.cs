namespace Masa.Blazor.Presets
{
    public partial class PSidebar<TItem>
    {
        private List<SidebarItem<TItem>> _sidebarItems = new();

        [Parameter]
        public List<TItem> Items { get; set; } = new();

        [Parameter]
        public StringNumber Value
        {
            get => _value;
            set
            {
                _value = value;
                _listItemGroupValue = value;
            }
        }

        private StringNumber _listItemGroupValue;
        private StringNumber _value;

        private async Task ListItemGroupValueChanged(StringNumber v)
        {
            _listItemGroupValue = v;
            await UpdateValue(v);
        }

        [Parameter]
        public EventCallback<StringNumber> ValueChanged { get; set; }

        private async Task UpdateValue(StringNumber value)
        {
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(value);
            }
            else
            {
                Value = value;
            }

            _listItemGroupValue = Value;
        }

        [Parameter]
        public Func<TItem, StringNumber> Key { get; set; }

        [Parameter]
        public Func<TItem, string> Title { get; set; }

        [Parameter]
        public Func<TItem, string> Icon { get; set; }

        [Parameter]
        public Func<TItem, List<TItem>> Children { get; set; }

        [Parameter]
        public EventCallback<TItem> OnClick { get; set; }

        [Parameter]
        public string Color { get; set; } = "primary";

        #region List parameters

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Shaped { get; set; }

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Nav { get; set; }

        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public StringNumber MinHeight { get; set; }

        [Parameter]
        public StringNumber MinWidth { get; set; }

        [Parameter]
        public StringNumber MaxHeight { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

        #endregion

        protected override void OnInitialized()
        {
            _listItemGroupValue = Value;

            _sidebarItems = ConvertToSidebarItems(Items, Value);
        }

        private List<SidebarItem<TItem>> ConvertToSidebarItems(List<TItem> items, StringNumber activeValue)
        {
            var sidebarItems = new List<SidebarItem<TItem>>();

            foreach (var item in items)
            {
                var title = Title?.Invoke(item);
                var icon = Icon?.Invoke(item);
                var value = Key?.Invoke(item);
                var children = Children?.Invoke(item);

                var sidebarItem = new SidebarItem<TItem>
                {
                    Title = title,
                    Icon = icon,
                    Key = value,
                    Expanded = value == activeValue,
                    Data = item
                };

                if (children != null && children.Count > 0)
                {
                    sidebarItem.Children = ConvertToSidebarItems(children, activeValue);
                    sidebarItem.Expanded = sidebarItem.Children.Any(c => c.Expanded);
                }

                sidebarItems.Add(sidebarItem);
            }

            return sidebarItems;
        }
    }

    internal class SidebarItem<T>
    {
        public string Title { get; set; }

        public string Icon { get; set; }

        public StringNumber Key { get; set; }

        public bool Expanded { get; set; }

        public T Data { get; set; }

        public List<SidebarItem<T>> Children { get; set; }
    }
}