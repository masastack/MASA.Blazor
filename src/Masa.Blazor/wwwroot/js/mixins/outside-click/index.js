class OutsideClick {
    constructor(dotNetHelper, excludedSelectors) {
        this.lastMousedownWasOutside = true;
        this.dotNetHelper = dotNetHelper;
        this.excludedSelectors = excludedSelectors;
    }
    genListeners() {
        this.listener = (e) => {
            if (this.checkEvent(e))
                return;
            if (this.lastMousedownWasOutside) {
                this.dotNetHelper.invokeMethodAsync("OnOutsideClick");
            }
        };
        this.mousedownListener = (e) => {
            this.lastMousedownWasOutside = !this.checkEvent(e);
        };
    }
    addListeners() {
        this.genListeners();
        document.addEventListener("click", this.listener, true);
        document.addEventListener("mousedown", this.mousedownListener, true);
    }
    removeListeners() {
        document.removeEventListener("click", this.listener, true);
        document.removeEventListener("mousedown", this.mousedownListener, true);
    }
    resetListener() {
        this.removeListeners();
        this.addListeners();
    }
    updateExcludeSelectors(selectors) {
        this.excludedSelectors = selectors;
    }
    checkEvent(e) {
        return this.excludedSelectors.some((selector) => {
            const elements = Array.from(document.querySelectorAll(selector));
            return elements.some(el => el.contains(e.target));
        });
    }
    unbind() {
        this.removeListeners();
    }
}
function init(dotNetHelper, excludeSelectors) {
    var instance = new OutsideClick(dotNetHelper, excludeSelectors);
    instance.addListeners();
    return instance;
}

export { init };
//# sourceMappingURL=index.js.map
