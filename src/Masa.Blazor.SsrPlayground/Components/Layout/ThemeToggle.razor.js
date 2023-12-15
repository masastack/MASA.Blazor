function toggleTheme() {
  window.BlazorComponent.interop.ssr.toggleTheme();
  upsertThemeIcon();
}

function upsertThemeIcon() {
  const app = document.querySelector('.m-application');
  const isDark = app.classList.contains("theme--dark");
  const themeToggleIcon = document.querySelector('.theme-toggle__icon');
  if (themeToggleIcon) {
    if (!themeToggleIcon.classList.contains('mdi')) {
      themeToggleIcon.classList.add('mdi');
    }
    themeToggleIcon.classList.remove(isDark ? 'mdi-weather-sunny' : 'mdi-weather-night');
    themeToggleIcon.classList.add(isDark ? 'mdi-weather-night' : 'mdi-weather-sunny');
  }
}

export function onLoad() {
  upsertThemeIcon();

  const themeToggleBtn = document.querySelector('.theme-toggle__btn')
  if (themeToggleBtn) {
    themeToggleBtn.addEventListener('click', toggleTheme)
  }
}

export function onUpdate() {
  upsertThemeIcon();
}

export function onDispose() {
  const themToggleBtn = document.querySelector('.theme-toggle__btn')
  themToggleBtn.removeEventListener('click', toggleTheme)
}