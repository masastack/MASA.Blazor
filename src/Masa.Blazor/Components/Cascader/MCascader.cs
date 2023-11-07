using BlazorComponent.JSInterop;
using BlazorComponent.Web;

namespace Masa.Blazor
{
    public partial class MCascader<TItem, TValue> : MSelect<TItem, TValue, TValue>, ICascader<TItem, TValue>
    {
        [Parameter]
        public bool ChangeOnSelect { get; set; }

        [Parameter, MassApiParameter(true)]
        public bool ShowAllLevels { get; set; } = true;

        [Parameter, EditorRequired]
        public Func<TItem, List<TItem>> ItemChildren { get; set; } = null!;

        [Parameter]
        public Func<TItem, Task>? LoadChildren { get; set; }

        [Parameter]
        public override bool Outlined { get; set; }

        private List<TItem> _selectedCascadeItems = new();
        private List<BCascaderColumn<TItem, TValue>> _cascaderLists = new();

        public override Action<TextFieldNumberProperty>? NumberProps { get; set; }

        protected override IList<TItem> SelectedItems => FindSelectedItems(Items).ToList();

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            ItemChildren.ThrowIfNull(ComponentName);
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            var valueItem = SelectedItems.FirstOrDefault();
            if (valueItem is not null)
            {
                _selectedCascadeItems.Clear();
                FindAllLevelItems(valueItem, ComputedItems, ref _selectedCascadeItems);
                _selectedCascadeItems.Reverse();
            }
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder => { cssBuilder.Add("m-cascader"); })
                .Apply("columns", cssBuilder => { cssBuilder.Add("m-cascader__columns"); });

            AbstractProvider
                .ApplyCascaderDefault<TItem, TValue>()
                .Apply<BItemGroup, MListItemGroup>()
                .Merge<BMenu, MMenu>(attrs =>
                {
                    attrs[nameof(MMenu.OffsetY)] = true;
                    attrs[nameof(MMenu.MinWidth)] = (StringNumber)(Dense ? 120 : 180);
                    attrs[nameof(MMenu.CloseOnContentClick)] = false;
                })
                .Apply(typeof(BCascaderColumn<,>), typeof(MCascaderColumn<TItem, TValue>), attrs =>
                {
                    attrs[nameof(ChangeOnSelect)] = ChangeOnSelect;
                    attrs[nameof(Dense)] = Dense;
                    attrs[nameof(ItemValue)] = ItemValue;
                    attrs[nameof(SelectedItems)] = _selectedCascadeItems;
                    attrs[nameof(ItemText)] = ItemText;
                    attrs[nameof(LoadChildren)] = LoadChildren;
                    attrs[nameof(ItemChildren)] = ItemChildren;
                    attrs[nameof(MCascaderColumn<TItem, TValue>.OnSelect)] = CreateEventCallback<(TItem, bool, int)>(HandleOnSelect);
                });
        }

        public void Register(BCascaderColumn<TItem, TValue> cascaderColumn)
        {
            _cascaderLists.Add(cascaderColumn);
        }

        private async Task HandleOnSelect((TItem item, bool isLast, int columnIndex) args)
        {
            if (ChangeOnSelect)
            {
                await SelectItem(args.item, closeOnSelect: args.isLast);
            }
            else if (args.isLast)
            {
                await SelectItem(args.item);
            }

            NextTick(async () =>
            {
                var selector = $"{MMenu.ContentElement.GetSelector()} .m-cascader__column:nth-child({args.columnIndex + 1})";
                await Js.ScrollIntoParentView(selector, true, true);
            });
        }

        protected override async Task OnMenuBeforeShowContent()
        {
            await base.OnMenuBeforeShowContent();

            _cascaderLists.ForEach(cascaderList => cascaderList.ActiveSelectedOrNot());
        }

        protected override async Task OnMenuAfterShowContent(bool isLazyContent)
        {
            await base.OnMenuAfterShowContent(isLazyContent);

            await ScrollToInlineStartAsync();

            await _cascaderLists.ForEachAsync(async cascaderList => await cascaderList.ScrollToSelected());
        }

        private async Task ScrollToInlineStartAsync()
        {
            if (MMenu.ContentElement.Context is not null)
            {
                await Js.ScrollTo($"{MMenu.ContentElement.GetSelector()} > .m-cascader__columns", top: null, left: 0);
            }
        }

        private IEnumerable<TItem> FindSelectedItems(IList<TItem> items)
        {
            var selectedItems = items.Where(item => InternalValues.Contains(ItemValue(item))).ToList();
            if (selectedItems.Any())
            {
                return selectedItems;
            }

            foreach (var item in items)
            {
                var children = ItemChildren(item);
                if (children != null && children.Any())
                {
                    var childrenSelectedItems = FindSelectedItems(children).ToList();
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
            return !ShowAllLevels
                ? base.GetText(item)
                : string.Join(" / ", _selectedCascadeItems.Select(base.GetText));
        }

        private bool FindAllLevelItems(TItem item, IList<TItem> searchItems, ref List<TItem> allLevelItems)
        {
            allLevelItems ??= new List<TItem>();

            if (searchItems.Contains(item))
            {
                allLevelItems.Add(item);
                return true;
            }

            foreach (var searchItem in searchItems)
            {
                var children = ItemChildren(searchItem);
                if (children is { Count: > 0 })
                {
                    var find = FindAllLevelItems(item, children, ref allLevelItems);
                    if (find)
                    {
                        allLevelItems.Add(searchItem);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
