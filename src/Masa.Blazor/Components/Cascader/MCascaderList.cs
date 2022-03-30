namespace Masa.Blazor
{
    public class MCascaderList<TItem, TValue> : BCascaderList<TItem, TValue>
    {
        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public string Color { get; set; } = "primary";

        protected override string Icon => "mdi-chevron-right";

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-cascader-list");
                })
                .Apply("wrapper", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-cascader-list__wrapper")
                        .AddIf("m-cascader-list__wrapper--dense", () => Dense);
                });

            AbstractProvider
                .Apply(typeof(BCascaderList<,>), typeof(MCascaderList<TItem, TValue>), attrs =>
                 {
                     attrs[nameof(Dense)] = Dense;
                     attrs[nameof(ItemText)] = ItemText;
                     attrs[nameof(LoadChildren)] = LoadChildren;
                     attrs[nameof(ItemChildren)] = ItemChildren;
                     attrs[nameof(OnSelect)] = OnSelect;
                 })
                .Apply(typeof(BList), typeof(MList), attrs =>
                {
                    attrs[nameof(MList.Dense)] = Dense;
                })
                .Apply(typeof(BListItem), typeof(MListItem), attrs =>
                {
                    attrs[nameof(MListItem.Dense)] = Dense;
                    attrs[nameof(MListItem.ActiveClass)] = new CssBuilder().AddTextColor(Color).Class; ;
                    if (attrs.Data is TItem item)
                    {
                        attrs[nameof(MListItem.IsActive)] = EqualityComparer<TItem>.Default.Equals(SelectedItem, item);
                    }
                })
                .Apply(typeof(BProgressCircular), typeof(MProgressCircular), attrs =>
                {
                    attrs[nameof(MProgressCircular.Indeterminate)] = true;
                    attrs[nameof(MProgressCircular.Size)] = (StringNumber)20;
                    attrs[nameof(MProgressCircular.Width)] = (StringNumber)2;
                });
        }
    }
}
