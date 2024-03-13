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

        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

        private bool IndependentTheme => (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

#if NET8_0_OR_GREATER

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-item-group")
                        .AddTheme(IsDark, IndependentTheme);
                });
        }
    }
}