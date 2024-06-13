namespace Microsoft.AspNetCore.Components.Web;

public class MouseEventWithRefArgs : MouseEventArgs
{
    public ElementReference ElementReference { get; init; }

    public MouseEventWithRefArgs(MouseEventArgs eventArgs, ElementReference elementReference)
    {
        Detail = eventArgs.Detail;
        ScreenX = eventArgs.ScreenX;
        ScreenY = eventArgs.ScreenY;
        ClientX = eventArgs.ClientX;
        ClientY = eventArgs.ClientY;
        OffsetX = eventArgs.OffsetX;
        OffsetY = eventArgs.OffsetY;
        Button = eventArgs.Button;
        Buttons = eventArgs.Buttons;
        CtrlKey = eventArgs.CtrlKey;
        ShiftKey = eventArgs.ShiftKey;
        AltKey = eventArgs.AltKey;
        MetaKey = eventArgs.MetaKey;
        Type = eventArgs.Type;
        ElementReference = elementReference;
    }
}
