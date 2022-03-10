#nullable enable

namespace Masa.Blazor.Presets;

public partial class PCopyableText
{
    [Inject] public IJSRuntime Js { get; set; } = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public string? Class { get; set; }

    [Parameter] public string? ContentClass { get; set; }

    [Parameter] public string? ContentStyle { get; set; }

    [Parameter] public string? CopiedIcon { get; set; }

    [Parameter] public string? CopyIcon { get; set; }

    [Parameter] public string? CopyIconColor { get; set; } = "primary";

    [Parameter] public bool DisableTooltip { get; set; }

    [Parameter] public EventCallback OnCopy { get; set; }

    [Parameter] public string? Style { get; set; }

    [Parameter] public string? Text { get; set; }

    [Parameter] public string? Tooltip { get; set; }

    [Parameter] public string? TooltipClass { get; set; }

    [Parameter] public string? TooltipStyle { get; set; }

    private bool _copying;

    protected ElementReference Ref { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        CopiedIcon ??= "mdi-check";
        CopyIcon ??= "mdi-content-copy";
        Tooltip ??= "Copy";
    }

    private string Icon => _copying ? CopiedIcon! : CopyIcon!;

    private async Task HandleOnCopy()
    {
        if (_copying) return;

        _copying = true;

        await InvokeJsCopy();

        if (OnCopy.HasDelegate)
        {
            await OnCopy.InvokeAsync();
        }

        await Task.Delay(1000);

        _copying = false;
    }

    private async Task InvokeJsCopy()
    {
        if (string.IsNullOrEmpty(Text))
        {
            await Js.InvokeVoidAsync(JsInteropConstants.CopyChild, Ref);
        }
        else
        {
            await Js.InvokeVoidAsync(JsInteropConstants.CopyText, Text);
        }
    }
}