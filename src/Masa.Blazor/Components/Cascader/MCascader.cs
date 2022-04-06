namespace Masa.Blazor
{
    public partial class MCascader<TItem, TValue> : MSelect<TItem, TValue, TValue>, ICascader<TItem, TValue>
    {
        [Parameter]
        public bool ShowAllLevels { get; set; } = true;

        [EditorRequired]
        [Parameter]
        public Func<TItem, List<TItem>> ItemChildren { get; set; }

        [Parameter]
        public Func<TItem, Task> LoadChildren { get; set; }

        [Parameter]
        public override bool Outlined { get; set; } = true;

        public override Action<TextFieldNumberProperty> NumberProps { get; set; }

        protected override IList<TItem> SelectedItems
        {
            get
            {
                return FindSelectedItems(Items).ToList();
            }
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-cascader");
                });

            AbstractProvider
                .ApplyCascaderDefault<TItem, TValue>()
                .Merge<BMenu, MMenu>(attrs =>
                {
                    attrs[nameof(MMenu.OffsetY)] = true;
                    attrs[nameof(MMenu.MinWidth)] = (StringNumber)(Dense ? 120 : 180);
                    attrs[nameof(MMenu.CloseOnContentClick)] = false;
                    attrs[nameof(MMenu.ContentStyle)] = "display:flex";
                })
                .Apply(typeof(BCascaderList<,>), typeof(MCascaderList<TItem, TValue>), attrs =>
                {
                    attrs[nameof(Dense)] = Dense;
                    attrs[nameof(ItemText)] = ItemText;
                    attrs[nameof(LoadChildren)] = LoadChildren;
                    attrs[nameof(ItemChildren)] = ItemChildren;
                    attrs[nameof(MCascaderList<TItem, TValue>.OnSelect)] = CreateEventCallback<TItem>(SelectItemsAsync);
                });
        }

        protected IEnumerable<TItem> FindSelectedItems(IEnumerable<TItem> items)
        {
            var selectedItems = items.Where(item => InternalValues.Contains(ItemValue(item)));
            if (selectedItems.Any())
            {
                return selectedItems;
            }

            foreach (var item in items)
            {
                var children = ItemChildren(item);
                if (children != null && children.Any())
                {
                    var childrenSelectedItems = FindSelectedItems(children);
                    if (childrenSelectedItems.Any())
                    {
                        return childrenSelectedItems;
                    }
                }
            }

            return Array.Empty<TItem>();
        }

        protected override string GetText(TItem item)
        {
            if (!ShowAllLevels)
            {
                return base.GetText(item);
            }

            var allLevelItems = new List<TItem>();
            FindAllLevelItems(item, ComputedItems, ref allLevelItems);

            allLevelItems.Reverse();
            return string.Join(" / ", allLevelItems.Select(base.GetText));
        }

        private bool FindAllLevelItems(TItem item, IList<TItem> searchItems, ref List<TItem> allLevelItems)
        {
            if (searchItems.Contains(item))
            {
                allLevelItems.Add(item);
                return true;
            }
            else
            {
                foreach (var searchItem in searchItems)
                {
                    var children = ItemChildren(searchItem);
                    if (children != null && children.Count > 0)
                    {
                        var find = FindAllLevelItems(item, children, ref allLevelItems);
                        if (find)
                        {
                            allLevelItems.Add(searchItem);
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
