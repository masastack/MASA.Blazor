namespace Masa.Blazor
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

        protected override string TreeCloseIcon { get; set; } = "mdi-minus";

        protected override string TreeOpenIcon { get; set; } = "mdi-plus";

        protected override void SetComponentClass()
        {
            AbstractProvider.Apply<BButton, MButton>("tree-toggle", attrs =>
                            {
                                attrs[nameof(MButton.Icon)] = true;
                                attrs[nameof(MButton.Small)] = true;
                                attrs[nameof(MButton.Class)] = "mr-1";
                                attrs[nameof(MButton.Disabled)] = Children.Count == 0;
                            })
                            .Apply<BIcon, MIcon>("tree-toggle-icon");

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .AddIf("m-data-table__selected", () => IsSelected?.Invoke(Item) is true)
                        .AddIf("m-data-table__expanded m-data-table__expanded__row", () => IsExpanded?.Invoke(Item) is true)
                        .AddIf("stripe", () => IsStripe)
                        .AddIf($"m-row__level-{Level}", () => Level > 0)
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
                })
                .Apply("row-indent", cssBuilder =>
                {
                    cssBuilder.Add("m-row__indent")
                              .AddIf($"mr-{Level * 6}", () => Level > 0);
                });
        }
    }
}
