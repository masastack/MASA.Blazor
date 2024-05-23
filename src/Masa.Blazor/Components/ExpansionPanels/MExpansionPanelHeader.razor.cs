using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MExpansionPanelHeader : MasaComponentBase
{
    [CascadingParameter] public MExpansionPanel? ExpansionPanel { get; set; }

    [Parameter] public RenderFragment? ActionsContent { get; set; }

    [Parameter] public RenderFragment<bool>? ChildContent { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public bool DisableIconRotate { get; set; }

    [Parameter] public string? ExpandIcon { get; set; }

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

        base.OnParametersSet();
    }
    
    private Block _block = new("m-expansion-panel-header");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.Modifier("active", IsActive)
            .And("mousedown", HasMouseDown)
            // .And("disabled", IsDisabled)
            .AddBackgroundColor(Color)
            // .AddClass("m-btn--disabled", IsDisabled)
            .GenerateCssClasses();
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