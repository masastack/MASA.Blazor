using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MDataTableRow<TItem> : BDataTableRow<TItem>
    {
        [Parameter]
        public Func<TItem, bool> IsSelected { get; set; }

        [Parameter]
        public Func<TItem, bool> IsExpanded { get; set; }

        [Parameter]
        public string ItemClass { get; set; }

        [Parameter]
        public bool Stripe { get; set; }

        public bool IsStripe => Stripe && Index % 2 == 1;

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .AddIf("m-data-table__selected", () => IsSelected(Item))
                        .AddIf("m-data-table__expanded m-data-table__expanded__row", () => IsExpanded(Item))
                        .AddIf("stripe", () => IsStripe)
                        .Add(ItemClass);
                })
                .Apply("cell", cssBuilder =>
                {
                    if (cssBuilder.Data is DataTableHeader header)
                    {
                        cssBuilder
                            .Add($"text-{header.Align ?? "start"}")
                            .Add(header.CellClass)
                            .AddIf("m-data-table__divider", () => header.Divider);
                    }
                });
        }
    }
}
