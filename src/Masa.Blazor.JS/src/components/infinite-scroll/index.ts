import { isWindow } from "utils/helper";

interface JsInteropObject {
  dispose: () => void;
  [key: string]: Function;
}

export function registerInfiniteScrollJSInterop(
  el: HTMLElement,
  container: string,
  threshold: number,
  enable: boolean,
  dotnetHelper: DotNet.DotNetObject
): JsInteropObject {
  if (!el || !container) {
    console.warn(
      "[MInfiniteScroll] Element or container is not provided. Make sure to provide the correct element and container."
    );
    return;
  }

  console.log('enable', enable)

  let containerEl: Element | Window;
  if (container === "window") {
    containerEl = window;
  } else if (container === "document") {
    containerEl = document.documentElement;
  } else {
    containerEl = document.querySelector(container);
  }

  if (!containerEl) {
    console.warn(
      `[MInfiniteScroll] Element with selector '${container}' not found.`
    );
    return;
  }

  containerEl.addEventListener("scroll", onScroll);

  async function onScroll(e: Event) {
    console.log('enable 2', enable)
    if (enable === false) return;

    const rect = el.getBoundingClientRect();
    const elementTop = rect.top;
    const current = isWindow(containerEl)
      ? window.innerHeight
      : containerEl.getBoundingClientRect().bottom;

    if (current >= elementTop - threshold) {
      await dotnetHelper.invokeMethodAsync("OnScrollInternal");
    }
  }

  return {
    updateThreshold: (t: number) => (threshold = t),
    updateEnable: (s: boolean) => (enable = s),
    dispose: () => {
      dotnetHelper.dispose();
      containerEl.removeEventListener("scroll", onScroll);
    },
  };
}
