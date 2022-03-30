namespace Masa.Blazor
{
    public partial class MItemGroup : BItemGroup, IThemeable
    {
        public MItemGroup() : base(GroupType.ItemGroup)
        {
        }

        public MItemGroup(GroupType groupType) : base(groupType)
        {
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-item-group")
                        .AddTheme(IsDark);
                });
        }
    }
}