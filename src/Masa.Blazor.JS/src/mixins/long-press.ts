import { getDom } from "utils/helper";

// 存储长按事件监听器的配置
const longPressEventConfigs: Map<
  HTMLElement,
  {
    mousedownHandler: (e: MouseEvent) => void;
    touchstartHandler: (e: TouchEvent) => void;
  }
> = new Map();

export function register(s: HTMLElement | string, dotnet: DotNet.DotNetObject) {
  const container = getDom(s);
  if (!container) {
    return null;
  }

  // 如果已经注册过，先注销
  if (longPressEventConfigs.has(container)) {
    unregisterLongPressEvent(container);
  }

  // 使用事件委托来处理长按事件
  const createLongPressHandler = (eventType: "mouse" | "touch") => {
    return function (e: MouseEvent | TouchEvent) {
      // 阻止默认行为，特别是触摸事件的默认行为
      if (eventType === "touch") {
        e.preventDefault();
      }

      const element = e.target as HTMLElement;
      console.log(
        `Long press started on ${eventType} event with target:`,
        element
      );

      let timer: ReturnType<typeof setTimeout>;
      const longPressDelay = 500; // 长按阈值（毫秒）
      let isPressed = true;
      let startX =
        e instanceof MouseEvent
          ? e.clientX
          : (e as TouchEvent).touches?.[0]?.clientX || 0;
      let startY =
        e instanceof MouseEvent
          ? e.clientY
          : (e as TouchEvent).touches?.[0]?.clientY || 0;

      timer = setTimeout(() => {
        if (isPressed) {
          dotnet.invokeMethodAsync("Invoke");
        }
      }, longPressDelay);

      const checkMovement = function (moveEvent: MouseEvent | TouchEvent) {
        if (!isPressed) return;

        const moveX =
          moveEvent instanceof MouseEvent
            ? moveEvent.clientX
            : (moveEvent as TouchEvent).touches?.[0]?.clientX || 0;
        const moveY =
          moveEvent instanceof MouseEvent
            ? moveEvent.clientY
            : (moveEvent as TouchEvent).touches?.[0]?.clientY || 0;

        const moveThreshold = 10; // 移动阈值（像素）
        if (
          Math.abs(moveX - startX) > moveThreshold ||
          Math.abs(moveY - startY) > moveThreshold
        ) {
          cancelPress();
        }
      };

      const cancelPress = function (event?: MouseEvent | TouchEvent) {
        isPressed = false;
        clearTimeout(timer);

        console.log(
          `Long press cancelled on ${eventType} event with target:`,
          event?.target
        );

        // 清理所有监听器
        if (eventType === "mouse") {
          document.removeEventListener("mouseup", cancelPress);
          document.removeEventListener("mousemove", checkMovement);
        } else {
          document.removeEventListener("touchend", cancelPress);
          document.removeEventListener("touchcancel", cancelPress);
          document.removeEventListener("touchmove", checkMovement);
        }
        element.removeEventListener("mouseleave", cancelPress);
      };

      // 添加监听器
      element.addEventListener("mouseleave", cancelPress, { once: true });

      if (eventType === "mouse") {
        document.addEventListener("mouseup", cancelPress, { once: true });
        document.addEventListener("mousemove", checkMovement, {
          passive: false,
        });
      } else {
        document.addEventListener("touchend", cancelPress, {
          once: true,
          passive: false,
        });
        document.addEventListener("touchcancel", cancelPress, {
          once: true,
          passive: false,
        });
        document.addEventListener("touchmove", checkMovement, {
          passive: false,
        });
      }
    };
  };

  const mousedownHandler = createLongPressHandler("mouse");
  const touchstartHandler = createLongPressHandler("touch");

  // 添加事件监听器
  container.addEventListener("mousedown", mousedownHandler, { passive: false });
  container.addEventListener("touchstart", touchstartHandler, {
    passive: false,
  });

  // 保存监听器引用
  longPressEventConfigs.set(container, {
    mousedownHandler,
    touchstartHandler,
  });

  return {
    dispose: () => {
      unregisterLongPressEvent(container);
      dotnet.dispose();
    },
  };
}

function unregisterLongPressEvent(container: HTMLElement) {
  const config = longPressEventConfigs.get(container);
  if (config) {
    // 移除事件监听器
    container.removeEventListener("mousedown", config.mousedownHandler);
    container.removeEventListener("touchstart", config.touchstartHandler);

    // 从配置中删除
    longPressEventConfigs.delete(container);
  }
}
