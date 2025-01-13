import { b as __awaiter } from '../../chunks/tslib.es6.js';
import { formatToStandardOptions } from '../../mixins/intersect/index.js';
import '../../chunks/helper.js';

class ScrollToTargetJSInterop {
    constructor(handle, options) {
        this.activeStack = [];
        this.handle = handle;
        this.options = formatToStandardOptions(options);
    }
    observe(id) {
        const el = document.getElementById(id);
        if (!el) {
            console.warn(`[ScrollToTarget] Element with id '${id}' not found`);
            return;
        }
        if (el["_intersectForScrollToTarget"]) {
            return;
        }
        const observer = new IntersectionObserver((entries = [], observer) => __awaiter(this, void 0, void 0, function* () {
            const isIntersecting = entries.some((e) => e.isIntersecting);
            if (isIntersecting) {
                this.activeStack.push(id);
            }
            else if (this.activeStack.includes(id)) {
                this.activeStack.splice(this.activeStack.indexOf(id), 1);
            }
            yield this.handle.invokeMethodAsync("UpdateActiveTarget", this.activeStack[this.activeStack.length - 1]);
        }), this.options);
        el["_intersectForScrollToTarget"] = Object(el["_intersectForScrollToTarget"]);
        el["_intersectForScrollToTarget"] = { handle: this.handle, observer };
        observer.observe(el);
    }
    unobserve(id) {
        const el = document.getElementById(id);
        if (!el)
            return;
        const observe = el["_intersectForScrollToTarget"];
        if (!observe)
            return;
        observe.observer.unobserve(el);
        delete el["_intersectForScrollToTarget"];
    }
    dispose() {
        if (this.handle) {
            this.handle.dispose();
            this.handle = null;
        }
    }
}
function init(handle, options) {
    return new ScrollToTargetJSInterop(handle, options);
}

export { init };
//# sourceMappingURL=index.js.map
