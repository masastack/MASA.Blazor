namespace Masa.Blazor
{
    public class MDataIterator<TItem> : BDataIterator<TItem>, IDataIterator<TItem>, ILoadable
    {
        [Inject]
        protected I18n I18n { get; set; } = null!;

        [Parameter]
        public RenderFragment<(int PageStart, int PageStop, int ItemsLength)>? PageTextContent { get; set; }

        [Parameter]
        public Action<IDataFooterParameters>? FooterProps { get; set; }

        [Parameter]
        public Func<TItem, string>? ItemKey { get; set; }

        [Parameter]
        public string? Color { get; set; }

        [Parameter]
        public bool SingleSelect { get; set; }

        [Parameter]
        public List<TItem>? Expanded { get; set; }

        [Parameter]
        public bool SingleExpand { get; set; }

        [Parameter]
        public StringBoolean? Loading { get; set; }

        [Parameter]
        public string? NoResultsText { get; set; }

        [Parameter]
        public string? NoDataText { get; set; }

        [Parameter]
        public string? LoadingText { get; set; }

        [Parameter]
        public bool HideDefaultFooter { get; set; }

        [Parameter]
        public Func<TItem, bool>? SelectableKey { get; set; }

        [Parameter]
        public RenderFragment<(IEnumerable<TItem> Items, Func<TItem, bool> IsExpanded, Action<TItem, bool> Expand)>? ChildContent { get; set; }

        RenderFragment? IDataIterator<TItem>.ChildContent => ChildContent?.Invoke((ComputedItems, IsExpanded, Expand));

        [Parameter]
        public RenderFragment<ItemProps<TItem>>? ItemContent { get; set; }

        [Parameter]
        public RenderFragment? LoadingContent { get; set; }

        [Parameter]
        public RenderFragment? NoDataContent { get; set; }

        [Parameter]
        public RenderFragment? NoResultsContent { get; set; }

        [Parameter]
        public RenderFragment? HeaderContent { get; set; }

        [Parameter]
        public RenderFragment? FooterContent { get; set; }

        [Parameter]
        public StringNumber LoaderHeight { get; set; } = 4;

        [Parameter]
        public RenderFragment? ProgressContent { get; set; }

        [Parameter]
        public IEnumerable<TItem>? Value
        {
            get => GetValue<IEnumerable<TItem>>();
            set => SetValue(value);
        }

        [Parameter]
        public EventCallback<IEnumerable<TItem>> ValueChanged { get; set; }

        [Parameter]
        public EventCallback<(TItem, bool)> OnItemSelect { get; set; }

        [Parameter]
        public EventCallback<(TItem, bool)> OnItemExpand { get; set; }

        [Parameter]
        public EventCallback<(IEnumerable<TItem>, bool)> OnToggleSelectAll { get; set; }

        public bool EveryItem => SelectableItems.Any() && SelectableItems.All(IsSelected);

        public bool SomeItems => SelectableItems.Any(IsSelected);

        public IEnumerable<TItem> SelectableItems => ComputedItems.Where(IsSelectable);

        public bool IsEmpty => !Items.Any() || Pagination.ItemsLength == 0;

        protected Dictionary<string, bool> Expansion { get; } = new();

        protected Dictionary<string, bool> Selection { get; } = new();

        public override Task SetParametersAsync(ParameterView parameters)
        {
            NoResultsText = I18n.T("$masaBlazor.dataIterator.noResultsText");
            NoDataText = I18n.T("$masaBlazor.noDataText");
            LoadingText = I18n.T("$masaBlazor.dataIterator.loadingText");

            return base.SetParametersAsync(parameters);
        }

        protected override void RegisterWatchers(PropertyWatcher watcher)
        {
            base.RegisterWatchers(watcher);

            watcher
                .Watch<IEnumerable<TItem>>(nameof(Value), val =>
                {
                    if (val is null) return;
                    
                    var keys = new List<string>();

                    foreach (var item in val)
                    {
                        var key = ItemKey?.Invoke(item);
                        if (key is null) return;

                        Selection[key] = true;

                        keys.Add(key);
                    }

                    // Unselect those in selection but not in Value when updating Value
                    foreach (var (key, _) in Selection)
                    {
                        Selection[key] = keys.Contains(key);
                    }
                });
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-iterator");
                });

            AbstractProvider
                .ApplyDataIteratorDefault<TItem>()
                .Apply<BDataFooter, MDataFooter>(attrs =>
                {
                    attrs[nameof(MDataFooter.PageTextContent)] = PageTextContent;
                    attrs[nameof(MDataFooter.Options)] = InternalOptions;
                    attrs[nameof(MDataFooter.Pagination)] = Pagination;
                    attrs[nameof(MDataFooter.OnOptionsUpdate)] =
                        EventCallback.Factory.Create<Action<DataOptions>>(this, options => UpdateOptions(options));
                    attrs[nameof(MDataFooter.ItemsPerPageAllText)] = I18n.T("$masaBlazor.dataFooter.itemsPerPageAll");
                    attrs[nameof(MDataFooter.Parameters)] = FooterProps;
                });
        }

        public bool IsExpanded(TItem item)
        {
            var key = ItemKey?.Invoke(item);
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            if (Expansion.TryGetValue(key, out var expanded))
            {
                return expanded;
            }

            return false;
        }

        public void Expand(TItem item, bool value = true)
        {
            if (SingleExpand)
            {
                Expansion.Clear();
            }

            var key = ItemKey?.Invoke(item);
            if (string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("ItemKey or key should not be null");
            }

            Expansion[key] = value;

            OnItemExpand.InvokeAsync((item, value));
        }

        public bool IsSelected(TItem item)
        {
            var key = ItemKey?.Invoke(item);
            if (key == null)
            {
                return false;
            }

            if (Selection.TryGetValue(key, out var selected))
            {
                return selected;
            }

            return false;
        }

        public void Select(TItem item, bool value = true)
        {
            if (!IsSelectable(item))
            {
                return;
            }

            if (SingleSelect)
            {
                Selection.Clear();
            }

            var key = ItemKey?.Invoke(item);
            if (string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("ItemKey or key should not be null");
            }

            Selection[key] = value;

            UpdateSelectedItemsAsValue();

            OnItemSelect.InvokeAsync((item, value));
        }

        public bool IsSelectable(TItem item)
        {
            return SelectableKey?.Invoke(item) != false;
        }

        public void ToggleSelectAll(bool value)
        {
            foreach (var item in SelectableItems)
            {
                var key = ItemKey?.Invoke(item);
                if (key == null) continue;
                Selection[key] = value;
            }

            UpdateSelectedItemsAsValue();

            OnToggleSelectAll.InvokeAsync((SelectableItems, value));
        }

        private void UpdateSelectedItemsAsValue()
        {
            var selectedItems = Items.Where(IsSelected);
            if (ValueChanged.HasDelegate)
            {
                ValueChanged.InvokeAsync(selectedItems);
            }
            else
            {
                Value = selectedItems;
            }
        }
    }
}
