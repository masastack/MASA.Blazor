#nullable enable
using Masa.Blazor.Presets;

namespace Masa.Blazor.Popup.Components;

public partial class Alert : AlertingPopupComponentBase
{
    #region parameters from MSnackbar

    [Parameter] public bool? Bottom { get; set; }

    [Parameter] public bool? Centered { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public string? ContentClass { get; set; }

    [Parameter] public bool? Dark { get; set; }

    [Parameter] public StringNumber? Elevation { get; set; }

    [Parameter] public StringNumber? Height { get; set; }

    [Parameter] public bool? Left { get; set; }

    [Parameter] public bool? Light { get; set; }

    [Parameter] public bool? MultiLine { get; set; }

    [Parameter] public EventCallback OnClosed { get; set; }

    [Parameter] public bool? Outlined { get; set; }

    [Parameter] public bool? Right { get; set; }

    [Parameter] public bool? Rounded { get; set; }

    [Parameter] public bool? Shaped { get; set; }

    [Parameter] public bool? Text { get; set; }

    [Parameter] public bool? Tile { get; set; }

    [Parameter] public int? Timeout { get; set; } = 5000;

    [Parameter] public bool? Top { get; set; }

    [Parameter] public bool? Vertical { get; set; }

    [Parameter] public StringNumber? Width { get; set; }

    #endregion

    [Parameter] public Action<ModalButtonProps>? ActionProps { get; set; }

    [Parameter] public string? ActionText { get; set; }

    [Parameter, EditorRequired] public string? Content { get; set; }

    [Parameter] public Func<Task>? OnAction { get; set; }

    private AlertParameters? _defaultParameters;

    private ModalButtonProps? ComputedActionButtonProps { get; set; }

    protected string? ComputedColor
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(Color))
            {
                return Color;
            }

            return Type switch
            {
                AlertTypes.Success => "success",
                AlertTypes.Info => "info",
                AlertTypes.Warning => "warning",
                AlertTypes.Error => "error",
                _ => null
            };
        }
    }

    protected override string? ComputedIconColor
    {
        get
        {
            var isOutlined = Outlined ?? false;
            var isText = Text ?? false;

            if (isOutlined || isText)
            {
                return base.ComputedIconColor;
            }

            if (!string.IsNullOrWhiteSpace(IconColor))
            {
                return IconColor;
            }

            return null;
        }
    }

    protected override void OnParametersSet()
    {
        if (_defaultParameters is null && MApp?.AlertParameters is not null)
        {
            _defaultParameters = new AlertParameters();

            MApp.AlertParameters.Invoke(_defaultParameters);
        }

        _defaultParameters?.MapTo(this);

        base.OnParametersSet();

        ActionText ??= "Close";

        ComputedActionButtonProps = new ModalButtonProps()
        {
            Text = true
        };

        if (!string.IsNullOrEmpty(ComputedIconColor))
        {
            ComputedActionButtonProps.Color = ComputedIconColor;
        }

        ActionProps?.Invoke(ComputedActionButtonProps);
    }

    private async Task HandleOnClosed()
    {
        if (OnClosed.HasDelegate)
        {
            await OnClosed.InvokeAsync();
        }

        PopupItem?.Discard(true);
    }

    private async Task HandleOnAction()
    {
        if (OnAction is not null)
        {
            await OnAction.Invoke();
        }

        Visible = false;
        PopupItem?.Discard(true);
    }
}