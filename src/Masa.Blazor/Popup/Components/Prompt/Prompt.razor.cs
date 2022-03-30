using Masa.Blazor.Presets;

#nullable enable

namespace Masa.Blazor.Popup.Components;

public partial class Prompt : PopupComponentBase
{
    [Parameter] public string? ActionsClass { get; set; }

    [Parameter] public string? ActionsStyle { get; set; }

    [Parameter] public string? CancelText { get; set; }

    [Parameter] public Action<ModalButtonProps>? CancelProps { get; set; }

    [Parameter] public string? Content { get; set; }

    [Parameter] public string? ContentClass { get; set; }

    [Parameter] public string? ContentStyle { get; set; }

    [Parameter] public Func<PopupOkEventArgs<string?>, Task>? OnOk { get; set; }

    [Parameter] public string? OkText { get; set; }

    [Parameter] public Action<ModalButtonProps>? OkProps { get; set; }

    [Parameter] public string? Placeholder { get; set; }

    [Parameter] public string? Title { get; set; }

    [Parameter] public string? TitleClass { get; set; }

    [Parameter] public string? TitleStyle { get; set; }

    private readonly PromptValue _promptValue = new();

    private MForm _form;
    private bool _okLoading;
    private PromptParameters? _defaultParameters;

    private ModalButtonProps? ComputedOkButtonProps { get; set; }

    private ModalButtonProps? ComputedCancelButtonProps { get; set; }

    protected override void OnParametersSet()
    {
        if (_defaultParameters is null && MApp?.PromptParameters is not null)
        {
            _defaultParameters = new PromptParameters();

            MApp.PromptParameters.Invoke(_defaultParameters);
        }

        _defaultParameters?.MapTo(this);

        base.OnParametersSet();

        OkText ??= "Ok";
        CancelText ??= "Cancel";

        ComputedOkButtonProps = GetDefaultSaveButtonProps();
        ComputedCancelButtonProps = GetDefaultCancelButtonProps();

        OkProps?.Invoke(ComputedOkButtonProps);
        CancelProps?.Invoke(ComputedCancelButtonProps);
    }

    private Task HandleOnCancel()
    {
        Visible = false;
        return SetResult(null);
    }

    private async Task HandleOnSubmit()
    {
        PopupOkEventArgs<string?> args = new(_promptValue.Value);

        if (OnOk != null)
        {
            _okLoading = true;
            await OnOk.Invoke(args);
            _okLoading = false;
        }

        if (args.IsCanceled is false)
        {
            Visible = false;
            await SetResult(_promptValue.Value);
        }
    }

    private async Task HandleOnOk()
    {
        if (_form.EditContext.Validate())
        {
            await HandleOnSubmit();
        }
    }

    private async Task SetResult(string? value)
    {
        if (PopupItem != null)
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