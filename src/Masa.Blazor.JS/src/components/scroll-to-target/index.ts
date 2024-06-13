import { formatToStandardOptions, IntersectionObserverOptions } from "../../mixins/intersect";

class ScrollToTargetJSInterop {
  activeStack: string[] = [];
  handle: DotNet.DotNetObject;
  options?: IntersectionObserverInit | null;

  constructor(
    handle: DotNet.DotNetObject,
    options?: IntersectionObserverOptions
  ) {
    this.handle = handle;
    this.options = formatToStandardOptions(options);
  }

  observe(id: string) {
    const el = document.getElementById(id);
    if (!el) {
      console.warn(`[ScrollToTarget] Element with id '${id}' not found`);
      return;
    }

    if (el["_intersectForScrollToTarget"]) {
      return;
    }

    const observer = new IntersectionObserver(
      async (
        entries: IntersectionObserverEntry[] = [],
        observer: IntersectionObserver
      ) => {
        const isIntersecting = entries.some((e) => e.isIntersecting);
        if (isIntersecting) {
          this.activeStack.push(id);
        } else if (this.activeStack.includes(id)) {
          this.activeStack.splice(this.activeStack.indexOf(id), 1);
        }

        await this.handle.invokeMethodAsync(
          "UpdateActiveTarget",
          this.activeStack[this.activeStack.length - 1]
        );
      },
      this.options
    );

    el["_intersectForScrollToTarget"] = Object(el["_intersectForScrollToTarget"]);
    el["_intersectForScrollToTarget"] = { handle: this.handle, observer };

    observer.observe(el);
  }

  unobserve(id: string) {
    const el = document.getElementById(id);
    if (!el) return;

    const observe = el["_intersectForScrollToTarget"];
    if (!observe) return;

    observe.observer.unobserve(el);
    delete el["_intersectForScrollToTarget"];
  }

  dispose() {
    if (this.handle) {
      this.handle.dispose();
      this.handle = null
    }
  }
}

function init(
  handle: DotNet.DotNetObject,
  options?: IntersectionObserverOptions
) {
  return new ScrollToTargetJSInterop(handle, options);
}

export { init };
