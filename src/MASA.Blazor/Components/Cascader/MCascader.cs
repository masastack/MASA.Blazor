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
                .Merge<BMenu, MCascaderMenu>(props =>
                {
                    props[nameof(MCascaderMenu.OffsetY)] = true;
                    props[nameof(MCascaderMenu.MinWidth)] = (StringNumber)(Dense ? 120 : 180);
                    props[nameof(MCascaderMenu.CloseOnContentClick)] = false;
                    props[nameof(MCascaderMenu.ContentStyle)] = "display:flex";
                })
                .Apply<BList, MList>()
                .Apply<BItemGroup, MListItemGroup>(props =>
                {
                    props[nameof(MListItemGroup.Color)] = "primary";
                })
                .Merge(typeof(BSelectList<,,>), typeof(MCascaderSelectList<TItem, TValue>))
                .Merge(typeof(BSelectMenu<,,,>), typeof(BCascaderMenu<TItem, TValue, MCascader<TItem, TValue>>))
                .Apply(typeof(BCascaderMenuBody<,,>), typeof(BCascaderMenuBody<TItem, TValue, MCascader<TItem, TValue>>));
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
