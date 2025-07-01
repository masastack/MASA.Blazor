export interface TouchData {
  touchstartX: number;
  touchstartY: number;
  touchmoveX: number;
  touchmoveY: number;
  touchendX: number;
  touchendY: number;
  offsetX: number;
  offsetY: number;
}

export type TouchWrapper = TouchData & { dotnetHelper: DotNet.DotNetObject };

export interface TouchValue {
  parent?: boolean;
  options?: {
    touchstart?: AddEventListenerOptions & { stopPropagation?: boolean };
    touchmove?: AddEventListenerOptions & { stopPropagation?: boolean };
    touchend?: AddEventListenerOptions & { stopPropagation?: boolean };
  };
}

export interface TouchStoredHandlers {
  touchstart: (e: TouchEvent) => void;
  touchend: (e: TouchEvent) => void;
  touchmove: (e: TouchEvent) => void;
}

const handleGesture = (wrapper: TouchWrapper) => {
  const { touchstartX, touchendX, touchstartY, touchendY, dotnetHelper } =
    wrapper;
  const dirRatio = 0.5;
  const minDistance = 16;
  wrapper.offsetX = touchendX - touchstartX;
  wrapper.offsetY = touchendY - touchstartY;

  if (Math.abs(wrapper.offsetY) < dirRatio * Math.abs(wrapper.offsetX)) {
    if (touchendX < touchstartX - minDistance) {
      dotnetHelper.invokeMethodAsync("OnTouchend", "left" /*, wrapper*/);
    }
    if (touchendX > touchstartX + minDistance) {
      dotnetHelper.invokeMethodAsync("OnTouchend", "right" /*, wrapper*/);
    }
  }

  if (Math.abs(wrapper.offsetX) < dirRatio * Math.abs(wrapper.offsetY)) {
    if (touchendY < touchstartY - minDistance) {
      dotnetHelper.invokeMethodAsync("OnTouchend", "up" /*, wrapper*/);
    }
    if (touchendY > touchstartY + minDistance) {
      dotnetHelper.invokeMethodAsync("OnTouchend", "down" /*, wrapper*/);
    }
  }
};

function touchstart(event: TouchEvent, wrapper: TouchWrapper) {
  const { dotnetHelper } = wrapper;
  const touch = event.changedTouches[0];
  wrapper.touchstartX = touch.clientX;
  wrapper.touchstartY = touch.clientY;

  // dotnetHelper.invokeMethodAsync("OnTouch", "start", {
  //   originEvent: event,
  //   ...wrapper,
  // });
}

function touchend(event: TouchEvent, wrapper: TouchWrapper) {
  const { dotnetHelper } = wrapper;
  const touch = event.changedTouches[0];
  wrapper.touchendX = touch.clientX;
  wrapper.touchendY = touch.clientY;

  // dotnetHelper.invokeMethodAsync("OnTouch", "end", {
  //   originEvent: event,
  //   ...wrapper,
  // });

  handleGesture(wrapper);
}

function touchmove(event: TouchEvent, wrapper: TouchWrapper) {
  const { dotnetHelper } = wrapper;
  const touch = event.changedTouches[0];
  wrapper.touchmoveX = touch.clientX;
  wrapper.touchmoveY = touch.clientY;

  // dotnetHelper.invokeMethodAsync("OnTouch", "move", {
  //   originEvent: event,
  //   ...wrapper,
  // });
}

function createHandlers(
  options: TouchValue["options"],
  dotnetHelper: DotNet.DotNetObject
): TouchStoredHandlers {
  const wrapper = {
    touchstartX: 0,
    touchstartY: 0,
    touchendX: 0,
    touchendY: 0,
    touchmoveX: 0,
    touchmoveY: 0,
    offsetX: 0,
    offsetY: 0,
    dotnetHelper,
  };

  return {
    touchstart: (e: TouchEvent) => {
      options.touchstart?.stopPropagation && e.stopPropagation();
      touchstart(e, wrapper);
    },
    touchend: (e: TouchEvent) => {
      options.touchend?.stopPropagation && e.stopPropagation();
      touchend(e, wrapper);
    },
    touchmove: (e: TouchEvent) => {
      options.touchmove?.stopPropagation && e.stopPropagation();
      touchmove(e, wrapper);
    },
  };
}

let touchHandlersId = 0;

export function useTouch(
  el: HTMLElement,
  value?: TouchValue,
  dotnetHelper?: any
) {
  const target = value?.parent ? el.parentElement : el;
  const options = value?.options ?? {};

  if (!target) return null;

  const handlers = createHandlers(options, dotnetHelper);

  target._touchHandlers = target._touchHandlers ?? Object.create(null);
  target._touchHandlers![touchHandlersId] = handlers;

  Object.keys(handlers).forEach((eventName) => {
    const { stopPropagation, ...eventOptions } =
      options[eventName] ?? ({ passive: true } as AddEventListenerOptions);
    target.addEventListener(eventName, handlers[eventName], eventOptions);
  });

  return touchHandlersId++;
}

export function cleanupTouch(el: HTMLElement, id: number) {
  if (!el) return;

  const target = el._touchHandlers;

  if (!target || !target[id]) return;

  const handlers = target[id];

  Object.keys(handlers).forEach((eventName) => {
    el.removeEventListener(eventName, handlers[eventName]);
  });

  delete target[id];
}
