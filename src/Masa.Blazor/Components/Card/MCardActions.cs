namespace Masa.Blazor
{
    public partial class MCardActions : BCardActions
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-card__actions");
                });
        }
    }
}