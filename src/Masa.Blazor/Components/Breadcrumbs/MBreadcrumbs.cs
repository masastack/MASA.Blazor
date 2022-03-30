namespace Masa.Blazor
{
    public class MBreadcrumbs : BBreadcrumbs
    {
        [Parameter]
        public bool Large { get; set; }



        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-breadcrumbs")
                        .AddIf("m-breadcrumbs--large", () => Large)
                        .AddTheme(IsDark);
                });

            AbstractProvider
                .Apply(typeof(BBreadcrumbsItemGroup<>), typeof(BBreadcrumbsItemGroup<MBreadcrumbs>))
                .Apply<BBreadcrumbsDivider, MBreadcrumbsDivider>()
                .Apply<BBreadcrumbsItem, MBreadcrumbsItem>();
        }
    }
}