export function onLoad() {
  console.log('[MainNav] onLoad');

  resize()

  window.addEventListener('resize', resize)

  document.querySelector('.app-nav-toggle').addEventListener('click', toggleDrawer);

  function toggleDrawer() {
    // TODO: 需要维护一个状态！
    
    const nav = document.querySelector('.app-nav-drawer');
    if (nav.classList.contains('m-navigation-drawer--open')) {
      nav.classList.replace('m-navigation-drawer--open', 'm-navigation-drawer--close')
    } else {
      nav.classList.replace('m-navigation-drawer--close', 'm-navigation-drawer--open')
    }
  }
}

export function onUpdate() {
  console.log('[MainNav] onUpdate');

  resize();
}

export function onDispose() {
  console.log('[MainNav] onDispose');
}

function resize() {
  const mobileBreakpoint = getComputedStyle(document.documentElement).getPropertyValue('--mobile-breakpoint');
  console.log('mobileBreakpoint', mobileBreakpoint);
  const appNav = document.querySelector('.m-navigation-drawer.m-navigation-drawer--app');
  console.log('appNav', appNav)
  if (window.innerWidth < parseInt(mobileBreakpoint)) {
    if (appNav.classList.contains('m-navigation-drawer--open'))
      appNav.classList.add('m-navigation-drawer--is-mobile');
    appNav.classList.replace('m-navigation-drawer--open', 'm-navigation-drawer--close');
  } else {
    if (appNav.classList.contains('m-navigation-drawer--is-mobile'))
      appNav.classList.remove('m-navigation-drawer--is-mobile');
    appNav.classList.replace('m-navigation-drawer--close', 'm-navigation-drawer--open');

  }
}