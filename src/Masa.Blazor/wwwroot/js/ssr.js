const MASA_BLAZOR_SSR_STATE = "masablazor@ssr-state";
let firstUpdate = true;

// Called when the script first gets loaded on the page.
export function onLoad() {
}

// Called when an enhanced page update occurs, plus once immediately after
// the initial load.
export function onUpdate() {
    if (firstUpdate) {
        firstUpdate = false;
        return;
    }

    console.log('Update');

    const stateStr = localStorage.getItem(MASA_BLAZOR_SSR_STATE);
    console.log('[Update] stateStr', stateStr)
    if (stateStr) {
        const state = JSON.parse(stateStr);
        console.log('[Update] state', state)

        restoreMain(state);
        restoreTheme(state);
        restoreRtl(state);
    }
}

// Called when an enhanced page update removes the script from the page.
export function onDispose() {
    console.log('Dispose');
}

function restoreMain(state) {
    const main = document.querySelector('.m-main');
    if (main) {
        main.style.paddingTop = state.main.top + "px";
        main.style.paddingRight = state.main.right + "px";
        main.style.paddingBottom = state.main.bottom + "px";
        main.style.paddingLeft = state.main.left + "px";
    }
}

function restoreTheme(state) {
    console.log('restoreTheme', state)
    if (typeof state.dark === 'boolean') {
        window.BlazorComponent.interop.ssr.setTheme(state.dark);
    }
}

function restoreRtl(state) {
    console.log('restoreRtl', state)
    if (typeof state.rtl === 'boolean') {
        window.BlazorComponent.interop.ssr.setRtl(state.rtl);
    }
}
