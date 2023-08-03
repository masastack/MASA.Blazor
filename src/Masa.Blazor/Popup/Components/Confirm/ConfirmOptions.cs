namespace Masa.Blazor.Popup;

public class ConfirmOptions
{
    public string? ActionsClass { get; set; }

    public string? ActionsStyle { get; set; }

    public string? CancelText { get; set; }

    public Action<ModalButtonProps>? CancelProps { get; set; }

    public string? Content { get; set; }

    public RenderFragment? ContentContent { get; set; }

    public string? ContentClass { get; set; }

    public string? ContentStyle { get; set; }

    public Func<PopupOkEventArgs<bool>, Task>? OnOk { get; set; }

    public string? OkText { get; set; }

    public Action<ModalButtonProps>? OkProps { get; set; }

    public string? Title { get; set; }

    public string? TitleClass { get; set; }

    public string? TitleStyle { get; set; }

    public AlertTypes Type { get; set; } = AlertTypes.None;
}
