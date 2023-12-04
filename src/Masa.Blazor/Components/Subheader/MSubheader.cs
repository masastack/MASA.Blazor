namespace Masa.Blazor
{
    public partial class MSubheader : BSubheader, IThemeable
    {
        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

        [Parameter] public bool Inset { get; set; }

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
                        .Add("m-subheader")
                        .AddIf("m-subheader--inset", () => Inset)
                        .AddTheme(IsDark, IndependentTheme);
                });
        }
    }
}
