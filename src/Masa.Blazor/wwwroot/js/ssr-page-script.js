const MASA_BLAZOR_SSR_STATE = "masablazor@ssr-state";
function setRtl(rtl, updateCache = true) {
    console.log("[index.ts] setRtl", rtl);
    const app = getApp();
    if (!app)
        return;
    const rtlCss = "m-application--is-rtl";
    if (!rtl) {
        app.classList.remove(rtlCss);
    }
    else if (!app.classList.contains(rtlCss)) {
        app.classList.add(rtlCss);
    }
    if (updateCache) {
        updateStorage({ rtl });
    }
}
function getThemeCss(dark) {
    return dark ? "theme--dark" : "theme--light";
}
function getApp() {
    return document.querySelector(".m-application");
}
function updateStorage(obj) {
    const stateStr = localStorage.getItem(MASA_BLAZOR_SSR_STATE);
    if (stateStr) {
        const state = JSON.parse(stateStr);
        localStorage.setItem(MASA_BLAZOR_SSR_STATE, JSON.stringify(Object.assign(Object.assign({}, state), obj)));
    }
}
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
        const selector = `.${getThemeCss(!state.dark)}:not(.theme--independent)`;
        const elements = document.querySelectorAll(selector);
        for (let i = 0; i < elements.length; i++) {
            elements[i].classList.remove(getThemeCss(!state.dark));
            elements[i].classList.add(getThemeCss(state.dark));
        }
    }
}
function restoreRtl(state) {
    console.log("restoreRtl", state);
    if (typeof state.rtl === "boolean") {
        setRtl(state.rtl, false);
    }
}

export { onDispose, onLoad, onUpdate, restoreRtl, restoreTheme };
//# sourceMappingURL=ssr-page-script.js.map
