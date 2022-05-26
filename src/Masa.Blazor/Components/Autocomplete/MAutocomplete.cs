using Microsoft.AspNetCore.Components.Web;
using OneOf.Types;

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
        public bool NoFilter
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

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
                return GetComputedValue(() =>
                {
                    if (!IsSearching || NoFilter || InternalSearch == null)
                    {
                        return Items;
                    }

                    return Items.Where(item => Filter(item, InternalSearch, GetText(item) ?? "")).ToList();
                }, new string[]
                {
                    nameof(IsSearching),
                    nameof(NoFilter),
                    nameof(InternalSearch)
                });
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

        protected string InternalSearch
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

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
                .Watch<string>(nameof(SearchInput), val => InternalSearch = val)
                .Watch<IList<TItem>>(nameof(FilteredItems), OnFilteredItemsChanged);
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

        public override async Task HandleOnInputAsync(ChangeEventArgs args)
        {
            if (SelectedIndex > -1)
            {
                return;
            }

            var value = args.Value?.ToString();

            if (!Multiple && string.IsNullOrEmpty(value))
            {
                await DeleteCurrentItem();
            }

            InternalSearch = value;
            
            if (OnSearchInputUpdate.HasDelegate)
            {
                await OnSearchInputUpdate.InvokeAsync(InternalSearch);
            }
        }

        private async Task DeleteCurrentItem()
        {
            var curIndex = SelectedIndex;
            var curItem = SelectedItems.ElementAtOrDefault(curIndex);
            
            // TODO: need i check the curItem is null?

            if (Disabled || Readonly || ItemDisabled(curItem))
            {
                return;
            }

            var lastIndex = SelectedItems.Count - 1;

            if (SelectedIndex == -1 && lastIndex != 0)
            {
                SelectedIndex = lastIndex;
                
                return;
            }

            var length = SelectedItems.Count;
            var nextIndex = curIndex != length - 1 ? curIndex : curIndex - 1;
            var nextItem = SelectedItems.ElementAtOrDefault(nextIndex);

            if (nextItem is null)
            {
                if (Multiple)
                {
                    IList<TItemValue> values = new List<TItemValue>();
                    await SetInternalValueAsync((TValue)values);
                }
                else
                {
                    await SetInternalValueAsync(default);
                }
            }
            else
            {
                await SelectItemsAsync(curItem);
            }

            SelectedIndex = nextIndex;
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

        private void OnFilteredItemsChanged(IList<TItem> val, IList<TItem> oldVal)
        {
            if (val == null || oldVal == null)
            {
                return;
            }
            
            if (!AutoSelectFirst)
            {
                var preSelectedItem = oldVal.ElementAtOrDefault(MenuListIndex);
                if (preSelectedItem is not null)
                {
                    SetMenuIndex(val.IndexOf(preSelectedItem));
                }
                else
                {
                    SetMenuIndex(-1);
                }
            }
            
            NextTick(() =>
            {
                if (string.IsNullOrEmpty(InternalSearch) || (val.Count != 1 && !AutoSelectFirst))
                {
                    SetMenuIndex(-1);
                    return Task.CompletedTask;
                }

                if (AutoSelectFirst && val.Count > 0)
                {
                    SetMenuIndex(0);
                    StateHasChanged();
                }
                
                return Task.CompletedTask;
            });
            
            StateHasChanged();

        }
    }
}
