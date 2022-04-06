namespace Masa.Blazor
{
    public class MSimpleCheckbox : BSimpleCheckbox
    {
        [Parameter]
        public string Color { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-simple-checkbox")
                        .AddIf("m-simple-checkbox--disabled", () => Disabled);
                })
                .Apply("input", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--selection-controls__input");
                })
                .Apply("ripple", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--selection-controls__ripple")
                        .AddTextColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddTextColor(Color);
                });

            AbstractProvider
                .Apply(typeof(BIcon), typeof(MIcon), attrs =>
                {
                    attrs[nameof(MIcon.Color)] = Color;
                    attrs[nameof(MIcon.Disabled)] = Disabled;
                    attrs[nameof(MIcon.Dark)] = Dark;
                    attrs[nameof(MIcon.Light)] = Light;
                });
        }
    }
}
