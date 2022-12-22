namespace Masa.Blazor.Docs.Examples.components.badges
{
    public class Usage : Masa.Blazor.Docs.Components.Usage
    {
        protected override ParameterList<bool> GenToggleParameters() => new()
        {
            { nameof(MBadge.Bordered), false },
            { nameof(MBadge.Inline), false },
            { nameof(MBadge.Tile), false },
        };

        protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
        {
            { nameof(MBadge.Bottom), new CheckboxParameter("false", true) },
            { nameof(MBadge.Dot), new CheckboxParameter("false", true) },
            { nameof(MBadge.Left), new CheckboxParameter("false", true) },
            { nameof(MBadge.OverLap), new CheckboxParameter("false", true) },
        };

        protected override RenderFragment GenChildContent() => builder =>
        {
            builder.OpenComponent<MIcon>(0);
            builder.AddAttribute(1, nameof(MIcon.Size), (StringNumber)36);
            builder.AddAttribute(2, "ChildContent", (RenderFragment)(b => b.AddContent(0, "mdi-heart")));
            builder.CloseComponent();
        };

        public Usage() : base(typeof(MBadge)) { }
    }
}
