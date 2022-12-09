using Microsoft.AspNetCore.Components.Web;

namespace Masa.Docs.Shared.Examples.dialogs;

public class Usage : Masa.Docs.Shared.Components.Usage
{
    public Usage() : base(typeof(MDialog))
    {
    }

    private bool _dialog;

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.OpenComponent<MCard>(0);
        builder.AddAttribute(1, nameof(MCard.ChildContent), (RenderFragment)(childBuilder =>
        {
            childBuilder.OpenComponent<MCardTitle>(0);
            childBuilder.AddAttribute(1, nameof(MCardTitle.Class), "text-h5 grey lighten-2");
            childBuilder.AddChildContent(2, "Privacy Policy");
            childBuilder.CloseComponent();

            childBuilder.OpenComponent<MCardText>(3);
            childBuilder.AddChildContent(4, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
            childBuilder.CloseComponent();

            childBuilder.OpenComponent<MDivider>(5);
            childBuilder.CloseComponent();

            childBuilder.OpenComponent<MCardActions>(6);
            childBuilder.AddAttribute(7, nameof(MCardActions.ChildContent), (RenderFragment)(childBuilder =>
            {
                childBuilder.OpenComponent<MSpacer>(0);
                childBuilder.CloseComponent();

                childBuilder.OpenComponent<MButton>(1);
                childBuilder.AddAttribute(2, nameof(MButton.Color), "primary");
                childBuilder.AddAttribute(3, nameof(MButton.Text), true);
                childBuilder.AddChildContent(4, "I accept");
                childBuilder.AddAttribute(5, nameof(MButton.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, () => _dialog = !_dialog));
                childBuilder.CloseComponent();
            }));

            childBuilder.CloseComponent();
        }));

        builder.CloseComponent();
    };

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(MDialog.Value), _dialog },
            { nameof(MDialog.Width), (StringNumber)500 },
            { nameof(MDialog.ValueChanged), EventCallback.Factory.Create<bool>(this, val => _dialog = val) },
            {
                nameof(MDialog.ActivatorContent), new RenderFragment<ActivatorProps>(context => builder =>
                {
                    builder.OpenComponent<MButton>(0);
                    builder.AddAttribute(1, nameof(MButton.Color), "red lighten-2");
                    builder.AddAttribute(2, nameof(MButton.Dark), true);
                    builder.AddAttribute(3, nameof(MButton.Attributes), context.Attrs);
                    builder.AddChildContent(4, "Click Me");
                    builder.CloseComponent();
                })
            }
        };
    }
}
