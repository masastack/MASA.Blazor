using Masa.Blazor.Presets;
namespace Masa.Docs.Shared.Examples.drawers;

public class Usage : Masa.Docs.Shared.Components.Usage
{
    public Usage() : base(typeof(PDrawer))
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
            { nameof(PDrawer.Title),"Title"},
            { nameof(PDrawer.Width),(StringNumber)500},
            {
                nameof(PDrawer.ActivatorContent), new RenderFragment<ActivatorProps>(context => builder =>
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
