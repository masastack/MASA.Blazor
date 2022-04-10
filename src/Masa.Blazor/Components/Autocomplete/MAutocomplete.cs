using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor
{
    public class MAutocomplete<TItem, TItemValue, TValue> : MSelect<TItem, TItemValue, TValue>, IAutocomplete<TItem, TItemValue, TValue>
    {
        private Func<TItem, string, string, bool> _filter;

        [Parameter]
        public bool AllowOverflow { get; set; } = true;

        [Parameter]
        public bool AutoSelectFirst { get; set; }

        [Parameter]
        public Func<TItem, string, string, bool> Filter
        {
            get
            {
                if (_filter == null)
                {
                    _filter = (item, query, text) => text.ToLower().IndexOf(query.ToLower()) > -1;
                }

                return _filter;
            }
            set
            {
                _filter = value;
            }
        }

        [Parameter]
        public bool HideNoData { get; set; }

        [Parameter]
        public bool NoFilter { get; set; }

        [Parameter]
        public string SearchInput { get; set; }

        [Parameter]
        public EventCallback<string> OnSearchInputUpdate { get; set; }

        protected override IList<TItem> ComputedItems => FilteredItems;

        public override Action<TextFieldNumberProperty> NumberProps { get; set; }

        protected IList<TItemValue> SelectedValues
        {
            get
            {
                return SelectedItems.Select(GetValue).ToList();
            }
        }

        protected bool HasDisplayedItems
        {
            get
            {
                return HideSelected ? FilteredItems.Any(item => !HasItem(item)) : FilteredItems.Count > 0;
            }
        }

        protected bool HasItem(TItem item)
        {
            return SelectedValues.IndexOf(GetValue(item)) > -1;
        }

        protected IList<TItem> FilteredItems
        {
            get
            {
                if (!IsSearching || NoFilter || InternalSearch == null)
                {
                    return Items;
                }

                return Items.Where(item => Filter(item, InternalSearch, GetText(item) ?? "")).ToList();
            }
        }

        protected bool IsSearching
        {
            get
            {
                return (Multiple && SearchIsDirty) || (SearchIsDirty && InternalSearch != GetText(SelectedItem));
            }
        }

        protected TItem SelectedItem
        {
            get
            {
                return SelectedItems.FirstOrDefault();
            }
        }

        protected bool SearchIsDirty
        {
            get
            {
                return !string.IsNullOrEmpty(InternalSearch);
            }
        }

        protected string InternalSearch { get; set; }

        protected override Dictionary<string, object> InputAttrs => new()
        {
            { "type", Type },
            { "value", InternalSearch ?? (HasSlot || Multiple ? null : GetText(SelectedItem)) },
            { "autocomplete", "off" }
        };

        protected override BMenuProps GetDefaultMenuProps()
        {
            var props = base.GetDefaultMenuProps();
            props.OffsetY = true;

            // props.OffsetOverflow = true;
            // props.Transition = false;

            return props;
        }

        protected override bool IsDirty => SearchIsDirty || base.IsDirty;

        protected bool HasSlot => HasChips || SelectionContent != null;

        bool IAutocomplete<TItem, TItemValue, TValue>.HasSlot => HasSlot;

        protected override bool MenuCanShow
        {
            get
            {
                return HasDisplayedItems || !HideNoData;
            }
        }

        protected override void OnWatcherInitialized()
        {
            base.OnWatcherInitialized();

            Watcher
                .Watch<string>(nameof(SearchInput), val =>
                {
                    InternalSearch = val;
                });
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-autocomplete");
                });

            AbstractProvider
                .ApplyAutocompleteDefault<TItem, TItemValue, TValue>()
                .Merge(typeof(BSelectList<,,>), typeof(MSelectList<TItem, TItemValue, TValue>), attrs =>
                {
                    attrs[nameof(MSelectList<TItem, TItemValue, TValue>.SearchInput)] = InternalSearch;
                    attrs[nameof(MSelectList<TItem, TItemValue, TValue>.NoFilter)] = NoFilter || !IsSearching || FilteredItems.Count == 0;
                });
        }

        protected override async Task SelectItemsAsync(TItem item)
        {
            await base.SelectItemsAsync(item);
            InternalSearch = null;
        }

        public override async Task HandleOnBlurAsync(FocusEventArgs args)
        {
            InternalSearch = null;
            await base.HandleOnBlurAsync(args);
        }

        public override async Task HandleOnInputAsync(ChangeEventArgs args)
        {
            InternalSearch = args.Value.ToString();
            if (OnSearchInputUpdate.HasDelegate)
            {
                await OnSearchInputUpdate.InvokeAsync(InternalSearch);
            }
        }

        public override async Task HandleOnKeyDownAsync(KeyboardEventArgs args)
        {
            await base.HandleOnKeyDownAsync(args);

            switch (args.Code)
            {
                case "Backspace":
                    IsMenuActive = true;
                    if (Multiple)
                    {
                        var internalValues = InternalValues;
                        if (internalValues.Count > 0 && string.IsNullOrEmpty(InternalSearch))
                        {
                            internalValues.RemoveAt(internalValues.Count - 1);
                            await SetInternalValueAsync((TValue)internalValues);
                        }
                    }
                    else
                    {
                        if (Chips && !EqualityComparer<TValue>.Default.Equals(InternalValue, default) && string.IsNullOrEmpty(InternalSearch))
                        {
                            await SetInternalValueAsync(default);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public override async Task HandleOnClearClickAsync(MouseEventArgs args)
        {
            if (!string.IsNullOrEmpty(InternalSearch))
            {
                InternalSearch = null;
            }

            await base.HandleOnClearClickAsync(args);
        }
    }
}