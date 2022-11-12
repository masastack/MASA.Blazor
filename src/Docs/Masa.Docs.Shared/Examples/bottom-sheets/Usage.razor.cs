using Microsoft.AspNetCore.Components.Web;

namespace Masa.Docs.Shared.Examples.bottom_sheets;

public class Usage : Masa.Docs.Shared.Components.Usage
{
    public Usage() : base(typeof(MBottomSheet))
    {
    }

    private bool _sheet;

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
            childBuilder.AddAttribute(3, nameof(MButton.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, () =>
            {
                Console.WriteLine($"_sheet1:{_sheet}");
                _sheet = !_sheet;
                Console.WriteLine($"_sheet2:{_sheet}");
            }));
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
            { nameof(MBottomSheet.ValueChanged), EventCallback.Factory.Create<bool>(this, val =>
            {
                _sheet = val;
                Console.WriteLine($"val:{val},_sheet:{_sheet}");
            }) },
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
