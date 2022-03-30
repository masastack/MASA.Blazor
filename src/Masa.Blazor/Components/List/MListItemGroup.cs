namespace Masa.Blazor
{
    public partial class MListItemGroup : MItemGroup
    {
        public MListItemGroup() : base(GroupType.ListItemGroup)
        {
        }

        [Parameter]
        public string Color { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-list-item-group")
                        .AddTextColor(Color);
                });
        }
    }
}