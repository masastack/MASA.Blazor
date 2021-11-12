using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MDataIterator<TItem> : BDataIterator<TItem>, IDataIterator<TItem>, ILoadable
    {
        [Parameter]
        public RenderFragment<(int PageStart, int PageStop, int ItemsLength)> PageTextContent { get; set; }

        [Parameter]
        public Dictionary<string, object> FooterProps { get; set; }

        [Parameter]
        public Func<TItem, string> ItemKey { get; set; }

        [Parameter]
        public bool SingleSelect { get; set; }

        [Parameter]
        public List<TItem> Expanded { get; set; }

        [Parameter]
        public bool SingleExpand { get; set; }

        [Parameter]
        public StringBoolean Loading { get; set; }

        //TODO:Internationalization
        [Parameter]
        public string NoResultsText { get; set; } = "No matching records found";

        [Parameter]
        public string NoDataText { get; set; } = "No data available";

        [Parameter]
        public string LoadingText { get; set; } = "Loading... Please wait";

        [Parameter]
        public bool HideDefaultFooter { get; set; }

        [Parameter]
        public Func<TItem, bool> SelectableKey { get; set; }

        [Parameter]
        public RenderFragment<(IEnumerable<TItem> Items, Func<TItem, bool> IsExpanded, Action<TItem, bool> Expand)> ChildContent { get; set; }

        RenderFragment IDataIterator<TItem>.ChildContent => ChildContent == null ? null : ChildContent((ComputedItems, IsExpanded, Expand));

        [Parameter]
        public RenderFragment<ItemProps<TItem>> ItemContent { get; set; }

        [Parameter]
        public RenderFragment LoadingContent { get; set; }

        [Parameter]
        public RenderFragment NoDataContent { get; set; }

        [Parameter]
        public RenderFragment NoResultsContent { get; set; }

        [Parameter]
        public RenderFragment HeaderContent { get; set; }

        [Parameter]
        public RenderFragment FooterContent { get; set; }

        [Parameter]
        public StringNumber LoaderHeight { get; set; } = 4;

        [Parameter]
        public RenderFragment ProgressContent { get; set; }

        [Parameter]
        public IEnumerable<TItem> Value
        {
            get
            {
                return GetValue<IEnumerable<TItem>>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public EventCallback<IEnumerable<TItem>> ValueChanged { get; set; }

        public bool EveryItem => SelectableItems.Any() && SelectableItems.All(item => IsSelected(item));

        public bool SomeItems => SelectableItems.Any(item => IsSelected(item));

        public IEnumerable<TItem> SelectableItems => ComputedItems.Where(item => IsSelectable(item));

        public bool IsEmpty => !Items.Any() || Pagination.ItemsLength == 0;

        protected Dictionary<string, bool> Expansion { get; } = new();

        protected Dictionary<string, bool> Selection { get; } = new();

        [Parameter]
        public string Color { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Watcher
                .Watch<IEnumerable<TItem>>(nameof(Value), val =>
                {
                    foreach (var item in val)
                    {
                        var key = ItemKey(item);
                        Selection[key] = true;
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
                .Apply<BDataFooter, MDataFooter>(props =>
                {
                    props[nameof(MDataFooter.PageTextContent)] = PageTextContent;
                    props[nameof(MDataFooter.Options)] = InternalOptions;
                    props[nameof(MDataFooter.Pagination)] = Pagination;
                    props[nameof(MDataFooter.OnOptionsUpdate)] = EventCallback.Factory.Create<Action<DataOptions>>(this, options => UpdateOptions(options));

                    if (FooterProps != null)
                    {
                        foreach (var prop in FooterProps)
                        {
                            props[prop.Key] = prop.Value;
                        }
                    }
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
            if (ValueChanged.HasDelegate)
            {
                bool IsSelected(TItem item)
                {
                    var key = ItemKey(item);
                    if (Selection.ContainsKey(key))
                    {
                        return Selection[key];
                    }

                    return false;
                }

                var selectedItems = Items.Where(IsSelected);
                ValueChanged.InvokeAsync(selectedItems);
            }
        }

        public bool IsSelectable(TItem item)
        {
            return SelectableKey?.Invoke(item) != false;
        }

        public void ToggleSelectAll(bool value)
        {
            foreach (var item in SelectableItems)
            {
                if (!IsSelectable(item)) continue;

                var key = ItemKey?.Invoke(item);
                Selection[key] = value;
            }
        }
    }
}
