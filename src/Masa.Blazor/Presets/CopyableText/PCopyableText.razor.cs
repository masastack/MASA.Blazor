using Element = BemIt.Element;

namespace Masa.Blazor.Presets;

public partial class PCopyableText : MasaComponentBase
{
    [Inject] public I18n I18n { get; set; } = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public string? ContentClass { get; set; }

    [Parameter] public string? ContentStyle { get; set; }

    [Parameter] public string? CopiedIcon { get; set; }

    [Parameter] public string? CopyIcon { get; set; }

    [Parameter] public string? CopyIconColor { get; set; } = "primary";

    [Parameter] public bool DisableTooltip { get; set; }

    // TODO: Change name to OnCopied?
    [Parameter] public EventCallback OnCopy { get; set; }

    [Parameter] public string? Text { get; set; }

    [Parameter] public string? Tooltip { get; set; }

    [Parameter] public string? TooltipClass { get; set; }

    [Parameter] public string? TooltipStyle { get; set; }

    [Parameter]
    [MasaApiParameter(CopyableTextVariant.AppendIcon, ReleasedOn = "v1.9.0")]
    public CopyableTextVariant Variant { get; set; }

    [Parameter]
    [MasaApiParameter(true, ReleasedOn = "v1.9.0")]
    public bool Ripple { get; set; } = true;

    private static Block _root = new("m-presets-copyable-text"); // TODO: rename to m-copyable-text in v2.0
    private static Element _icon = _root.Element("icon");
    private static Element _content = _root.Element("content");

    private ModifierBuilder _rootModifierBuilder = _root.CreateModifierBuilder();
    private ModifierBuilder _contentModifierBuilder = _content.CreateModifierBuilder();

    private bool _copying;
    private ElementReference _contentRef;

    private string Icon => _copying ? CopiedIcon! : CopyIcon!;

    private bool ComputedRipple => Variant == CopyableTextVariant.Content && !DisableTooltip && Ripple;

    public override Task SetParametersAsync(ParameterView parameters)
    {
        Tooltip = I18n.T("$masaBlazor.copy");
        CopiedIcon ??= "$success";
        CopyIcon ??= "$copy";

        return base.SetParametersAsync(parameters);
    }

    protected override IEnumerable<string?> BuildComponentClass()
    {
        yield return _rootModifierBuilder.Add(Variant).Build();
    }

    private async Task HandleOnCopy()
    {
        if (_copying) return;

        _copying = true;

        InvokeJsCopy();

        if (OnCopy.HasDelegate)
        {
            await OnCopy.InvokeAsync();
        }

        await Task.Delay(1000);

        _copying = false;
    }

    private void InvokeJsCopy()
    {
        _ = string.IsNullOrEmpty(Text)
            ? Js.InvokeVoidAsync(JsInteropConstants.CopyChild, _contentRef).ConfigureAwait(false)
            : Js.InvokeVoidAsync(JsInteropConstants.CopyText, Text).ConfigureAwait(false);
    }
}