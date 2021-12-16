using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MASA.Blazor
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

        public TItem LoadingItem { get; private set; }

        public Dictionary<int, List<TItem>> ChildrenItems { get; } = new Dictionary<int, List<TItem>>();

        protected override List<string> FormatText(TValue value)
        {
            return new List<string> { string.Join(" / ", GetItemByValue(Items, value, ShowAllLevels).Select(ItemText)) };
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-cascader");
                })
                .Apply("menu-body", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-cascader__menu-body");
                })
                .Apply("menu-body-wrapper", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-cascader__menu-body-wrapper")
                        .AddIf("m-cascader__menu-body-wrapper--dense", () => Dense);
                });

            AbstractProvider
                .Merge<BMenu, MCascaderMenu>(attrs =>
                {
                    attrs[nameof(MCascaderMenu.OffsetY)] = true;
                    attrs[nameof(MCascaderMenu.MinWidth)] = (StringNumber)(Dense ? 120 : 180);
                    attrs[nameof(MCascaderMenu.CloseOnContentClick)] = false;
                    attrs[nameof(MCascaderMenu.ContentStyle)] = "display:flex";
                })
                .Apply<BList, MList>()
                .Apply<BItemGroup, MListItemGroup>(attrs =>
                {
                    attrs[nameof(MListItemGroup.Color)] = "primary";
                })
                .Merge(typeof(BSelectList<,,>), typeof(MCascaderSelectList<TItem, TValue>))
                .Merge(typeof(BSelectMenu<,,,>), typeof(BCascaderMenu<TItem, TValue, MCascader<TItem, TValue>>))
                .Apply(typeof(BCascaderMenuBody<,,>), typeof(BCascaderMenuBody<TItem, TValue, MCascader<TItem, TValue>>));
        }

        public async Task HandleOnItemClickAsync(TItem item, int level)
        {
            var children = ItemChildren(item);

            if (LoadChildren != null && children != null && children.Count == 0)
            {
                LoadingItem = item;
                await LoadChildren(item);
                LoadingItem = default;

                children = ItemChildren(item);
            }

            if (children != null && children.Count > 0)
            {
                ChildrenItems[level] = children;
            }
            else
            {
                ChildrenItems.Remove(level);
            }
        }

        private List<TItem> GetItemByValue(IEnumerable<TItem> items, TValue value, bool isFull)
        {
            var results = new List<TItem>();

            foreach (var item in items)
            {
                if (results.Any()) break;

                if (ItemValue(item).Equals(value))
                {
                    results.Add(item);
                    break;
                }
                else
                {
                    var children = ItemChildren(item);
                    if (children != null && children.Any())
                    {
                        var result = GetItemByValue(children, value, isFull);
                        if (result.Any())
                        {
                            if (isFull)
                            {
                                results.Add(item);
                            }

                            results.AddRange(result);
                        }
                    }
                }
            }

            return results;
        }
    }
}
