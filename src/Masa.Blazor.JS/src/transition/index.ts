import { getBlazorId } from "utils/helper";

class Transition {
  el: HTMLElement;
  handle: DotNet.DotNetObject;

  constructor(elOrSelector: HTMLElement, handle: DotNet.DotNetObject) {
    this.handle = handle;
    this.el = elOrSelector;
    this.el.addEventListener("transitionend", this._onTransitionEnd);
    this.el.addEventListener("transitioncancel", this._onTransitionCancel);
  }

  _onTransitionEnd = (e: TransitionEvent) => {
    const leaveEnter = this._getTransitionLeaveEnter(e);
    if (!leaveEnter) return;

    this.handle.invokeMethodAsync(
      "OnTransitionEnd",
      getBlazorId(e.target),
      leaveEnter == "leave" ? 0 : 1
    );
  };

  _onTransitionCancel = (e: TransitionEvent) => {
    const leaveEnter = this._getTransitionLeaveEnter(e);
    if (!leaveEnter) return;

    this.handle.invokeMethodAsync(
      "OnTransitionCancel",
      getBlazorId(e.target),
      leaveEnter == "leave" ? 0 : 1
    );
  };

  _getTransitionLeaveEnter(e: TransitionEvent): "leave" | "enter" | undefined {
    const classNames = e.target.className.split(" ");
    if (classNames.some((n) => n.includes("transition-leave"))) {
      return "leave";
    } else if (classNames.some((n) => n.includes("transition-enter"))) {
      return "enter";
    } else {
      return undefined;
    }
  }

  reset(el: HTMLElement) {
    this.el.removeEventListener("transitionend", this._onTransitionEnd);
    this.el.removeEventListener("transitioncancel", this._onTransitionCancel);
    this.el = el;
    this.el.addEventListener("transitionend", this._onTransitionEnd);
    this.el.addEventListener("transitioncancel", this._onTransitionCancel);
  }

  dispose() {
    this.el.removeEventListener("transitionend", this._onTransitionEnd);
    this.el.removeEventListener("transitioncancel", this._onTransitionCancel);
    this.handle.dispose();
  }
}

function init(elOrSelector: HTMLElement | string, handle: DotNet.DotNetObject) {
  let el: HTMLElement
  if (typeof elOrSelector === "string") {
    el = document.querySelector(elOrSelector);
  } else {
    el = elOrSelector;
  }

  if (!el) {
    return null;
  }

  const transitionEl = new Transition(el, handle);
  return transitionEl;
}

export { init };
