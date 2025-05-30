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

  const trackElement = el.querySelector(".m-slider__track-container");

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

    var trackRect = trackElement.getBoundingClientRect();

    if (isTouchEvent) {
      await dotnetHelper.invokeMethodAsync("OnTouchStartInternal", {
        touchEventArgs: createSharedEventArgs("touch", e),
        trackRect,
      });
    } else {
      await dotnetHelper.invokeMethodAsync("OnMouseDownInternal", {
        mouseEventArgs: createSharedEventArgs("mouse", e),
        trackRect,
      });
    }
  }

  async function onSliderMouseUp(e: MouseEvent | TouchEvent) {
    e.stopPropagation();

    app.removeEventListener("touchmove", onMouseMove, mouseMoveOptions as any);
    app.removeEventListener("mousemove", onMouseMove, mouseMoveOptions as any);

    const isTouchEvent = "touches" in e;
    const payload = {
      type: e.type,
      clientX: isTouchEvent ? e.changedTouches[0].clientX : e.clientX,
      clientY: isTouchEvent ? e.changedTouches[0].clientY : e.clientY,
    };

    await dotnetHelper.invokeMethodAsync("OnMouseUpInternal", payload);
  }

  async function onMouseMove(e: MouseEvent | TouchEvent) {
    const isTouchEvent = "touches" in e;
    const mouseEventArgs = {
      type: e.type,
      clientX: isTouchEvent ? e.touches[0].clientX : e.clientX,
      clientY: isTouchEvent ? e.touches[0].clientY : e.clientY,
    };

    var trackRect = trackElement.getBoundingClientRect();

    await dotnetHelper.invokeMethodAsync("OnMouseMoveInternal", {
      mouseEventArgs,
      trackRect,
    });
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
