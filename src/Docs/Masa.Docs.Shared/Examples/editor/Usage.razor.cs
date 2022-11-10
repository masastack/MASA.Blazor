namespace Masa.Docs.Shared.Examples.editor
{
    public class Usage : Masa.Docs.Shared.Components.Usage
    {
        public Usage() : base(typeof(MEditor)) { }

        protected override RenderFragment GenChildContent() => builder =>
        {
            builder.OpenComponent<Basic>(0);
            builder.CloseComponent();
        };
    }
}
