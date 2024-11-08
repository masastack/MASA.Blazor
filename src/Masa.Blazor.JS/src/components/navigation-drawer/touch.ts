import { useVelocity } from "mixins/touch";

type State = {
  isActive: boolean;
  isTemporary: boolean;
  width: number;
  touchless: boolean;
  position: "left" | "right" | "top" | "bottom";
};

export function useTouch(
  el: HTMLElement,
  dotNetObject: DotNet.DotNetObject,
  state: State
) {
  window.addEventListener("touchstart", onTouchstart, { passive: true });
  window.addEventListener("touchmove", onTouchmove, { passive: false });
  window.addEventListener("touchend", onTouchend, { passive: true });

  const isHorizontal = ["left", "right"].includes(state.position);

  const { addMovement, endTouch, getVelocity } = useVelocity();

  let maybeDragging = false;
  let isDragging = false;
  let dragProgress = 0;
  let offset = 0;
  let start: [number, number] | undefined;

  let transformBeforeTouchMove = el.style.transform ?? null;
  let transitionBeforeTouchMove = el.style.transition ?? null;

  function getOffset(pos: number, active: boolean): number {
    return (
      (state.position === "left"
        ? pos
        : state.position === "right"
        ? document.documentElement.clientWidth - pos
        : state.position === "top"
        ? pos
        : state.position === "bottom"
        ? document.documentElement.clientHeight - pos
        : oops()) - (active ? state.width : 0)
    );
  }

  function getProgress(pos: number, limit = true): number {
    const progress =
      state.position === "left"
        ? (pos - offset) / state.width
        : state.position === "right"
        ? (document.documentElement.clientWidth - pos - offset) / state.width
        : state.position === "top"
        ? (pos - offset) / state.width
        : state.position === "bottom"
        ? (document.documentElement.clientHeight - pos - offset) / state.width
        : oops();
    return limit ? Math.max(0, Math.min(1, progress)) : progress;
  }

  function onTouchstart(e: TouchEvent) {
    if (state.touchless) return;

    transformBeforeTouchMove = el.style.transform ?? null;
    transitionBeforeTouchMove = el.style.transition ?? null;

    const touchX = e.changedTouches[0].clientX;
    const touchY = e.changedTouches[0].clientY;

    const touchZone = 25;
    const inTouchZone: boolean =
      state.position === "left"
        ? touchX < touchZone
        : state.position === "right"
        ? touchX > document.documentElement.clientWidth - touchZone
        : state.position === "top"
        ? touchY < touchZone
        : state.position === "bottom"
        ? touchY > document.documentElement.clientHeight - touchZone
        : oops();

    const inElement: boolean =
      state.isActive &&
      (state.position === "left"
        ? touchX < state.width
        : state.position === "right"
        ? touchX > document.documentElement.clientWidth - state.width
        : state.position === "top"
        ? touchY < state.width
        : state.position === "bottom"
        ? touchY > document.documentElement.clientHeight - state.width
        : oops());

    if (inTouchZone || inElement || (state.isActive && state.isTemporary)) {
      start = [touchX, touchY];

      offset = getOffset(isHorizontal ? touchX : touchY, state.isActive);
      dragProgress = getProgress(isHorizontal ? touchX : touchY);

      maybeDragging = offset > -20 && offset < 80;
      endTouch(e);
      addMovement(e);
    }
  }

  function onTouchmove(e: TouchEvent) {
    const touchX = e.changedTouches[0].clientX;
    const touchY = e.changedTouches[0].clientY;

    if (maybeDragging) {
      if (!e.cancelable) {
        maybeDragging = false;
        return;
      }

      const dx = Math.abs(touchX - start![0]);
      const dy = Math.abs(touchY - start![1]);

      const thresholdMet = isHorizontal ? dx > dy && dx > 3 : dy > dx && dy > 3;

      if (thresholdMet) {
        isDragging = true;
        maybeDragging = false;
      } else if ((isHorizontal ? dy : dx) > 3) {
        maybeDragging = false;
      }
    }

    applyDragStyles();

    if (!isDragging) return;

    e.preventDefault();
    addMovement(e);

    const progress = getProgress(isHorizontal ? touchX : touchY, false);
    dragProgress = Math.max(0, Math.min(1, progress));

    if (progress > 1) {
      offset = getOffset(isHorizontal ? touchX : touchY, true);
    } else if (progress < 0) {
      offset = getOffset(isHorizontal ? touchX : touchY, false);
    }

    dotNetObject.invokeMethodAsync("TouchMove", isDragging, dragProgress);
  }

  function onTouchend(e: TouchEvent) {
    maybeDragging = false;

    if (!isDragging) return;

    addMovement(e);

    isDragging = false;

    applyDragStyles();

    const velocity = getVelocity(e.changedTouches[0].identifier);
    const vx = Math.abs(velocity.x);
    const vy = Math.abs(velocity.y);
    const thresholdMet = isHorizontal ? vx > vy && vx > 400 : vy > vx && vy > 3;

    if (thresholdMet) {
      state.isActive =
        velocity.direction ===
        ({
          left: "right",
          right: "left",
          top: "down",
          bottom: "up",
        }[state.position] || oops());
    } else {
      state.isActive = dragProgress > 0.5;
    }

    dotNetObject.invokeMethodAsync("TouchEnd", state.isActive);
  }

  const getDragStyles = () => {
    return isDragging
      ? {
          transform:
            state.position === "left"
              ? `translateX(calc(-100% + ${dragProgress * state.width}px))`
              : state.position === "right"
              ? `translateX(calc(100% - ${dragProgress * state.width}px))`
              : state.position === "top"
              ? `translateY(calc(-100% + ${dragProgress * state.width}px))`
              : state.position === "bottom"
              ? `translateY(calc(100% - ${dragProgress * state.width}px))`
              : oops(),
          transition: "none",
        }
      : undefined;
  };

  const applyDragStyles = () => {
    const dragStyles = getDragStyles();

    if (isDragging) {
      el.style.setProperty("transform", dragStyles?.transform || "none");
      el.style.setProperty("transition", dragStyles?.transition || null);
    } else {
      el.style.setProperty("transform", transformBeforeTouchMove);
      el.style.setProperty("transition", transitionBeforeTouchMove);
    }
  };

  return {
    syncState: (newState: State) => {
      state = newState;
    },
    dispose: () => {
      dotNetObject.invokeMethodAsync("Dispose");
      window.removeEventListener("touchstart", onTouchstart);
      window.removeEventListener("touchmove", onTouchmove);
      window.removeEventListener("touchend", onTouchend);
    },
  };
}

function oops(): never {
  throw new Error();
}
