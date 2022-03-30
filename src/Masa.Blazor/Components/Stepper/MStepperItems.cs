namespace Masa.Blazor
{
    public partial class MStepperItems : BStepperItems
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-stepper__items");
                });
        }
    }
}
