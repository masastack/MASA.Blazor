using Masa.Blazor.Presets;
namespace Masa.Blazor.Docs.Examples.components.modals;

[JSCustomElement(IncludeNamespace = true)]
public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(PModal))
    {
    }

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.AddContent(0, "Content");
    };

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(PModal.Title),"Title"},
            { nameof(PModal.Width),(StringNumber)"500"},
            {
                nameof(PModal.ActivatorContent), new RenderFragment<ActivatorProps>(context => builder =>
                {
                    builder.OpenComponent<MButton>(0);
                    builder.AddMultipleAttributes(1, context.Attrs);
                    builder.AddChildContent(2, "Activator");
                    builder.CloseComponent();
                })
            }
        };
    }
}
