namespace Masa.Docs.Shared.Examples.styles_and_animations.border_radius
{
    public class Usage : Masa.Docs.Shared.Components.Usage
    {
        protected override RenderFragment GenChildContent() => builder =>
        {
            builder.OpenComponent<Basic>(0);
            builder.CloseComponent();
        };

        public Usage() : base(typeof(MBorder)) { }
    }
}
