const activator_parent_prefix = "$parent.";
const IN_BROWSER = typeof window !== 'undefined';
let passiveSupported = false;
try {
    if (IN_BROWSER) {
        const testListenerOpts = Object.defineProperty({}, 'passive', {
            get: () => {
                passiveSupported = true;
            },
        });
        window.addEventListener('testListener', testListenerOpts, testListenerOpts);
        window.removeEventListener('testListener', testListenerOpts, testListenerOpts);
    }
}
catch (e) {
    console.warn(e);
} /* eslint-disable-line no-console */
// KeyboardEvent.keyCode aliases
Object.freeze({
    enter: 13,
    tab: 9,
    delete: 46,
    esc: 27,
    space: 32,
    up: 38,
    down: 40,
    left: 37,
    right: 39,
    end: 35,
    home: 36,
    del: 46,
    backspace: 8,
    insert: 45,
    pageup: 33,
    pagedown: 34,
    shift: 16,
});
function getBlazorId(el) {
    if (!el) {
        return null;
    }
    let _bl_ = el.getAttributeNames().find(a => a.startsWith('_bl_'));
    if (_bl_) {
        _bl_ = _bl_.substring(4);
    }
    return _bl_;
}
function getElementSelector(el) {
    if (!(el instanceof Element))
        return;
    var path = [];
    while (el.nodeType === Node.ELEMENT_NODE) {
        var selector = el.nodeName.toLowerCase();
        if (el.id) {
            selector = '#' + el.id;
            path.unshift(selector);
            break;
        }
        else {
            var sib = el, nth = 1;
            while (sib = sib.previousElementSibling) {
                if (sib.nodeName.toLowerCase() == selector)
                    nth++;
            }
            if (nth != 1)
                selector += ":nth-of-type(" + nth + ")";
        }
        path.unshift(selector);
        el = el.parentNode;
    }
    return path.join(" > ");
}
function getEventTarget(target) {
    const el = target;
    const eventTarget = {};
    const elementReferenceId = el
        .getAttributeNames()
        .find((a) => a.startsWith("_bl_"));
    if (elementReferenceId) {
        eventTarget.elementReferenceId = elementReferenceId;
        eventTarget.selector = `[${elementReferenceId}]`;
    }
    else {
        eventTarget.selector = getElementSelector(el);
    }
    eventTarget.class = el.getAttribute("class");
    return eventTarget;
}
function getElement(elOrString) {
    if (typeof elOrString === "string") {
        return document.querySelector(elOrString);
    }
    else {
        return elOrString;
    }
}
function convertToUnit(str, unit = 'px') {
    if (str == null || str === '') {
        return undefined;
    }
    else if (isNaN(+str)) {
        return String(str);
    }
    else if (!isFinite(+str)) {
        return undefined;
    }
    else {
        return `${Number(str)}${unit}`;
    }
}
function getActivator(selector) {
    var _a;
    if (selector.startsWith(activator_parent_prefix)) {
        const parentSelector = selector.replace(activator_parent_prefix, "");
        const parentElement = (_a = document.querySelector(parentSelector)) === null || _a === void 0 ? void 0 : _a.parentElement;
        if (!parentElement) {
            return null;
        }
        // special case for MButton component
        if (parentElement.classList.contains('m-btn__content')) {
            return parentElement.parentElement;
        }
        return parentElement;
    }
    else {
        return document.querySelector(selector);
    }
}

export { getElement as a, getBlazorId as b, convertToUnit as c, getActivator as d, getEventTarget as g };
//# sourceMappingURL=helper.js.map
