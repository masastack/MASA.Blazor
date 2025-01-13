import { d as getActivator } from '../../chunks/helper.js';
import { p as parseMouseEvent } from '../../chunks/EventType.js';

class Delayable {
    constructor(openDelay, closeDelay, dotNetHelper) {
        this.openDelay = openDelay;
        this.closeDelay = closeDelay;
        this.dotNetHelper = dotNetHelper;
    }
    clearDelay() {
        clearTimeout(this.openTimeout);
        clearTimeout(this.closeTimeout);
    }
    runDelay(type, cb) {
        this.clearDelay();
        const delay = parseInt(this[`${type}Delay`], 10);
        this[`${type}Timeout`] = setTimeout(cb ||
            (() => {
                const isActive = { open: true, close: false }[type];
                this.setActive(isActive);
            }), delay);
    }
    setActive(active) {
        if (this.isActive == active) {
            return;
        }
        this.isActive = active;
        this.dotNetHelper.invokeMethodAsync("SetActive", this.isActive);
    }
    resetDelay(openDelay, closeDelay) {
        this.openDelay = openDelay;
        this.closeDelay = closeDelay;
    }
}

class Activatable extends Delayable {
    constructor(activatorSelector, disabled, openOnClick, openOnHover, openOnFocus, openDelay, closeDelay, dotNetHelper) {
        super(openDelay, closeDelay, dotNetHelper);
        this.activatorListeners = {};
        this.popupListeners = {};
        this.activator = getActivator(activatorSelector);
        this.disabled = disabled;
        this.openOnClick = openOnClick;
        this.openOnHover = openOnHover;
        this.openOnFocus = openOnFocus;
        this.dotNetHelper = dotNetHelper;
    }
    //#region activators
    resetActivator(selector) {
        const activator = document.querySelector(selector);
        if (activator) {
            this.activator = activator;
        }
        this.resetActivatorEvents(this.disabled, this.openOnHover, this.openOnFocus);
    }
    addActivatorEvents() {
        if (!this.activator || this.disabled)
            return;
        this.activatorListeners = this.genActivatorListeners();
        const keys = Object.keys(this.activatorListeners);
        for (const key of keys) {
            this.activator.addEventListener(key, this.activatorListeners[key]);
        }
    }
    genActivatorListeners() {
        if (this.disabled)
            return {};
        const listeners = {};
        if (this.openOnHover) {
            listeners.mouseenter = (e) => {
                this.runDelay("open");
            };
            listeners.mouseleave = (e) => {
                this.runDelay("close");
            };
        }
        else if (this.openOnClick) {
            listeners.click = (e) => {
                if (this.activator)
                    this.activator.focus();
                e.stopPropagation();
                this.dotNetHelper.invokeMethodAsync("OnClick", parseMouseEvent(e));
                this.setActive(!this.isActive);
            };
        }
        if (this.openOnFocus) {
            listeners.focus = (e) => {
                e.stopPropagation();
                this.runDelay("open");
            };
            listeners.blur = (e) => {
                this.runDelay("close");
            };
        }
        return listeners;
    }
    removeActivatorEvents() {
        if (!this.activator)
            return;
        const keys = Object.keys(this.activatorListeners);
        for (const key of keys) {
            this.activator.removeEventListener(key, this.activatorListeners[key]);
        }
        this.activatorListeners = {};
    }
    resetActivatorEvents(disabled, openOnHover, openOnFocus) {
        this.disabled = disabled;
        this.openOnHover = openOnHover;
        this.openOnFocus = openOnFocus;
        this.removeActivatorEvents();
        this.addActivatorEvents();
    }
    runDelaying(val) {
        this.runDelay(val ? "open" : "close");
    }
    //#endregion
    //#region popups
    registerPopup(popupSelector, closeOnContentClick) {
        const popup = document.querySelector(popupSelector);
        if (!popup) {
            console.error("popup not exists");
            return;
        }
        this.popupElement = popup;
        this.closeOnContentClick = closeOnContentClick;
        this.addPopupEvents();
    }
    addPopupEvents() {
        if (!this.popupElement || this.disabled)
            return;
        this.popupListeners = this.genPopupListeners();
        const keys = Object.keys(this.popupListeners);
        for (const key of keys) {
            this.popupElement.addEventListener(key, this.popupListeners[key]);
        }
    }
    removePopupEvents() {
        if (!this.popupElement)
            return;
        const keys = Object.keys(this.popupListeners);
        for (const key of keys) {
            this.popupElement.removeEventListener(key, this.popupListeners[key]);
        }
        this.popupListeners = {};
    }
    genPopupListeners() {
        if (this.disabled)
            return;
        const listeners = {};
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
    resetPopupEvents(closeOnContentClick) {
        this.closeOnContentClick = closeOnContentClick;
        this.removePopupEvents();
        this.addPopupEvents();
    }
}
function init(activatorSelector, disabled, openOnClick, openOnHover, openOnFocus, openDelay, closeDelay, dotNetHelper) {
    var instance = new Activatable(activatorSelector, disabled, openOnClick, openOnHover, openOnFocus, openDelay, closeDelay, dotNetHelper);
    instance.addActivatorEvents();
    return instance;
}

export { init };
//# sourceMappingURL=index.js.map
