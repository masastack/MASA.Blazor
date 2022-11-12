namespace Masa.Docs.Shared.Examples.carousels
{
    public class Usage : Masa.Docs.Shared.Components.Usage
    {
        public Usage() : base(typeof(MCarousel)) { }

        protected override RenderFragment GenChildContent() => builder =>
        {
            builder.OpenComponent<Index>(0);
            builder.CloseComponent();
        };
    }
}
