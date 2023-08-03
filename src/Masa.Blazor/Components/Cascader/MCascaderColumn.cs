using BlazorComponent.Web;

namespace Masa.Blazor
{
    public class MCascaderColumn<TItem, TValue> : BCascaderColumn<TItem, TValue>
    {
        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public string? Color { get; set; } = "primary";

        protected override string Icon => "$next";

        protected override string GetSelectedItemSelector(int index)
        {
            return $"{ItemGroup.Ref.GetSelector()} > .m-cascader__column-item:nth-child({index + 1})";
        }

        protected override void SetComponentClass()
        {
            AbstractProvider
                .Apply(typeof(BCascaderColumn<,>), typeof(MCascaderColumn<TItem, TValue>), attrs =>
                {
                    attrs[nameof(Dense)] = Dense;
                    attrs[nameof(ItemText)] = ItemText;
                    attrs[nameof(LoadChildren)] = LoadChildren;
                    attrs[nameof(ItemChildren)] = ItemChildren;
                    attrs[nameof(OnSelect)] = OnSelect;
                })
                .Apply(typeof(BList), typeof(MList), attrs =>
                {
                    attrs[nameof(MList.Class)] = "m-cascader__column";
                    attrs[nameof(MList.Dense)] = Dense;
                })
                .Apply(typeof(BListItem), typeof(MListItem), attrs =>
                {
                    attrs[nameof(MListItem.Class)] = "m-cascader__column-item";
                    attrs[nameof(MListItem.Dense)] = Dense;
                    attrs[nameof(MListItem.ActiveClass)] = new CssBuilder().AddTextColor(Color).Class;
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
