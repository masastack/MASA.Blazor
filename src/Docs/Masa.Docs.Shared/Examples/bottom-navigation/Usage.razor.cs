namespace Masa.Docs.Shared.Examples.bottom_navigation
{
    public class Usage : Masa.Docs.Shared.Components.Usage
    {
        protected override RenderFragment GenChildContent() => builder =>
        {
            builder.OpenComponent<UsageTemplate>(0);
            builder.CloseComponent();
        };

        public Usage() : base(typeof(MBottomNavigation)) { }
    }
}
