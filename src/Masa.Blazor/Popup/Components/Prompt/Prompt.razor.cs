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

    private MForm? _form;
    private bool _okLoading;

    private ModalButtonProps? ComputedOkButtonProps { get; set; }

    private ModalButtonProps? ComputedCancelButtonProps { get; set; }

    protected override string ComponentName => PopupComponents.PROMPT;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        OkText ??= I18n.T("$masaBlazor.ok");
        CancelText ??= I18n.T("$masaBlazor.cancel");

        ComputedOkButtonProps = GetDefaultSaveButtonProps();
        ComputedCancelButtonProps = GetDefaultCancelButtonProps();

        OkProps?.Invoke(ComputedOkButtonProps);
        CancelProps?.Invoke(ComputedCancelButtonProps);
    }

    private Task HandleOnCancel()
    {
        return ClosePopupAsync(null);
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
            await ClosePopupAsync(_promptValue.Value);
        }
    }

    private async Task HandleOnOk()
    {
        if (_form != null && _form.Validate())
        {
            await HandleOnSubmit();
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
