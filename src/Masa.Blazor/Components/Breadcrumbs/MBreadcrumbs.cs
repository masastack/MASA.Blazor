namespace Masa.Blazor
{
    public class MBreadcrumbs : BBreadcrumbs
    {
        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

        [Parameter] public bool Large { get; set; }

        private bool IndependentTheme => (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

#if NET8_0_OR_GREATER
        if (MasaBlazor.IsSsr && !IndependentTheme)
        {
            CascadingIsDark = MasaBlazor.Theme.Dark;
        }
#endif
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-breadcrumbs")
                        .AddIf("m-breadcrumbs--large", () => Large)
                        .AddTheme(IsDark, IndependentTheme);
                });

            AbstractProvider
                .Apply(typeof(BBreadcrumbsItemGroup<>), typeof(BBreadcrumbsItemGroup<MBreadcrumbs>))
                .Apply<BBreadcrumbsDivider, MBreadcrumbsDivider>()
                .Apply<BBreadcrumbsItem, MBreadcrumbsItem>();
        }
    }
}
