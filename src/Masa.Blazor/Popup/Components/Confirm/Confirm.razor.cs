#nullable enable
using Masa.Blazor.Presets;

namespace Masa.Blazor.Popup.Components;

public partial class Confirm : AlertingPopupComponentBase
{
    [Parameter] public string? ActionsClass { get; set; }

    [Parameter] public string? ActionsStyle { get; set; }

    [Parameter] public string? CancelText { get; set; }

    [Parameter] public Action<ModalButtonProps>? CancelProps { get; set; }

    [Parameter] public string? Content { get; set; }

    [Parameter] public string? ContentClass { get; set; }

    [Parameter] public string? ContentStyle { get; set; }

    [Parameter] public Func<PopupOkEventArgs<bool>, Task>? OnOk { get; set; }

    [Parameter] public string? OkText { get; set; }

    [Parameter] public Action<ModalButtonProps>? OkProps { get; set; }

    [Parameter] public string? Title { get; set; }

    [Parameter] public string? TitleClass { get; set; }

    [Parameter] public string? TitleStyle { get; set; }

    private bool _okLoading;
    private ConfirmParameters? _defaultParameters;

    private ModalButtonProps? ComputedOkButtonProps { get; set; }

    private ModalButtonProps? ComputedCancelButtonProps { get; set; }

    protected override void OnParametersSet()
    {
        if (_defaultParameters is null && MApp?.ConfirmParameters is not null)
        {
            _defaultParameters = new ConfirmParameters();

            MApp.ConfirmParameters.Invoke(_defaultParameters);
        }

        _defaultParameters?.MapTo(this);

        base.OnParametersSet();

        OkText ??= "Ok";
        CancelText ??= "Cancel";

        ComputedOkButtonProps = GetDefaultSaveButtonProps();
        ComputedCancelButtonProps = GetDefaultCancelButtonProps();

        if (!string.IsNullOrEmpty(ComputedIconColor))
        {
            ComputedOkButtonProps.Color = ComputedIconColor;
        }

        OkProps?.Invoke(ComputedOkButtonProps);
        CancelProps?.Invoke(ComputedCancelButtonProps);
    }

    private Task HandleOnCancel()
    {
        Visible = false;
        return SetResult(false);
    }

    private async Task HandleOnOk()
    {
        PopupOkEventArgs<bool> args = new(true);

        if (OnOk != null)
        {
            _okLoading = true;
            await OnOk.Invoke(args);
            _okLoading = false;
        }

        if (args.IsCanceled is false)
        {
            Visible = false;
            await SetResult(true);
        }
    }

    private async Task SetResult(bool value)
    {
        if (PopupItem is not null)
        {
            await Task.Delay(256);
            PopupItem.Discard(value);
        }
    }

    protected virtual ModalButtonProps GetDefaultSaveButtonProps() => new()
    {
        Color = "primary",
        Text = true,
    };

    protected virtual ModalButtonProps GetDefaultCancelButtonProps() => new()
    {
        Text = true
    };
}