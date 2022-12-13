namespace Masa.Blazor.Docs.Examples.components.simple_tables
{
    public class Usage : Masa.Blazor.Docs.Components.Usage
    {
        protected override RenderFragment GenChildContent() => builder =>
        {
            builder.OpenComponent<Index>(0);
            builder.CloseComponent();
        };

        public Usage() : base(typeof(MSimpleTable)) { }
    }
}
