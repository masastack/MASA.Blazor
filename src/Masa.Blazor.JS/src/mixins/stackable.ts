import { getZIndex } from "utils/helper";

export function getActiveZIndex(
  el: Element,
  content: Element,
  isActive: boolean,
  stackExclude: Element[] = [],
  stackMinZIndex = 0
) {
  if (typeof window === "undefined") return 0;

  // Return current zindex if not active

  const index = !isActive
    ? getZIndex(content)
    : getMaxZIndex(el, stackExclude || [content], stackMinZIndex) + 2;

  if (index == null) return index;

  // Return max current z-index (excluding self) + 2
  // (2 to leave room for an overlay below, if needed)
  return parseInt(index);
}

function getMaxZIndex(
  el: Element,
  exclude: Element[] = [],
  stackMinZIndex = 0
) {
  const base = el;
  // Start with lowest allowed z-index or z-index of
  // base component's element, whichever is greater
  const zis = [stackMinZIndex, getZIndex(base)];
  // Convert the NodeList to an array to
  // prevent an Edge bug with Symbol.iterator
  // https://github.com/vuetifyjs/vuetify/issues/2146
  const activeElements = [
    ...document.getElementsByClassName("m-menu__content--active"),
    ...document.getElementsByClassName("m-dialog__content--active"),
  ];

  // Get z-index for all active dialogs
  for (let index = 0; index < activeElements.length; index++) {
    if (!exclude.includes(activeElements[index])) {
      zis.push(getZIndex(activeElements[index]));
    }
  }

  return Math.max(...zis);
}
