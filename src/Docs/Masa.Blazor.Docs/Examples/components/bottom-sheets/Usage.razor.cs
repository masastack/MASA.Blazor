using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor.Docs.Examples.components.bottom_sheets;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MBottomSheet))
    {
    }

    private bool _sheet;

    protected override ParameterList<bool> GenToggleParameters()
    {
        return new ParameterList<bool>()
        {
            { nameof(MBottomSheet.Inset), false }
        };
    }

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters()
    {
        return new ParameterList<CheckboxParameter>()
        {
            { nameof(MBottomSheet.HideOverlay), new CheckboxParameter() },
            { nameof(MBottomSheet.Persistent), new CheckboxParameter() },
        };
    }

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.OpenComponent<MSheet>(0);
        builder.AddAttribute(1, nameof(MSheet.Class), "text-center");
        builder.AddAttribute(2, nameof(MSheet.Height), (StringNumber)200);
        builder.AddAttribute(3, nameof(MSheet.ChildContent), (RenderFragment)(childBuilder =>
        {
            childBuilder.OpenComponent<MButton>(0);
            childBuilder.AddAttribute(1, nameof(MButton.Class), "mt-6");
            childBuilder.AddAttribute(2, nameof(MButton.Color), "error");
            childBuilder.AddAttribute(3, nameof(MButton.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, () => { _sheet = !_sheet; }));
            childBuilder.AddChildContent(4, "Close");
            childBuilder.CloseComponent();
        }));
        builder.CloseComponent();
    };

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(MBottomSheet.Value), _sheet },
            { nameof(MBottomSheet.ValueChanged), EventCallback.Factory.Create<bool>(this, val => { _sheet = val; }) },
            {
                nameof(MBottomSheet.ActivatorContent), new RenderFragment<ActivatorProps>(context => builder =>
                {
                    builder.OpenComponent<MButton>(0);
                    builder.AddAttribute(1, nameof(MButton.Color), "purple");
                    builder.AddAttribute(2, nameof(MButton.Dark), true);
                    builder.AddMultipleAttributes(3, context.Attrs);
                    builder.AddChildContent(4, "Open Playground");
                    builder.CloseComponent();
                })
            }
        };
    }
}
