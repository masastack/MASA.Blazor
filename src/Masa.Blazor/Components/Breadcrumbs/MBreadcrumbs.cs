namespace Masa.Blazor
{
    public class MBreadcrumbs : BBreadcrumbs
    {
        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

        [Parameter] public bool Large { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            CascadingIsDark = MasaBlazor.IsSsr && MasaBlazor.Theme.Dark;
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
