﻿namespace Masa.Blazor
{
    public class MDataTableRow<TItem> : BDataTableRow<TItem>
    {
        [Parameter]
        public Func<TItem, bool>? IsSelected { get; set; }

        [Parameter]
        public Func<TItem, bool>? IsExpanded { get; set; }

        [Parameter]
        public Func<TItem, string?>? ItemClass { get; set; }

        [Parameter]
        public bool Stripe { get; set; }

        public bool IsStripe => Stripe && Index % 2 == 1;

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .AddIf("m-data-table__selected", () => IsSelected != null && IsSelected(Item))
                        .AddIf("m-data-table__expanded m-data-table__expanded__row", () => IsExpanded != null && IsExpanded(Item))
                        .AddIf("stripe", () => IsStripe)
                        .Add(() => ItemClass?.Invoke(Item) ?? "");
                })
                .Apply("cell", cssBuilder =>
                {
                    if (cssBuilder.Data is DataTableHeader header)
                    {
                        cssBuilder
                            .Add($"text-{header.Align.ToString().ToLower()}")
                            .Add(header.CellClass)
                            .AddIf("m-data-table__divider", () => header.Divider);
                    }
                });
        }
    }
}
