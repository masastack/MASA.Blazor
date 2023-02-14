namespace Masa.Blazor.Docs.Examples.components.toolbars
{
        public class Usage : Masa.Blazor.Docs.Components.Usage
    {
        protected override ParameterList<bool> GenToggleParameters() => new()
        {
            { nameof(MToolbar.Dense), false },
            { nameof(MToolbar.Flat), false },
            { nameof(MToolbar.Outlined), false },
            { nameof(MToolbar.Prominent), false },
            { nameof(MToolbar.Rounded), false },
            { nameof(MToolbar.Shaped), false },
        };

        protected override ParameterList<SliderParameter> GenSliderParameters() => new()
        {
            { nameof(MToolbar.Elevation), new SliderParameter(4, 0, 24) }
        };

        protected override ParameterList<SelectParameter> GenSelectParameters() => new()
        {
           { nameof(MToolbar.Color), new SelectParameter(new List<string>() { "red", "orange", "yellow", "green", "blue", "purple" }) },
        };

        protected override RenderFragment GenChildContent() => builder =>
        {
            builder.OpenComponent<BAppBarNavIcon>(0);
            builder.CloseComponent();
        };

        public Usage() : base(typeof(MToolbar)) { }

        protected override object? CastValue(ParameterItem<object?> parameter)
        {
            if (parameter.Value == null)
            {
                return parameter.Value;
            }

            return parameter.Key switch
            {
                nameof(MToolbar.Rounded) => (StringBoolean)(bool)parameter.Value,
                nameof(MToolbar.Elevation) => (StringNumber)(double)parameter.Value,
                _ => parameter.Value
            };
        }
    }
}
