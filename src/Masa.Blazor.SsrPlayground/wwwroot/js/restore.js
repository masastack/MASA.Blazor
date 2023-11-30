const MASA_BLAZOR_SSR_STATE = "masablazor@ssr-state";

const stateStr = localStorage.getItem(MASA_BLAZOR_SSR_STATE);
if (stateStr) {
  const state = JSON.parse(stateStr);

  restoreTheme(state);
  restoreRtl(state);
}

function restoreTheme(state) {
  console.log('[restoreTheme] state', state.dark)
  if (typeof state.dark === "boolean") {
    const elements = document.querySelectorAll("." + getThemeCss(!state.dark));
    console.log('elements', elements)
    for (let i = 0; i < elements.length; i++) {
      elements[i].classList.remove(getThemeCss(!state.dark));
      elements[i].classList.add(getThemeCss(state.dark));
    }
  }
}

function restoreRtl(state) {
  console.log('[restoreRtl] state', state.rtl)
  if (typeof state.rtl === "boolean") {
    const rootElement = document.querySelector(".m-application");
    console.log('rootElement', rootElement)
    const rtlCss = "m-application--is-rtl";
    if (state.rtl) {
      if (!rootElement.classList.contains(rtlCss)) {
        rootElement.classList.add(rtlCss);
      }
    } else {
      rootElement.classList.remove(rtlCss);
    }
  }
}

function getThemeCss(dark) {
  return dark ? "theme--dark" : "theme--light";
}
