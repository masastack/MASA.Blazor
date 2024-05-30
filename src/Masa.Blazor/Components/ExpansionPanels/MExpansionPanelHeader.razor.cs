using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MExpansionPanelHeader : MasaComponentBase
{
    [CascadingParameter] public MExpansionPanel? ExpansionPanel { get; set; }

    [Parameter] public RenderFragment? ActionsContent { get; set; }

    [Parameter] public RenderFragment<bool>? ChildContent { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public bool DisableIconRotate { get; set; }

    [Parameter]
    [MasaApiParameter("$expand")]
    public string? ExpandIcon { get; set; } = "$expand";

    [Parameter] public bool HideActions { get; set; }

    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

    [Parameter] public bool Ripple { get; set; }

    protected bool HasMouseDown;

    protected bool IsActive => ExpansionPanel?.InternalIsActive ?? false;

    protected bool IsDisabled => ExpansionPanel?.IsDisabled ?? false;

    protected bool IsReadonly => ExpansionPanel?.IsReadonly ?? false;

    protected override void OnParametersSet()
    {
        Attributes["ripple"] = Ripple;
        ExpandIcon ??= "$expand";

        base.OnParametersSet();
    }

    private static Block _block = new("m-expansion-panel-header");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private ModifierBuilder _iconModifierBuilder = _block.Element("icon").CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder.Add("active", IsActive)
            .Add("mousedown", HasMouseDown)
            .Add("disabled", IsDisabled)
            .AddBackgroundColor(Color)
            .Build();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return StyleBuilder.Create().AddBackgroundColor(Color).GenerateCssStyles();
    }

    protected virtual async Task HandleClickAsync(MouseEventArgs args)
    {
        _ = Js.InvokeVoidAsync(JsInteropConstants.Blur, Ref);

        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(args);
        }

        if (!(IsReadonly || IsDisabled))
        {
            if (ExpansionPanel is null) return;

            await ExpansionPanel.Toggle();
        }
    }
}