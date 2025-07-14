import { get$ParentElement } from "utils/helper";

class OutsideClick {
  dotNetHelper: DotNet.DotNetObject;
  listener: (e: MouseEvent & KeyboardEvent & FocusEvent) => void;
  mousedownListener: (e: MouseEvent) => void;
  excludedSelectors: string[];
  lastMousedownWasOutside: boolean = true;
  lastMousedownTarget: EventTarget | null = null;

  constructor(dotNetHelper: DotNet.DotNetObject, excludedSelectors: string[]) {
    this.dotNetHelper = dotNetHelper;
    this.excludedSelectors = excludedSelectors;
  }

  genListeners() {
    this.listener = e => {
      if (this.checkEvent(e)) return;

      const isSameTarget = this.lastMousedownTarget === e.target;
      // 如果点击目标和 mousedown 目标不同，可能是长按后的遮罩点击
      if (!isSameTarget) {
        return;
      }

      if (this.lastMousedownWasOutside) {
        this.dotNetHelper.invokeMethodAsync("OnOutsideClick");
      }
    };

    this.mousedownListener = e => {
      this.lastMousedownWasOutside = !this.checkEvent(e);
      this.lastMousedownTarget = e.target;
    };
  }

  addListeners() {
    this.genListeners();
    document.addEventListener("click", this.listener, true);
    document.addEventListener("mousedown", this.mousedownListener, true);
  }

  removeListeners() {
    document.removeEventListener("click", this.listener, true);
    document.removeEventListener("mousedown", this.mousedownListener, true);
  }

  resetListener() {
    this.removeListeners();
    this.addListeners();
  }

  updateExcludeSelectors(selectors: string[]) {
    this.excludedSelectors = selectors;
  }

  checkEvent(e: MouseEvent) {
    return this.excludedSelectors.some(selector => {
      const parentElement = get$ParentElement(selector);
      if (parentElement) {
        return parentElement.contains(e.target as HTMLElement);
      }

      const elements = Array.from(document.querySelectorAll(selector));
      return elements.some(el => el.contains(e.target as HTMLElement));
    });
  }

  unbind() {
    this.removeListeners();
  }
}

function init(dotNetHelper: DotNet.DotNetObject, excludeSelectors: string[]) {
  var instance = new OutsideClick(dotNetHelper, excludeSelectors);

  instance.addListeners();

  return instance;
}

export { init };
