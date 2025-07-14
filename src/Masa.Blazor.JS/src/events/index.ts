import { parseMouseEvent } from "./EventType";
import {
  registerExtraDropEvent,
  registerExtraMouseEvent,
  registerExtraTouchEvent,
} from "./extra";

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
  registerLongPressEvent();
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
      createEventArgs: parseMouseEvent,
    });
  }
}

function registerLongPressEvent() {
  if (Blazor) {
    Blazor.registerCustomEventType("longpress", {
      createEventArgs: parseMouseEvent,
    });

    // 使用事件委托来处理长按事件
    const longPressHandler = function (e: MouseEvent | TouchEvent | Event) {
      const element = e.target as HTMLElement;

      // 如果是右键菜单事件，直接阻止并返回，不执行长按逻辑
      if (e.type === 'contextmenu') {
        e.preventDefault();
        e.stopPropagation();
        return;
      }

      let timer: ReturnType<typeof setTimeout>;
      const longPressDelay = 500; // 长按阈值（毫秒）
      let isPressed = true;
      let startX = e instanceof MouseEvent ? e.clientX :
                   (e as TouchEvent).touches?.[0]?.clientX || 0;
      let startY = e instanceof MouseEvent ? e.clientY :
                   (e as TouchEvent).touches?.[0]?.clientY || 0;

      timer = setTimeout(() => {
        if (isPressed) {
          const longPressEvent = new CustomEvent("longpress", {
            bubbles: true,
            cancelable: true,
            detail: { originalEvent: e },
          });

          // 派发自定义事件
          element.dispatchEvent(longPressEvent);
        }
      }, longPressDelay);

      const checkMovement = function (moveEvent: MouseEvent | TouchEvent) {
        if (!isPressed) return;

        const moveX = moveEvent instanceof MouseEvent ? moveEvent.clientX :
                      (moveEvent as TouchEvent).touches?.[0]?.clientX || 0;
        const moveY = moveEvent instanceof MouseEvent ? moveEvent.clientY :
                      (moveEvent as TouchEvent).touches?.[0]?.clientY || 0;

        const moveThreshold = 10; // 移动阈值（像素）
        if (
          Math.abs(moveX - startX) > moveThreshold ||
          Math.abs(moveY - startY) > moveThreshold
        ) {
          cancelPress();
        }
      };

      const cancelPress = function () {
        isPressed = false;
        clearTimeout(timer);

        // 清理所有监听器
        element.removeEventListener("mouseup", cancelPress);
        element.removeEventListener("mouseleave", cancelPress);
        element.removeEventListener("touchend", cancelPress);
        element.removeEventListener("touchcancel", cancelPress);
        element.removeEventListener("mousemove", checkMovement);
        element.removeEventListener("touchmove", checkMovement);
        element.removeEventListener("contextmenu", preventContextMenu);
      };

      // 阻止上下文菜单的函数
      const preventContextMenu = function (e: Event) {
        e.preventDefault();
        e.stopPropagation();
        return false;
      };

      // 添加监听器到目标元素，全部设置 passive: false 避免警告
      element.addEventListener("contextmenu", preventContextMenu, { passive: false });
      element.addEventListener("mouseup", cancelPress, { once: true });
      element.addEventListener("mouseleave", cancelPress, { once: true });
      element.addEventListener("touchend", cancelPress, { once: true, passive: false });
      element.addEventListener("touchcancel", cancelPress, { once: true, passive: false });
      element.addEventListener("mousemove", checkMovement, { passive: false });
      element.addEventListener("touchmove", checkMovement, { passive: false });
    };

    // 在 document 级别监听所有的 mousedown 和 touchstart 事件
    document.addEventListener("mousedown", longPressHandler, { passive: false });
    document.addEventListener("touchstart", longPressHandler, { passive: false });
    // 全局阻止右键菜单
    document.addEventListener("contextmenu", longPressHandler, { passive: false });
  }
}
