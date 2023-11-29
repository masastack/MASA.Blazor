const MASA_BLAZOR_SSR_STATE = "masablazor@ssr-state";
function restoreMain(application) {
    const main = document.querySelector(".m-main");
    if (main && application) {
        main.style.paddingTop = application.top + application.bar + "px";
        main.style.paddingRight = application.right + "px";
        main.style.paddingBottom =
            application.bottom + application.insetFooter + application.bottom + "px";
        main.style.paddingLeft = application.left + "px";
    }
}

let firstUpdate = true;
// Called when the script first gets loaded on the page.
function onLoad() { }
// Called when an enhanced page update occurs, plus once immediately after
// the initial load.
function onUpdate() {
    if (firstUpdate) {
        firstUpdate = false;
        return;
    }
    console.log("Update");
    const stateStr = localStorage.getItem(MASA_BLAZOR_SSR_STATE);
    console.log("[Update] stateStr", stateStr);
    if (!stateStr)
        return;
    const state = JSON.parse(stateStr);
    if (!state)
        return;
    console.log("[Update] state", state);
    restoreMain(state.passive.application);
    restoreTheme(state);
    restoreRtl(state);
}
// Called when an enhanced page update removes the script from the page.
function onDispose() {
    console.log("Dispose");
}
function restoreTheme(state) {
    console.log("restoreTheme", state);
    if (typeof state.dark === "boolean") {
        window.BlazorComponent.interop.ssr.setTheme(state.dark);
    }
}
function restoreRtl(state) {
    console.log("restoreRtl", state);
    if (typeof state.rtl === "boolean") {
        window.BlazorComponent.interop.ssr.setRtl(state.rtl);
    }
}

export { onDispose, onLoad, onUpdate };
//# sourceMappingURL=ssr-page-script.js.map
