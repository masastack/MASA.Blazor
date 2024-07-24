import { getBlazorId } from "utils/helper";

class Transition {
  el: HTMLElement;
  handle?: DotNet.DotNetObject;

  constructor(elOrSelector: HTMLElement, handle: DotNet.DotNetObject) {
    this.handle = handle;
    this.el = elOrSelector;
    this.el.addEventListener("transitionend", this._onTransitionEnd);
    this.el.addEventListener("transitioncancel", this._onTransitionCancel);
  }

  _onTransitionEnd = (e: TransitionEvent) => {
    const leaveEnter = this._getTransitionLeaveEnter(e);
    if (!leaveEnter) return;

    this.handle &&
      this.handle.invokeMethodAsync(
        "OnTransitionEnd",
        getBlazorId(e.target),
        leaveEnter == "leave" ? 0 : 1
      );
  };

  _onTransitionCancel = (e: TransitionEvent) => {
    const leaveEnter = this._getTransitionLeaveEnter(e);
    if (!leaveEnter) return;

    this.handle &&
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

    delete transitionInstances[this.handle["_id"]];
    this.handle.dispose();
    this.handle = null;
  }
}

// Store all instances of transition,
// avoid creating multiple instances of the same element,
// because we can't guarantee that js interop will only call the instantiation method once
let transitionInstances = {
  number: Transition,
};

function init(elOrSelector: HTMLElement | string, handle: DotNet.DotNetObject) {
  let el: HTMLElement;
  if (typeof elOrSelector === "string") {
    el = document.querySelector(elOrSelector);
  } else {
    el = elOrSelector;
  }

  if (!el || !handle) {
    return null;
  }

  const dotNetObjectId = handle["_id"];

  if (transitionInstances[dotNetObjectId]) {
    return transitionInstances[dotNetObjectId];
  }

  const transitionEl = new Transition(el, handle);
  transitionInstances[dotNetObjectId] = transitionEl;

  return transitionEl;
}

export { init };
