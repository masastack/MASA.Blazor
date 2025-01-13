import { b as __awaiter } from '../../chunks/tslib.es6.js';

const dotnetRefs = {};
const clickHandlers = {};
const popstateHandlers = {};
let nextId = 0;
function attachListener(handle) {
    const id = nextId;
    const clickHandler = (event) => onDocumentClick(id, event);
    const popstateHandler = () => handle.invokeMethodAsync("Popstate", window.location.pathname);
    clickHandlers[id] = clickHandler;
    popstateHandlers[id] = popstateHandler;
    dotnetRefs[id] = handle;
    document.addEventListener("click", clickHandler);
    window.addEventListener("popstate", popstateHandler);
    return nextId++;
}
function onDocumentClick(id, event) {
    return __awaiter(this, void 0, void 0, function* () {
        const dotnet = dotnetRefs[id];
        if (dotnet === null)
            return;
        const anchor = event.target.closest("a");
        if (!anchor)
            return;
        const href = anchor.getAttribute("href");
        if (!href)
            return;
        let strategy = anchor.getAttribute("data-page-stack-strategy");
        strategy = strategy === null ? null : strategy.toLowerCase();
        if (strategy === "" || strategy === "true" || strategy === "push") {
            yield dotnet.invokeMethodAsync("Push", href);
        }
    });
}
function detachListener(id) {
    const clickHandler = clickHandlers[id];
    if (clickHandler) {
        document.removeEventListener("click", clickHandler);
    }
    const popstateHandler = popstateHandlers[id];
    if (popstateHandler) {
        window.removeEventListener("popstate", popstateHandler);
    }
    dotnetRefs[id] && dotnetRefs[id].dispose();
    delete clickHandlers[id];
    delete popstateHandlers[id];
    delete dotnetRefs[id];
}

export { attachListener, detachListener };
//# sourceMappingURL=index.js.map
