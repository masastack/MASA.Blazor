namespace Masa.Docs.Shared.Examples.borders
{
    public class Usage : Masa.Docs.Shared.Components.Usage
    {
        protected override RenderFragment GenChildContent() => builder =>
        {
            builder.OpenComponent<Border>(0);
            builder.CloseComponent();
        };

        public Usage() : base(typeof(MBorder)) { }
    }
}
