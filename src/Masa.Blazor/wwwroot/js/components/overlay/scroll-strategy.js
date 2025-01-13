import { c as convertToUnit } from '../../chunks/helper.js';

function getScrollParents(el, stopAt) {
    const elements = [];
    if (stopAt && el && !stopAt.contains(el))
        return elements;
    while (el) {
        if (hasScrollbar(el))
            elements.push(el);
        if (el === stopAt)
            break;
        el = el.parentElement;
    }
    return elements;
}
function hasScrollbar(el) {
    if (!el || el.nodeType !== Node.ELEMENT_NODE)
        return false;
    const style = window.getComputedStyle(el);
    return style.overflowY === 'scroll' || (style.overflowY === 'auto' && el.scrollHeight > el.clientHeight);
}

function useScrollStrategies(props, root, contentEl, targetEl, dotNet) {
    if (props.strategy === "block") {
        return useBlockScrollStrategy({
            root,
            contentEl,
            targetEl,
        }, props);
    }
    else {
        return useInvokerScrollStrategy({
            root,
            contentEl,
            targetEl,
            invoker: dotNet,
        }, props);
    }
}
function useBlockScrollStrategy(data, options) {
    const offsetParent = data.root.offsetParent;
    const scrollElements = [
        ...new Set([
            ...getScrollParents(data.contentEl, options.contained ? offsetParent : undefined),
        ]),
    ];
    const scrollableParent = ((el) => hasScrollbar(el) && el)(offsetParent || document.documentElement);
    const bind = () => {
        if (scrollableParent) {
            data.root.classList.add("m-overlay--scroll-blocked");
        }
        const scrollbarWidth = window.innerWidth - document.documentElement.offsetWidth;
        scrollElements
            .filter((el) => !el.classList.contains("m-overlay-scroll-blocked"))
            .forEach((el, i) => {
            el.style.setProperty("--m-body-scroll-x", convertToUnit(-el.scrollLeft));
            el.style.setProperty("--m-body-scroll-y", convertToUnit(-el.scrollTop));
            if (el !== document.documentElement) {
                el.style.setProperty("--m-scrollbar-offset", convertToUnit(scrollbarWidth));
            }
            el.classList.add("m-overlay-scroll-blocked");
        });
    };
    bind();
    return {
        bind,
        unbind: () => {
            scrollElements
                .filter((el) => el.classList.contains("m-overlay-scroll-blocked"))
                .forEach((el, i) => {
                const x = parseFloat(el.style.getPropertyValue("--m-body-scroll-x"));
                const y = parseFloat(el.style.getPropertyValue("--m-body-scroll-y"));
                const scrollBehavior = el.style.scrollBehavior;
                el.style.scrollBehavior = "auto";
                el.style.removeProperty("--m-body-scroll-x");
                el.style.removeProperty("--m-body-scroll-y");
                el.style.removeProperty("--m-scrollbar-offset");
                el.classList.remove("m-overlay-scroll-blocked");
                el.scrollLeft = -x;
                el.scrollTop = -y;
                el.style.scrollBehavior = scrollBehavior;
            });
            if (scrollableParent) {
                data.root.classList.remove("m-overlay--scroll-blocked");
            }
        },
    };
}
function useInvokerScrollStrategy(data, options) {
    var _a;
    const el = (_a = data.targetEl) !== null && _a !== void 0 ? _a : data.contentEl;
    const onScroll = () => {
        var _a;
        (_a = data.invoker) === null || _a === void 0 ? void 0 : _a.invokeMethodAsync("ScrollStrategy_OnScroll", options.strategy);
    };
    const scrollElements = [document, ...getScrollParents(el)];
    scrollElements.forEach((el) => el.addEventListener("scroll", onScroll, { passive: true }));
    return {
        unbind: () => {
            var _a;
            (_a = data.invoker) === null || _a === void 0 ? void 0 : _a.dispose();
            scrollElements.forEach((el) => el.removeEventListener("scroll", onScroll));
        },
    };
}

export { useScrollStrategies };
//# sourceMappingURL=scroll-strategy.js.map
