let isDark;
const localStorageKey = 'masablazor1.3.0@theme';

export function onLoad() {
  const theme = localStorage.getItem(localStorageKey);
  if (theme) {
    isDark = theme === 'dark';
  } else {
    const app = document.querySelector('.m-application');
    isDark = app.classList.contains("theme--dark");
    updateLocalStorage();
  }

  upsertIcon();

  const themeToggleBtn = document.querySelector('.theme-toggle__btn')
  if (themeToggleBtn) {
    themeToggleBtn.addEventListener('click', toggleTheme)
  }
}

export function onUpdate() {
  upsertIcon();
  updateAllDomTheme();
}

export function onDispose() {
  const themToggleBtn = document.querySelector('.theme-toggle__btn')
  themToggleBtn.removeEventListener('click', toggleTheme)
}

function toggleTheme() {
  isDark = !isDark;
  updateAllDomTheme();
  upsertIcon();
  updateLocalStorage();
}

function upsertIcon() {
  const themeToggleIcon = document.querySelector('.theme-toggle__icon');
  if (themeToggleIcon) {
    if (!themeToggleIcon.classList.contains('mdi')) {
      themeToggleIcon.classList.add('mdi');
    }
    themeToggleIcon.classList.remove(isDark ? 'mdi-weather-sunny' : 'mdi-weather-night');
    themeToggleIcon.classList.add(isDark ? 'mdi-weather-night' : 'mdi-weather-sunny');
  }
}

function updateLocalStorage() {
  localStorage.setItem(localStorageKey, isDark ? 'dark' : 'light')
}

function updateAllDomTheme() {
  const selector = `.${getThemeCss(!isDark)}:not(.theme--independent)`;
  const elements = document.querySelectorAll(selector);
  for (let i = 0; i < elements.length; i++) {
    elements[i].classList.remove(getThemeCss(!isDark));
    elements[i].classList.add(getThemeCss(isDark));
  }
}

function getThemeCss(dark) {
  return dark ? "theme--dark" : "theme--light";
}