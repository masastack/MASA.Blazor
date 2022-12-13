namespace Masa.Blazor.Docs.Examples.components.drag_zone
{
    public class Usage : Masa.Blazor.Docs.Components.Usage
    {
        public Usage() : base(typeof(MDragZone)) { }

        protected override RenderFragment GenChildContent() => builder =>
        {
            builder.OpenComponent<Index>(0);
            builder.CloseComponent();
        };
    }
}
