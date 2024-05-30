import { parseMouseEvent } from "../../events/EventType";
import Delayable from "../delayable";

type Listeners = Record<
  string,
  (e: MouseEvent & KeyboardEvent & FocusEvent) => void
>;

class Activatable extends Delayable {
  activator?: HTMLElement;
  popupElement?: HTMLElement;
  disabled: boolean;
  openOnClick: boolean;
  openOnHover: boolean;
  openOnFocus: boolean;

  closeOnOutsideClick: boolean;
  closeOnContentClick: boolean;

  isActive: boolean;
  activatorListeners: Listeners = {};
  popupListeners: Listeners = {};

  constructor(
    activatorSelector: string,
    disabled: boolean,
    openOnClick: boolean,
    openOnHover: boolean,
    openOnFocus: boolean,
    openDelay: number,
    closeDelay: number,
    dotNetHelper: DotNet.DotNetObject
  ) {
    super(openDelay, closeDelay, dotNetHelper);

    const activator = document.querySelector(activatorSelector);
    if (activator) {
      this.activator = activator as HTMLElement;
    }

    this.disabled = disabled;
    this.openOnClick = openOnClick;
    this.openOnHover = openOnHover;
    this.openOnFocus = openOnFocus;
    this.dotNetHelper = dotNetHelper;
  }

  //#region activators

  resetActivator(selector: string) {
    const activator = document.querySelector(selector);
    if (activator) {
      this.activator = activator as HTMLElement;
    }

    this.resetActivatorEvents(
      this.disabled,
      this.openOnHover,
      this.openOnFocus
    );
  }

  addActivatorEvents() {
    if (!this.activator || this.disabled) return;

    this.activatorListeners = this.genActivatorListeners();
    const keys = Object.keys(this.activatorListeners);

    for (const key of keys) {
      this.activator.addEventListener(key, this.activatorListeners[key] as any);
    }
  }

  genActivatorListeners() {
    if (this.disabled) return {};

    const listeners: Listeners = {};

    if (this.openOnHover) {
      listeners.mouseenter = (e: MouseEvent) => {
        this.runDelay("open");
      };
      listeners.mouseleave = (e: MouseEvent) => {
        this.runDelay("close");
      };
    } else if (this.openOnClick) {
      listeners.click = (e: MouseEvent) => {
        if (this.activator) this.activator.focus();

        e.stopPropagation();

        this.dotNetHelper.invokeMethodAsync("OnClick", parseMouseEvent(e));

        this.setActive(!this.isActive);
      };
    }

    if (this.openOnFocus) {
      listeners.focus = (e: FocusEvent) => {
        e.stopPropagation();

        this.runDelay("open");
      };

      listeners.blur = (e: FocusEvent) => {
        this.runDelay("close");
      };
    }

    return listeners;
  }

  removeActivatorEvents() {
    if (!this.activator) return;

    const keys = Object.keys(this.activatorListeners);

    for (const key of keys) {
      this.activator.removeEventListener(key, this.activatorListeners[key]);
    }

    this.activatorListeners = {};
  }

  resetActivatorEvents(
    disabled: boolean,
    openOnHover: boolean,
    openOnFocus: boolean
  ) {
    this.disabled = disabled;
    this.openOnHover = openOnHover;
    this.openOnFocus = openOnFocus;

    this.removeActivatorEvents();
    this.addActivatorEvents();
  }

  runDelaying(val: boolean) {
    this.runDelay(val ? "open" : "close");
  }

  //#endregion

  //#region popups

  registerPopup(popupSelector: string, closeOnContentClick: boolean) {
    const popup = document.querySelector(popupSelector);
    if (!popup) {
      console.error("popup not exists");
      return;
    }

    this.popupElement = popup as HTMLElement;
    this.closeOnContentClick = closeOnContentClick;

    this.addPopupEvents();
  }

  addPopupEvents() {
    if (!this.popupElement || this.disabled) return;

    this.popupListeners = this.genPopupListeners();
    const keys = Object.keys(this.popupListeners);

    for (const key of keys) {
      this.popupElement.addEventListener(key, this.popupListeners[key] as any);
    }
  }

  removePopupEvents() {
    if (!this.popupElement) return;

    const keys = Object.keys(this.popupListeners);

    for (const key of keys) {
      this.popupElement.removeEventListener(key, this.popupListeners[key]);
    }

    this.popupListeners = {};
  }

  genPopupListeners() {
    if (this.disabled) return;

    const listeners: Listeners = {};

    if (!this.disabled && this.openOnHover) {
      listeners.mouseenter = (e) => {
        this.runDelay("open");
      };

      listeners.mouseleave = (e) => {
        this.runDelay("close");
      };
    }

    if (this.closeOnContentClick) {
      listeners.click = (e) => {
        this.setActive(false);
      };
    }

    return listeners;
  }

  resetPopupEvents(closeOnContentClick: boolean) {
    this.closeOnContentClick = closeOnContentClick;

    this.removePopupEvents();
    this.addPopupEvents();
  }

  //#endregion
}

function init(
  activatorSelector: string,
  disabled: boolean,
  openOnClick: boolean,
  openOnHover: boolean,
  openOnFocus: boolean,
  openDelay: number,
  closeDelay: number,
  dotNetHelper: DotNet.DotNetObject
) {
  var instance = new Activatable(
    activatorSelector,
    disabled,
    openOnClick,
    openOnHover,
    openOnFocus,
    openDelay,
    closeDelay,
    dotNetHelper
  );

  instance.addActivatorEvents();

  return instance;
}

export { init };
