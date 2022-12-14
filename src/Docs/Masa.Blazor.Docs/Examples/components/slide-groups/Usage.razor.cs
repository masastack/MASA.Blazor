namespace Masa.Blazor.Docs.Examples.components.slide_groups
{
    public class Usage : Masa.Blazor.Docs.Components.Usage
    {
        public Usage() : base(typeof(MSlideGroup)) { }

        protected override RenderFragment GenChildContent() => builder =>
        {
            builder.OpenComponent<Index>(0);
            builder.CloseComponent();
        };
    }
}
