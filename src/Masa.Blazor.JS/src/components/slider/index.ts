import { createSharedEventArgs } from "events/extra";
import { addOnceEventListener, passiveSupported } from "utils/helper";

let sliderHandleId = 0;
const sliderHandlesById = {};

export function registerSliderEvents(
  el: HTMLElement,
  dotnetHelper: DotNet.DotNetObject
) {
  sliderHandlesById[sliderHandleId] = onSliderMouseDown;

  const app = document.querySelector("[data-app]");

  const mouseUpOptions = passiveSupported
    ? { passive: true, capture: true }
    : true;

  const mouseMoveOptions = passiveSupported ? { passive: true } : false;

  el.addEventListener("mousedown", onSliderMouseDown);
  el.addEventListener("touchstart", onSliderMouseDown);

  return sliderHandleId++;

  async function onSliderMouseDown(e: MouseEvent | TouchEvent) {
    const isTouchEvent = "touches" in e;

    onMouseMove(e);

    app.addEventListener(
      isTouchEvent ? "touchmove" : "mousemove",
      onMouseMove,
      mouseMoveOptions
    );
    addOnceEventListener(
      app,
      isTouchEvent ? "touchend" : "mouseup",
      onSliderMouseUp,
      mouseUpOptions
    );

    if (isTouchEvent) {
      await dotnetHelper.invokeMethodAsync(
        "OnTouchStartInternal",
        createSharedEventArgs("touch", e)
      );
    } else {
      await dotnetHelper.invokeMethodAsync(
        "OnMouseDownInternal",
        createSharedEventArgs("mouse", e)
      );
    }
  }

  async function onSliderMouseUp(e: Event) {
    e.stopPropagation();

    app.removeEventListener("touchmove", onMouseMove, mouseMoveOptions as any);
    app.removeEventListener("mousemove", onMouseMove, mouseMoveOptions as any);

    await dotnetHelper.invokeMethodAsync("OnMouseUpInternal");
  }

  async function onMouseMove(e: MouseEvent | TouchEvent) {
    const isTouchEvent = "touches" in e;
    const payload = {
      type: e.type,
      clientX: isTouchEvent ? e.touches[0].clientX : e.clientX,
      clientY: isTouchEvent ? e.touches[0].clientY : e.clientY,
    };

    await dotnetHelper.invokeMethodAsync("OnMouseMoveInternal", payload);
  }
}

export function unregisterSliderEvents(el: HTMLElement, id: number) {
  if (el) {
    const onSliderMouseDown = sliderHandlesById[id];
    el.removeEventListener("mousedown", onSliderMouseDown);
    el.removeEventListener("touchstart", onSliderMouseDown);

    delete sliderHandlesById[id];
  }
}
