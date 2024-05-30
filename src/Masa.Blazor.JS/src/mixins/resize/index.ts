import throttle from "just-throttle";

function observe(el: HTMLElement, handle: DotNet.DotNetObject) {
  if (!handle) {
    throw new Error("the handle from .NET cannot be null");
  }

  if (!el) {
    handle.dispose();
    return;
  }

  const throttled = throttle(
    () => {
      if (!handle) return;
      handle.invokeMethodAsync("Invoke");
    },
    300,
    { trailing: true }
  );

  const observer = new ResizeObserver(
    async (entries: ResizeObserverEntry[] = []) => {
      if (!entries.length) return;
      throttled();
    }
  );

  el._resizeObserver = Object(el._resizeObserver);
  el._resizeObserver = { handle, observer };

    observer.observe(el);
}

function unobserve(el: HTMLElement) {
  if (!el) return;

  if (!el._resizeObserver) return;

  el._resizeObserver.observer.unobserve(el);
  el._resizeObserver.handle.dispose();
  delete el._resizeObserver;
}

function observeSelector(
  selector: string,
  handle: DotNet.DotNetObject
) {
  if (selector) {
    const el = document.querySelector(selector) as HTMLElement;
    el && observe(el, handle);
  }
}

function unobserveSelector(selector: string) {
  if (selector) {
    const el = document.querySelector(selector) as HTMLElement;
    el && unobserve(el);
  }
}

export { observe, unobserve, observeSelector, unobserveSelector };
