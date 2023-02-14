namespace Masa.Blazor.Docs.Examples.components.app_bars
{
    public class Usage : Masa.Blazor.Docs.Components.Usage
    {
        protected override ParameterList<bool> GenToggleParameters() => new()
        {
            { nameof(MAppBar.Dense), false },
            { nameof(MAppBar.Flat), false },
            { nameof(MAppBar.Outlined), false },
            { nameof(MAppBar.Prominent), false },
            { nameof(MAppBar.Rounded), false },
            { nameof(MAppBar.Shaped), false },
        };

        protected override ParameterList<SliderParameter> GenSliderParameters() => new()
        {
            { nameof(MAppBar.Elevation), new SliderParameter(4, 0, 24) }
        };

        protected override ParameterList<SelectParameter> GenSelectParameters() => new()
        {
           { nameof(MAppBar.Color), new SelectParameter(new List<string>() { "red", "orange", "yellow", "green", "blue", "purple" }) },
        };

        protected override RenderFragment GenChildContent() => builder =>
        {
            builder.OpenComponent<BAppBarNavIcon>(0);
            builder.CloseComponent();
        };

        public Usage() : base(typeof(MAppBar)) { }

        protected override object? CastValue(ParameterItem<object?> parameter)
        {
            if (parameter.Value == null)
            {
                return parameter.Value;
            }

            return parameter.Key switch
            {
                nameof(MAppBar.Rounded) => (StringBoolean)(bool)parameter.Value,
                nameof(MAppBar.Elevation) => (StringNumber)(double)parameter.Value,
                _ => parameter.Value
            };
        }
    }
}
