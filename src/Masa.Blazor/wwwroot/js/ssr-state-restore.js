(function () {
    'use strict';

    const MASA_BLAZOR_SSR_STATE$1 = "masablazor@ssr-state";
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
        const stateStr = localStorage.getItem(MASA_BLAZOR_SSR_STATE$1);
        if (stateStr) {
            const state = JSON.parse(stateStr);
            localStorage.setItem(MASA_BLAZOR_SSR_STATE$1, JSON.stringify(Object.assign(Object.assign({}, state), obj)));
        }
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

    const MASA_BLAZOR_SSR_STATE = "masablazor@ssr-state";
    const stateStr = localStorage.getItem(MASA_BLAZOR_SSR_STATE);
    if (stateStr) {
        const state = JSON.parse(stateStr);
        restoreTheme(state);
        restoreRtl(state);
    }
    // function restoreTheme(state) {
    //   console.log('[restoreTheme] state', state.dark)
    //   if (typeof state.dark === "boolean") {
    //     const elements = document.querySelectorAll("." + getThemeCss(!state.dark));
    //     console.log('elements', elements)
    //     for (let i = 0; i < elements.length; i++) {
    //       elements[i].classList.remove(getThemeCss(!state.dark));
    //       elements[i].classList.add(getThemeCss(state.dark));
    //     }
    //   }
    // }
    // function restoreRtl(state) {
    //   console.log('[restoreRtl] state', state.rtl)
    //   if (typeof state.rtl === "boolean") {
    //     const rootElement = document.querySelector(".m-application");
    //     console.log('rootElement', rootElement)
    //     const rtlCss = "m-application--is-rtl";
    //     if (state.rtl) {
    //       if (!rootElement.classList.contains(rtlCss)) {
    //         rootElement.classList.add(rtlCss);
    //       }
    //     } else {
    //       rootElement.classList.remove(rtlCss);
    //     }
    //   }
    // }
    // function getThemeCss(dark) {
    //   return dark ? "theme--dark" : "theme--light";
    // }

})();
//# sourceMappingURL=ssr-state-restore.js.map
