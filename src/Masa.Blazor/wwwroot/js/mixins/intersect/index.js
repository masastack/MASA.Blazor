import { b as __awaiter } from '../../chunks/tslib.es6.js';
import { g as getEventTarget } from '../../chunks/helper.js';

function observe(el, handle, options) {
    var _a;
    if (!handle) {
        throw new Error("the handle cannot be null");
    }
    if (!el) {
        handle.dispose();
        return;
    }
    if (el["_intersect"]) {
        return;
    }
    const once = (_a = options === null || options === void 0 ? void 0 : options.once) !== null && _a !== void 0 ? _a : false;
    const standardOptions = formatToStandardOptions(options);
    const observer = new IntersectionObserver((entries = [], observer) => __awaiter(this, void 0, void 0, function* () {
        const computedEntries = entries.map((entry) => ({
            isIntersecting: entry.isIntersecting,
            target: getEventTarget(entry.target),
        }));
        const isIntersecting = computedEntries.some((e) => e.isIntersecting);
        if (!once || isIntersecting) {
            yield handle.invokeMethodAsync("Invoke", {
                isIntersecting,
                entries: computedEntries,
            });
        }
        if (isIntersecting && once) {
            unobserve(el);
        }
    }), standardOptions);
    el["_intersect"] = Object(el["_intersect"]);
    el["_intersect"] = { handle, observer };
    observer.observe(el);
}
function unobserve(el) {
    if (!el)
        return;
    const observe = el["_intersect"];
    if (!observe)
        return;
    observe.observer.unobserve(el);
    observe.handle.dispose();
    delete el["_intersect"];
}
function observeSelector(selector, handle, options) {
    if (selector) {
        const el = document.querySelector(selector);
        el && observe(el, handle, options);
    }
}
function unobserveSelector(selector) {
    if (selector) {
        const el = document.querySelector(selector);
        el && unobserve(el);
    }
}
function formatToStandardOptions(options) {
    if (!options) {
        return null;
    }
    const root = options.rootSelector
        ? document.querySelector(options.rootSelector)
        : null;
    if (options.autoRootMargin !== "None") {
        if (options.autoRootMargin === "Top" &&
            options.rootMarginBottom !== "0px") {
            options.rootMarginTop =
                calcAuto(root, options.rootMarginBottom, false) + "px";
        }
        else if (options.autoRootMargin === "Right" &&
            options.rootMarginLeft !== "0px") {
            options.rootMarginRight =
                calcAuto(root, options.rootMarginLeft, false) + "px";
        }
        else if (options.autoRootMargin === "Bottom" &&
            options.rootMarginTop !== "0px") {
            options.rootMarginBottom =
                calcAuto(root, options.rootMarginTop, false) + "px";
        }
        else if (options.autoRootMargin === "Left" &&
            options.rootMarginRight !== "0px") {
            options.rootMarginLeft =
                calcAuto(root, options.rootMarginRight, false) + "px";
        }
    }
    return {
        rootMargin: `${options.rootMarginTop} ${options.rootMarginRight} ${options.rootMarginBottom} ${options.rootMarginLeft}`,
        root,
        threshold: options.threshold,
    };
}
function calcAuto(container, margin, x) {
    container = container || document.documentElement;
    const marginValue = parseInt(margin);
    if (isNaN(marginValue)) {
        return 0;
    }
    var clientValue = x ? container.clientWidth : container.clientHeight;
    return Math.abs(marginValue) - clientValue;
}

export { formatToStandardOptions, observe, observeSelector, unobserve, unobserveSelector };
//# sourceMappingURL=index.js.map
