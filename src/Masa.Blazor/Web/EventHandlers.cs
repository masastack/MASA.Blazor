namespace BlazorComponent;

#if !NET7_0_OR_GREATER
[EventHandler("onmouseleave", typeof(MouseEventArgs), true, true)]
[EventHandler("onmouseenter", typeof(MouseEventArgs), true, true)]
#endif
[EventHandler("onexmousedown", typeof(ExMouseEventArgs), true, true)]
[EventHandler("onexmouseup", typeof(ExMouseEventArgs), true, true)]
[EventHandler("onexmousemove", typeof(ExMouseEventArgs), true, true)]
[EventHandler("onexclick", typeof(ExMouseEventArgs), true, true)]
[EventHandler("onexmouseleave", typeof(ExMouseEventArgs), true, true)]
[EventHandler("onexmouseenter", typeof(ExMouseEventArgs), true, true)]
[EventHandler("onextouchstart", typeof(ExTouchEventArgs), true, true)]
[EventHandler("onpastewithdata", typeof(PasteWithDataEventArgs), true, true)]
[EventHandler("ontransitionend", typeof(TransitionEventArgs), true, true)]
[EventHandler("onauxclick", typeof(MouseEventArgs), true, true)]
public static class EventHandlers
{
}
