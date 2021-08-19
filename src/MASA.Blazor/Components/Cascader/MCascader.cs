using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MASA.Blazor
{
    public partial class MCascader<TItem, TValue> : MSelect<TItem, TValue>, ICascader<TItem, TValue>
    {
        [Parameter]
        public bool IsFull { get; set; }

        [Parameter]
        public Func<TItem, List<TItem>> ItemChildren { get; set; }

        protected override List<string> FormatText(TValue value)
        {
            return new List<string> { string.Join("/", GetItemByValue(Items, value, IsFull).Select(ItemText)) };
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Apply("cascader-menu-body", styleAction: styleBuilder =>
                {
                    styleBuilder
                        .Add("display: inline-flex");
                })
                .Apply("cascader-menu-body-wrap", styleAction: styleBuilder =>
                {
                    styleBuilder
                        .Add("vertical-align: top")
                        .Add("min-width: 180px")
                        .Add("background-color: white")
                        .Add("border-right: 1px solid #f0f0f0");
                });

            AbstractProvider
                .Merge<BMenu, MCascaderMenu>(props =>
                {
                    props[nameof(MCascaderMenu.OffsetY)] = true;
                    props[nameof(MCascaderMenu.MinWidth)] = (StringNumber)180;
                    props[nameof(MCascaderMenu.CloseOnContentClick)] = false;
                })
                .Apply<BList, MList>()
                .Apply<MItemGroup, MListItemGroup>(props =>
                {
                    props[nameof(MListItemGroup.Color)] = "primary";
                })
                .Merge<BSelectOption<TItem, TValue>, MCascaderSelectOption<TItem, TValue>>()
                .Merge(typeof(BSelectMenu<,,>), typeof(BCascaderMenu<TItem, TValue, MCascader<TItem, TValue>>))
                .Apply(typeof(BCascaderMenuBody<TItem, TValue, ICascader<TItem, TValue>>), typeof(BCascaderMenuBody<TItem, TValue, MCascader<TItem, TValue>>));
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
