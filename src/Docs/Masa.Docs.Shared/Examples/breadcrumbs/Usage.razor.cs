namespace Masa.Docs.Shared.Examples.breadcrumbs
{
    public class Usage : Masa.Docs.Shared.Components.Usage
    {
        protected override ParameterList<bool> GenToggleParameters() => new()
        {
            { nameof(MBreadcrumbs.Large), false },
        };

        protected override ParameterList<SelectParameter> GenSelectParameters() => new()
        {
            { nameof(MBreadcrumbs.Divider), new SelectParameter(new List<string>() { "/", "'\'",".",";",">","-"}, "/") },
        };

        protected override RenderFragment GenChildContent() => builder =>
        {
            builder.OpenComponent<UsageTemplate>(0);
            builder.CloseComponent();
        };

        public Usage() : base(typeof(MBreadcrumbs)) { }

    }
}
