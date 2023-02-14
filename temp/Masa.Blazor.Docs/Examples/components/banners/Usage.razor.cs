namespace Masa.Blazor.Docs.Examples.components.banners
{
    public class Usage : Masa.Blazor.Docs.Components.Usage
    {
        protected override ParameterList<bool> GenToggleParameters() => new()
        {
            { nameof(MBanner.SingleLine), false },
            { nameof(MBanner.Sticky), false },
        };

        protected override ParameterList<SliderParameter> GenSliderParameters() => new()
        {
            { nameof(MBanner.Elevation), new SliderParameter(0, 0, 24) }
        };

        protected override ParameterList<SelectParameter> GenSelectParameters() => new()
        {
            { nameof(MBanner.Color), new SelectParameter(new List<string>() { "accent", "primary","secondary"}) },
            { nameof(MBanner.Icon), new SelectParameter(new List<string>() { "mdi-account", "mdi-heart" }) },
        };

        protected override RenderFragment GenChildContent() => builder =>
        {
            builder.OpenElement(0, "span");
            builder.AddContent(1, "A banner for use on desktop / mobile");
            builder.CloseComponent();
        };

        public Usage() : base(typeof(MBanner)) { }

        protected override object? CastValue(ParameterItem<object?> parameter)
        {
            if (parameter.Value == null)
            {
                return parameter.Value;
            }

            return parameter.Key switch
            {
                nameof(MBanner.Elevation) => (StringNumber)(double)parameter.Value,
                _ => parameter.Value
            };
        }
    }
}
