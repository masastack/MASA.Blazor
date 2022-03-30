namespace Masa.Blazor
{
    public partial class MStepperHeader : BStepperHeader
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-stepper__header");
                });
        }
    }
}
