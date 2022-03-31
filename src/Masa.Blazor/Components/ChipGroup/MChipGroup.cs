namespace Masa.Blazor
{
    public partial class MChipGroup : MSlideGroup
    {
        public MChipGroup() : base(GroupType.ChipGroup)
        {
        }

        [Parameter]
        public bool Column { get; set; }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder.Add("m-chip-group")
                        .AddIf("m-chip-group--column ", () => Column);
                });
        }
    }
}