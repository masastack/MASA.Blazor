export function onLoad() {
  resize();

  window.addEventListener("resize", resize);

  addDrawerToggleEvent();
}

export function onUpdate() {
  window.removeEventListener('click', outsideClickFn)

  resize();
}

export function onDispose() {
  window.removeEventListener("resize", resize);

  const navIcon = document.querySelector(".m-app-bar--app .m-app-bar__nav-icon")
  if (navIcon) {
    navIcon.removeEventListener('click', toggleDrawer);
  }
}

const openCss = "m-navigation-drawer--open";
const closeCss = "m-navigation-drawer--close";
const isMobileCss = "m-navigation-drawer--is-mobile";
const temporaryCss = "m-navigation-drawer--temporary";

function addDrawerToggleEvent() {
  const navIcon = document.querySelector(".m-app-bar--app .m-app-bar__nav-icon")
  if (navIcon) {
    navIcon.addEventListener('click', toggleDrawer);
  }
}

function resize() {
  const mobileBreakpoint = getComputedStyle(document.documentElement).getPropertyValue("--mobile-breakpoint");

  const appNav = document.querySelector(".m-navigation-drawer--app");

  if (!appNav || appNav.classList.contains(temporaryCss)) {
    return;
  }

  if (window.innerWidth < parseInt(mobileBreakpoint)) {
    if (appNav.classList.contains(openCss)) {
      if (appNav.classList.contains(isMobileCss)) {
        return;
      }
      appNav.classList.add(isMobileCss);
      appNav.classList.replace(openCss, closeCss);
    }
  } else {
    if (appNav.classList.contains(isMobileCss)) {
      appNav.classList.remove(isMobileCss);
      appNav.classList.replace(closeCss, openCss);
    }
  }
}

function outsideClickFn(e) {
  const appNav = document.querySelector(".m-navigation-drawer--app");

  if (!appNav || appNav.contains(e.target)) {
    return;
  }

  appNav.classList.replace(openCss, closeCss);

  window.removeEventListener('click', outsideClickFn)
}

function toggleDrawer() {
  const appNav = document.querySelector(".m-navigation-drawer--app");
  if (!appNav) return

  if (appNav.classList.contains(openCss)) {
    appNav.classList.replace(openCss, closeCss);
  } else {
    // open navigation drawer
    appNav.classList.replace(closeCss, openCss);

    if (appNav.classList.contains(isMobileCss) || appNav.classList.contains(temporaryCss)) {
      setTimeout(() => {
        console.log('addEventListener outside click')
        window.addEventListener("click", outsideClickFn)
      }, 0)
    }
  }
}
