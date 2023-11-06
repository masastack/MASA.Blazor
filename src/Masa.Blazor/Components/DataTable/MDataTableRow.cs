namespace Masa.Blazor
{
    public class MDataTableRow<TItem> : BDataTableRow<TItem>
    {
        [Inject]
        private MasaBlazor MasaBlazor { get; set; } = null!;
        
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
                            .AddIf("m-data-table__divider", () => header.Divider)
                            .AddIf("m-data-table__column--fixed-right", () => header.Fixed == DataTableFixed.Right)
                            .AddIf("m-data-table__column--fixed-left", () => header.Fixed == DataTableFixed.Left)
                            .AddIf("first-fixed-column", () => header.IsFixedShadowColumn);
                    }
                },  styleBuilder =>
                {
                    if (styleBuilder.Data is DataTableHeader<TItem> header)
                    {
                        if (header.Fixed == DataTableFixed.Right)
                        {
                            var count = Headers.Count;
                            var lastIndex = Headers.LastIndexOf(header);
                            if (lastIndex > -1)
                            {
                                var widths = Headers.TakeLast(count - lastIndex - 1).Sum(u => u.Width?.ToDouble() ?? u.RealWidth);
                                styleBuilder.Add($"{(MasaBlazor.RTL ? "left" : "right")}: {widths}px");
                            }
                        }
                        else if (header.Fixed == DataTableFixed.Left)
                        {
                            var index = Headers.IndexOf(header);
                            if (index > -1)
                            {
                                var widths = Headers.Take(index).Sum(u => u.Width?.ToDouble() ?? u.RealWidth);
                                styleBuilder.Add($"{(MasaBlazor.RTL ? "right" : "left")}: {widths}px");
                            }
                        }
                    }
                });
        }
    }
}
