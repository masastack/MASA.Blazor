import { useVelocity } from "mixins/touch";
import { getElement } from "utils/helper";

type State = {
  isActive: boolean;
  position: "left" | "right";
};

export function useTouch(
  elOrString: HTMLElement | string,
  dotNetObject: DotNet.DotNetObject,
  state: State,
  previousPageId: number = -1
) {
  const el = getElement(elOrString);

  if (!el) return;

  const underlaySlideEl =
    previousPageId > -1
      ? getElement("[page-stack-id='" + previousPageId + "'] .m-page-stack-item")
      : getElement(".m-page-stack");

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

  function getOffset(pos: number, active: boolean): number {
    return (
      (state.position === "left"
        ? pos
        : state.position === "right"
        ? document.documentElement.clientWidth - pos
        : oops()) - (active ? document.documentElement.clientWidth : 0)
    );
  }

  function getProgress(pos: number, limit = true): number {
    const progress =
      state.position === "left"
        ? (pos - offset) / document.documentElement.clientWidth
        : state.position === "right"
        ? (document.documentElement.clientWidth - pos - offset) /
          document.documentElement.clientWidth
        : oops();
    return limit ? Math.max(0, Math.min(1, progress)) : progress;
  }

  function isActiveElement(e: TouchEvent) {
    const pageStackItem = (e.target as HTMLElement).closest(".m-page-stack-item");
    return pageStackItem && pageStackItem.parentElement === el;
  }

  function onTouchstart(e: TouchEvent) {
    if (!isActiveElement(e)) return;

    const touchX = e.changedTouches[0].clientX;
    const touchY = e.changedTouches[0].clientY;

    const touchZone = 25;
    const inTouchZone: boolean =
      state.position === "left"
        ? touchX < touchZone
        : state.position === "right"
        ? touchX > document.documentElement.clientWidth - touchZone
        : oops();

    const inElement: boolean =
      state.isActive &&
      (state.position === "left"
        ? touchX < document.documentElement.clientWidth
        : state.position === "right"
        ? touchX > 0
        : oops());

    if (inTouchZone || inElement || state.isActive) {
      start = [touchX, touchY];

      offset = getOffset(isHorizontal ? touchX : touchY, state.isActive);
      dragProgress = getProgress(isHorizontal ? touchX : touchY);

      maybeDragging = offset > -20 && offset < 80;
      endTouch(e);
      addMovement(e);
    }
  }

  async function onTouchmove(e: TouchEvent) {
    if (!isActiveElement(e)) return;

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

    applyStyles();
  }

  function onTouchend(e: TouchEvent) {
    if (!isActiveElement(e)) return;

    maybeDragging = false;

    if (!isDragging) return;

    addMovement(e);

    isDragging = false;

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

    applyStyles();

    setTimeout(
      () => dotNetObject.invokeMethodAsync("TouchEnd", state.isActive),
      200 // the transition duration of root element is 200ms
    );
  }

  const applyStyles = () => {
    if (isDragging) {
      const transform =
        state.position === "left"
          ? `translateX(calc(-100% + ${
              dragProgress * document.documentElement.clientWidth
            }px))`
          : state.position === "right"
          ? `translateX(calc(100% - ${
              dragProgress * document.documentElement.clientWidth
            }px))`
          : oops();

      el.style.setProperty("transform", transform);
      el.style.setProperty("transition", "none");

      if (underlaySlideEl) {
        underlaySlideEl.style.setProperty('--m-page-stack-item-progress', `${dragProgress.toFixed(2)}`);
        underlaySlideEl.style.setProperty('transition', 'none');
      }
    } else {
      if (state.isActive) {
        el.style.removeProperty("transform");
      } else {
        el.style.setProperty("transform", "translateX(100%)");
      }

      el.style.removeProperty("transition");

      if (underlaySlideEl) {
        underlaySlideEl.style.setProperty('--m-page-stack-item-progress', state.isActive ? "1" : "0");
        underlaySlideEl.style.removeProperty('transition');
        setTimeout(() => {
          underlaySlideEl.style.removeProperty('--m-page-stack-item-progress');
        }, 300);
      }
    }
  };

  return {
    // not used for now
    syncState: (newState: State) => {
      state = newState;
    },
    dispose: () => {
      dotNetObject.dispose();
      window.removeEventListener("touchstart", onTouchstart);
      window.removeEventListener("touchmove", onTouchmove);
      window.removeEventListener("touchend", onTouchend);
    },
  };
}

function oops(): never {
  throw new Error();
}
