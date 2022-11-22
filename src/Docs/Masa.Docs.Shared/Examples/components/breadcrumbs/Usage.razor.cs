namespace Masa.Docs.Shared.Examples.components.breadcrumbs
{
    public class Usage : Masa.Docs.Shared.Components.Usage
    {
        protected override ParameterList<bool> GenToggleParameters() => new()
        {
            { nameof(MBreadcrumbs.Large), false },
        };

        protected override ParameterList<SelectParameter> GenSelectParameters() => new()
        {
            { nameof(MBreadcrumbs.Divider), new SelectParameter(new List<string>() { "/", "'\'", ".", ";", ">", "-" }, "/") },
        };

        protected override RenderFragment GenChildContent() => builder => builder.AddComponent<UsageTemplate>();

        public Usage() : base(typeof(MBreadcrumbs)) { }

    }
}
