import { getEventTarget } from "utils/helper";

export interface IntersectionObserverOptions {
  rootSelector?: string;
  rootMarginLeft?: string;
  rootMarginRight?: string;
  rootMarginTop?: string;
  rootMarginBottom?: string;
  autoRootMargin: "None" | "Top" | "Right" | "Bottom" | "Left";
  threshold: number[];
  once: boolean;
}

function observe(
  el: HTMLElement,
  handle: DotNet.DotNetObject,
  options?: IntersectionObserverOptions
) {
  if (!handle) {
    throw new Error("the handle cannot be null");
  }

  if (!el) {
    handle.dispose();
    return;
  }

  if (el["_intersect"]) {
    return;
  }

  const once = options?.once ?? false;

  const standardOptions = formatToStandardOptions(options);

  const observer = new IntersectionObserver(
    async (
      entries: IntersectionObserverEntry[] = [],
      observer: IntersectionObserver
    ) => {

      const computedEntries = entries.map((entry) => ({
        isIntersecting: entry.isIntersecting,
        target: getEventTarget(entry.target),
      }));

      const isIntersecting = computedEntries.some((e) => e.isIntersecting);

      if (!once || isIntersecting) {
        await handle.invokeMethodAsync("Invoke", {
          isIntersecting,
          entries: computedEntries,
        });
      }

      if (isIntersecting && once) {
        unobserve(el);
      }
    },
    standardOptions
  );

  el["_intersect"] = Object(el["_intersect"]);
  el["_intersect"] = { handle, observer };

  observer.observe(el);
}

function unobserve(el: HTMLElement) {
  if (!el) return;

  const observe = el["_intersect"];
  if (!observe) return;

  observe.observer.unobserve(el);
  observe.handle.dispose();
  delete el["_intersect"];
}

function observeSelector(
  selector: string,
  handle: DotNet.DotNetObject,
  options?: IntersectionObserverOptions
) {

  if (selector) {
    const el = document.querySelector(selector) as HTMLElement;
    el && observe(el, handle, options);
  }
}

function unobserveSelector(selector: string) {
  if (selector) {
    const el = document.querySelector(selector) as HTMLElement;
    el && unobserve(el);
  }
}

export function formatToStandardOptions(
  options?: IntersectionObserverOptions
): IntersectionObserverInit | null {
  if (!options) {
    return null;
  }

  const root: HTMLLIElement = options.rootSelector
    ? document.querySelector(options.rootSelector)
    : null;

  if (options.autoRootMargin !== "None") {
    if (
      options.autoRootMargin === "Top" &&
      options.rootMarginBottom !== "0px"
    ) {
      options.rootMarginTop =
        calcAuto(root, options.rootMarginBottom, false) + "px";
    } else if (
      options.autoRootMargin === "Right" &&
      options.rootMarginLeft !== "0px"
    ) {
      options.rootMarginRight =
        calcAuto(root, options.rootMarginLeft, false) + "px";
    } else if (
      options.autoRootMargin === "Bottom" &&
      options.rootMarginTop !== "0px"
    ) {
      options.rootMarginBottom =
        calcAuto(root, options.rootMarginTop, false) + "px";
    } else if (
      options.autoRootMargin === "Left" &&
      options.rootMarginRight !== "0px"
    ) {
      options.rootMarginLeft =
        calcAuto(root, options.rootMarginRight, false) + "px";
    }
  }

  return {
    rootMargin: `${options.rootMarginTop} ${options.rootMarginRight} ${options.rootMarginBottom} ${options.rootMarginLeft}`,
    root,
    threshold: options.threshold,
  };
}

function calcAuto(container: HTMLElement, margin: string, x: boolean) {
  container = container || document.documentElement;
  const marginValue = parseInt(margin);
  if (isNaN(marginValue)) {
    return 0;
  }

  var clientValue = x ? container.clientWidth : container.clientHeight;

  return Math.abs(marginValue) - clientValue;
}

export { observe, unobserve, observeSelector, unobserveSelector };
