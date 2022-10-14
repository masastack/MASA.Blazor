using BlazorComponent.JSInterop;
using BlazorComponent.Web;

namespace Masa.Blazor
{
    public partial class MCascader<TItem, TValue> : MSelect<TItem, TValue, TValue>, ICascader<TItem, TValue>
    {
        [Parameter]
        public bool ChangeOnSelect { get; set; }

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

        protected override IList<TItem> SelectedItems => FindSelectedItems(Items).ToList();

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-cascader");
                })
                .Apply("lists", cssBuilder => { cssBuilder.Add("m-cascader-lists"); });

            AbstractProvider
                .ApplyCascaderDefault<TItem, TValue>()
                .Merge<BMenu, MMenu>(attrs =>
                {
                    attrs[nameof(MMenu.OffsetY)] = true;
                    attrs[nameof(MMenu.MinWidth)] = (StringNumber)(Dense ? 120 : 180);
                    attrs[nameof(MMenu.CloseOnContentClick)] = false;
                })
                .Apply(typeof(BCascaderList<,>), typeof(MCascaderList<TItem, TValue>), attrs =>
                {
                    attrs[nameof(ChangeOnSelect)] = ChangeOnSelect;
                    attrs[nameof(Dense)] = Dense;
                    attrs[nameof(ItemText)] = ItemText;
                    attrs[nameof(LoadChildren)] = LoadChildren;
                    attrs[nameof(ItemChildren)] = ItemChildren;
                    attrs[nameof(MCascaderList<TItem, TValue>.OnSelect)] = CreateEventCallback<(TItem item, bool close)>(HandleOnSelect);
                });
        }

        private async Task HandleOnSelect((TItem item, bool isLast) arg)
        {
            if (ChangeOnSelect)
            {
                await SelectItem(arg.item, closeOnSelect: arg.isLast);
            }
            else if (arg.isLast)
            {
                await SelectItem(arg.item);
                await ScrollToInlineStartAsync(); // todo: not work in sometimes.
            }
        }

        protected override async void OnMenuActiveChange(bool val)
        {
            base.OnMenuActiveChange(val);
            if (!val)
            {
                await ScrollToInlineStartAsync();
            }
        }

        private async Task ScrollToInlineStartAsync()
        {
            await Js.ScrollTo($"{MMenu.ContentElement.GetSelector()} > .m-cascader-lists", top: null, left: 0);
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
