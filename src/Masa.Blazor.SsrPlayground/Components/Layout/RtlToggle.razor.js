let isRtl;
const localStorageKey = 'masablazor1.3.0@direction';

export function onLoad() {
  const direction = localStorage.getItem(localStorageKey);
  if (direction) {
    isRtl = direction === 'rtl';
  } else {
    const app = document.querySelector('.m-application');
    isRtl = app.classList.contains("m-application--is-rtl");
    updateLocalStorage();
  }

  upsertIcon();

  const rtlToggleBtn = document.querySelector('.rtl-toggle__btn')
  if (rtlToggleBtn) {
    rtlToggleBtn.addEventListener('click', toggleRtl)
  }
}

export function onUpdate() {
  updateDom();
  upsertIcon();
}

export function onDispose() {
  const rtlToggleBtn = document.querySelector('.rtl-toggle__btn')
  rtlToggleBtn.removeEventListener('click', toggleRtl)
}

function toggleRtl() {
  isRtl = !isRtl;
  updateDom();
  upsertIcon();
  updateLocalStorage();
}

function updateDom() {
  const app = document.querySelector('.m-application');
  if (isRtl) {
    app.classList.remove('m-application--is-ltr');
    app.classList.add("m-application--is-rtl");
  } else {
    app.classList.remove("m-application--is-rtl");
    app.classList.add('m-application--is-ltr');
  }
}

function upsertIcon() {
  const app = document.querySelector('.m-application');
  const isRtl = app.classList.contains("m-application--is-rtl");
  const rtlToggleIcon = document.querySelector('.rtl-toggle__icon');
  if (rtlToggleIcon) {
    if (!rtlToggleIcon.classList.contains('mdi')) {
      rtlToggleIcon.classList.add('mdi');
    }
    rtlToggleIcon.classList.remove(isRtl ? 'mdi-format-pilcrow-arrow-right' : 'mdi-format-pilcrow-arrow-left');
    rtlToggleIcon.classList.add(isRtl ? 'mdi-format-pilcrow-arrow-left' : 'mdi-format-pilcrow-arrow-right');
  }
}

function updateLocalStorage() {
  localStorage.setItem(localStorageKey, isRtl ? 'rtl' : 'ltr')
}