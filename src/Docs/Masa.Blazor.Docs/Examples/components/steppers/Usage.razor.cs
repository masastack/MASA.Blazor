namespace Masa.Blazor.Docs.Examples.components.steppers;

[JSCustomElement(IncludeNamespace = true)]
public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MStepper)) { }

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.OpenComponent<Index>(0);
        builder.CloseComponent();
    };
}