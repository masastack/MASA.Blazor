namespace Masa.Blazor.Docs.Examples.components.avatars
{
    public class Usage : Masa.Blazor.Docs.Components.Usage
    {
        protected override ParameterList<bool> GenToggleParameters() => new()
        {
            { nameof(MAvatar.Rounded), false },
            { nameof(MAvatar.Tile), false },
        };

        protected override ParameterList<SliderParameter> GenSliderParameters() => new()
        {
            { nameof(MAvatar.Size), new SliderParameter(56, 25, 128) }
        };

        protected override ParameterList<SelectParameter> GenSelectParameters() => new()
        {
            { nameof(MAvatar.Color), new SelectParameter(new List<string>() { "primary", "accent", "warning lighten-2", "teal", "grey lighten-2" },"primary") },
        };

        protected override RenderFragment GenChildContent() => builder =>
        {
            builder.OpenComponent<MIcon>(0);
            builder.AddAttribute(1, nameof(MIcon.Dark), true);
            builder.AddAttribute(2, "ChildContent", (RenderFragment)(b => b.AddContent(0, "mdi-heart")));
            builder.CloseComponent();
        };

        public Usage() : base(typeof(MAvatar))
        {
        }

        protected override object? CastValue(ParameterItem<object?> parameter)
        {
            if (parameter.Value == null)
            {
                return parameter.Value;
            }

            return parameter.Key switch
            {
                nameof(MAvatar.Size) => (StringNumber)(double)parameter.Value,
                nameof(MAvatar.Rounded) => (StringBoolean)(bool)parameter.Value,
                _ => parameter.Value
            };
        }
    }
}
