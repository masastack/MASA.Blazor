import { parseMouseEvent } from "./EventType";
import { registerExtraDropEvent, registerExtraMouseEvent, registerExtraTouchEvent } from "./extra";

export function registerExtraEvents() {
  registerExtraMouseEvent("exmousedown", "mousedown");
  registerExtraMouseEvent("exmouseup", "mouseup");
  registerExtraMouseEvent("exclick", "click");
  registerExtraMouseEvent("exmouseleave", "mouseleave");
  registerExtraMouseEvent("exmouseenter", "mouseenter");
  registerExtraMouseEvent("exmousemove", "mousemove");
  registerExtraTouchEvent("extouchstart", "touchstart");
  registerEvent("transitionend", "transitionend");
  registerExtraDropEvent("exdrop", "drop");
  registerAuxclickEvent();
}

function registerEvent(eventType: string, eventName: string) {
  if (Blazor) {
    Blazor.registerCustomEventType(eventType, {
      browserEventName: eventName,
    });
  }
}

function registerAuxclickEvent() {
  if (Blazor) {
    Blazor.registerCustomEventType("auxclick", {
      browserEventName: "auxclick",
      createEventArgs: parseMouseEvent
    });
  }
}
