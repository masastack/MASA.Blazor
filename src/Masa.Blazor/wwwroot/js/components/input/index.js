import { _ as __classPrivateFieldGet } from '../../chunks/tslib.es6.js';
import { p as parseMouseEvent, a as parseTouchEvent, b as parseChangeEvent } from '../../chunks/EventType.js';
import { g as getEventTarget } from '../../chunks/helper.js';

function createSharedEventArgs(type, e) {
    let args = { target: {} };
    if (type === 'mouse') {
        args = Object.assign(Object.assign({}, args), parseMouseEvent(e));
    }
    else if (type === 'touch') {
        args = Object.assign(Object.assign({}, args), parseTouchEvent(e));
    }
    args.target = getEventTarget(e.target);
    return args;
}

// @event:stopPropagation only works in Blazor,
// so need to capture it manually.
function checkIfStopPropagationExistsInComposedPath(event, eventName, util) {
    let stopPropagation = false;
    for (const eventTarget of event.composedPath()) {
        if (eventTarget === util) {
            break;
        }
        if (checkIfStopPropagation(eventTarget, eventName)) {
            stopPropagation = true;
            break;
        }
    }
    return stopPropagation;
}
function checkIfStopPropagation(eventTarget, eventName) {
    const nestProps = ["_blazorEvents_1", "stopPropagationFlags", eventName];
    let isFlag = eventTarget;
    let i = 0;
    while (isFlag[nestProps[i]]) {
        isFlag = isFlag[nestProps[i]];
        i++;
    }
    return i == nestProps.length && typeof isFlag === "boolean" && isFlag;
}

var _Input_instances, _Input_registerAllEvents, _Input_registerInputEvent, _Input_formatNumberValue, _Input_registerClickEvent, _Input_registerMouseUpEvent;
class Input {
    constructor(dotnetHelper, input, inputSlot, debounce) {
        _Input_instances.add(this);
        this.input = input;
        this.inputSlot = inputSlot;
        this.dotnetHelper = dotnetHelper;
        this.debounce = debounce;
        __classPrivateFieldGet(this, _Input_instances, "m", _Input_registerAllEvents).call(this);
    }
    setValue(value) {
        this.input.value = value;
    }
}
_Input_instances = new WeakSet(), _Input_registerAllEvents = function _Input_registerAllEvents() {
    if (!this.input || !this.inputSlot)
        return;
    __classPrivateFieldGet(this, _Input_instances, "m", _Input_registerClickEvent).call(this);
    __classPrivateFieldGet(this, _Input_instances, "m", _Input_registerMouseUpEvent).call(this);
    if (!(this.input &&
        (this.input instanceof HTMLInputElement ||
            this.input instanceof HTMLTextAreaElement)))
        return;
    __classPrivateFieldGet(this, _Input_instances, "m", _Input_registerInputEvent).call(this);
}, _Input_registerInputEvent = function _Input_registerInputEvent() {
    let compositionInputting = false;
    let timeout;
    this.input.addEventListener("compositionstart", (_) => {
        compositionInputting = true;
    });
    this.input.addEventListener("compositionend", (event) => {
        compositionInputting = false;
        const changeEventArgs = parseChangeEvent(event);
        changeEventArgs.value = this.input.value;
        if (this.input.maxLength !== -1 &&
            changeEventArgs.value.length > this.input.maxLength) {
            changeEventArgs.value = changeEventArgs.value.substring(0, this.input.maxLength);
        }
        this.dotnetHelper.invokeMethodAsync("OnInput", changeEventArgs);
    });
    this.input.addEventListener("input", (event) => {
        if (compositionInputting)
            return;
        var changeEventArgs = parseChangeEvent(event);
        clearTimeout(timeout);
        timeout = setTimeout(() => {
            this.dotnetHelper.invokeMethodAsync("OnInput", changeEventArgs);
        }, this.debounce);
    });
    this.input.addEventListener('change', (event) => {
        var changeEventArgs = parseChangeEvent(event);
        __classPrivateFieldGet(this, _Input_instances, "m", _Input_formatNumberValue).call(this, event);
        this.dotnetHelper.invokeMethodAsync("OnChange", changeEventArgs);
    });
}, _Input_formatNumberValue = function _Input_formatNumberValue(event) {
    if (event.target.type === "number") {
        const value = event.target.value;
        const valueAsNumber = event.target.valueAsNumber;
        if (!!value && value !== valueAsNumber.toString()) {
            this.input.value = isNaN(valueAsNumber)
                ? ""
                : valueAsNumber.toString();
        }
    }
}, _Input_registerClickEvent = function _Input_registerClickEvent() {
    this.inputSlot.addEventListener("click", (e) => {
        if (checkIfStopPropagationExistsInComposedPath(e, "click", this.inputSlot)) {
            return;
        }
        var eventArgs = createSharedEventArgs("mouse", e);
        this.dotnetHelper.invokeMethodAsync("OnClick", eventArgs);
    });
}, _Input_registerMouseUpEvent = function _Input_registerMouseUpEvent() {
    this.inputSlot.addEventListener("mouseup", (e) => {
        if (checkIfStopPropagationExistsInComposedPath(e, "mouseup", this.inputSlot)) {
            return;
        }
        var eventArgs = createSharedEventArgs("mouse", e);
        this.dotnetHelper.invokeMethodAsync("OnMouseUp", eventArgs);
    });
};
function init(dotnetHelper, input, inputSlot, debounce) {
    return new Input(dotnetHelper, input, inputSlot, debounce);
}

export { init };
//# sourceMappingURL=index.js.map
