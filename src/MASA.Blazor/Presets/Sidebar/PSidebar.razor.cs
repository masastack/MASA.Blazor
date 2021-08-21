using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Presets
{
    public partial class PSidebar<TItem>
    {
        private List<SidebarItem<TItem>> _sidebarItems = new();

        private List<TItem> _items = new();
        private bool _itemsChanged;

        [Parameter]
        public List<TItem> Items
        {
            get => _items;
            set
            {
                _items = value ?? new List<TItem>();
                _itemsChanged = true;
            }
        }

        private StringNumber _value;
        [Parameter]
        public StringNumber Value
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = value;
                ValueChanged.InvokeAsync(_value);
            }
        }

        private StringNumber _listItemGroupValue;
        private void ListItemGroupValueChanged(StringNumber v)
        {
            _listItemGroupValue = v;
            Value = v;
        }

        [Parameter]
        public EventCallback<StringNumber> ValueChanged { get; set; }

        [Parameter]
        public Func<TItem, StringNumber> Key { get; set; }

        [Parameter]
        public Func<TItem, string> Title { get; set; }

        [Parameter]
        public Func<TItem, string> Icon { get; set; }

        [Parameter]
        public Func<TItem, List<TItem>> Children { get; set; }

        [Parameter]
        public EventCallback<TItem> Click { get; set; }

        [Parameter]
        public string Color { get; set; } = "primary";

        #region List parameters

        [Parameter] public bool Outlined { get; set; }

        [Parameter] public bool Shaped { get; set; }

        [Parameter] public bool Dense { get; set; }

        [Parameter] public bool Flat { get; set; }

        [Parameter] public bool Disabled { get; set; }

        [Parameter] public bool Nav { get; set; }

        [Parameter] public StringNumber Height { get; set; }

        [Parameter] public StringNumber MinHeight { get; set; }

        [Parameter] public StringNumber MinWidth { get; set; }

        [Parameter] public StringNumber MaxHeight { get; set; }

        [Parameter] public StringNumber MaxWidth { get; set; }

        [Parameter] public StringNumber Width { get; set; }

        #endregion

        protected override void OnInitialized()
        {
            _listItemGroupValue = Value;
        }

        protected override void OnParametersSet()
        {
            if (_sidebarItems.Count == 0 || _itemsChanged)
            {
                _sidebarItems = ConvertToSidebarItems(Items, Value);
                _itemsChanged = false;
            }
        }

        private List<SidebarItem<TItem>> ConvertToSidebarItems(List<TItem> items, StringNumber activeValue)
        {
            var sidebarItems = new List<SidebarItem<TItem>>();

            foreach (var item in items)
            {
                var title = Title?.Invoke(item);
                var icon = Icon?.Invoke(item);
                var value = Key?.Invoke(item);

                var sidebarItem = new SidebarItem<TItem>
                {
                    Title = title,
                    Icon = icon,
                    Key = value,
                    Expanded = value == activeValue,
                    Data = item
                };

                var children = Children?.Invoke(item);
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
