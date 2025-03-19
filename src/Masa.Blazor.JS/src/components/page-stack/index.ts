import { convertToUnit } from "utils/helper";

const dotnetRefs: { [prop: number]: DotNet.DotNetObject } = {};
const clickHandlers: { [prop: number]: (e: MouseEvent) => void } = {};
const popstateHandlers: { [prop: number]: () => void } = {};
let nextId = 0;

export function attachListener(handle: DotNet.DotNetObject) {
  const id = nextId;
  const clickHandler = (event: MouseEvent) => onDocumentClick(id, event);
  const popstateHandler = () =>
    handle.invokeMethodAsync("Popstate", window.location.pathname);
  clickHandlers[id] = clickHandler;
  popstateHandlers[id] = popstateHandler;
  dotnetRefs[id] = handle;

  document.addEventListener("click", clickHandler);
  window.addEventListener("popstate", popstateHandler);

  return nextId++;
}

async function onDocumentClick(id: number, event: MouseEvent) {
  const dotnet = dotnetRefs[id];
  if (dotnet === null) return;

  const anchor = event.target.closest("a");
  if (!anchor) return;

  const href = anchor.getAttribute("href");
  if (!href) return;

  let strategy = anchor.getAttribute("data-page-stack-strategy");
  strategy = strategy === null ? null : strategy.toLowerCase();
  if (strategy === "" || strategy === "true" || strategy === "push") {
    if (!document.querySelector("[page-stack-id]")) {
      blockScroll();
    }

    await dotnet.invokeMethodAsync("Push", href);
  }
}

export function blockScroll() {
  const doc = document.documentElement;

  if (doc.classList.contains("m-page-stack-scroll-blocked")) {
    return;
  }

  if (doc.scrollLeft === 0 && doc.scrollTop === 0) {
    return;
  }

  doc.style.setProperty("--m-page-stack-scroll-x", convertToUnit(-doc.scrollLeft));
  doc.style.setProperty("--m-page-stack-scroll-y", convertToUnit(-doc.scrollTop));
  doc.classList.add("m-page-stack-scroll-blocked");
}

export function unblockScroll() {
  const doc = document.documentElement;
  if (!doc.classList.contains("m-page-stack-scroll-blocked")) {
    return;
  }

  const x = parseFloat(doc.style.getPropertyValue("--m-page-stack-scroll-x"));
  const y = parseFloat(doc.style.getPropertyValue("--m-page-stack-scroll-y"));

  const scrollBehavior = doc.style.scrollBehavior;

  doc.style.scrollBehavior = "auto";
  doc.style.removeProperty("--m-page-stack-scroll-x");
  doc.style.removeProperty("--m-page-stack-scroll-y");
  doc.classList.remove("m-page-stack-scroll-blocked");

  doc.scrollLeft = -x;
  doc.scrollTop = -y;

  doc.style.scrollBehavior = scrollBehavior;
}

export function detachListener(id: number) {
  const clickHandler = clickHandlers[id];
  if (clickHandler) {
    document.removeEventListener("click", clickHandler);
  }

  const popstateHandler = popstateHandlers[id];
  if (popstateHandler) {
    window.removeEventListener("popstate", popstateHandler);
  }

  dotnetRefs[id] && dotnetRefs[id].dispose();

  delete clickHandlers[id];
  delete popstateHandlers[id];
  delete dotnetRefs[id];
}
