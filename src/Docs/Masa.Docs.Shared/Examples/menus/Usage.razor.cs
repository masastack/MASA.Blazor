
namespace Masa.Docs.Shared.Examples.menus;

public class Usage : Masa.Docs.Shared.Components.Usage
{
    public Usage() : base(typeof(MMenu))
    {
    }

    string[] items =
    {
        "Click Me",
        "Click Me",
        "Click Me",
        "Click Me 2"
    };

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.OpenComponent<MList>(0);
        builder.AddAttribute(1, nameof(MList.ChildContent), (RenderFragment)(childBuilder =>
        {
            foreach (var item in items)
            {
                childBuilder.OpenComponent<MListItem>(0);
                childBuilder.AddAttribute(1, nameof(MListItem.ChildContent), (RenderFragment)(mliChildBuilder =>
                {
                    mliChildBuilder.OpenComponent<MListItemContent>(0);
                    mliChildBuilder.AddAttribute(1, nameof(MListItemContent.ChildContent), (RenderFragment)(mlicChildBuilder =>
                    {
                        mlicChildBuilder.OpenComponent<MListItemTitle>(0);
                        mlicChildBuilder.AddChildContent(1, $"{item}");
                        mlicChildBuilder.CloseComponent();
                    }));

                    mliChildBuilder.CloseComponent();
                }));
                childBuilder.CloseComponent();
            }
        }));
        builder.CloseComponent();
    };

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            {
                nameof(MMenu.ActivatorContent), new RenderFragment<ActivatorProps>(context => builder =>
                {
                    builder.OpenComponent<MButton>(0);
                    builder.AddAttribute(1, nameof(MButton.Color), "primary");
                    builder.AddMultipleAttributes(2, context.Attrs);
                    builder.AddChildContent(3, "DROPDOWN");
                    builder.CloseComponent();
                })
            }
        };
    }
}
