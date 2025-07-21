import "driver.js/dist/driver.css";

import { Config, driver, DriveStep } from "driver.js";

class DriverWrapper {
  private driverInstance: ReturnType<typeof driver>;
  private config: Config;

  constructor(config: Config = {}) {
    this.driverInstance = driver(config);
  }

  updateConfig(config: Config, drive: boolean) {
    this.debug("updateConfig", config, drive);
    this.config = config;
    this.driverInstance.setConfig(config);
    if (drive) {
      this.observeElementResizeOnce(0);
      this.driverInstance.drive();
    }
  }

  drive(stepIndex?: number) {
    this.observeElementResizeOnce(stepIndex ?? 0);
    this.debug("drive", stepIndex, this.config);
    this.driverInstance.setConfig(this.config);
    this.driverInstance.drive(stepIndex);
  }

  highlight(step: DriveStep) {
    this.observeElementResizeOnce(step.element);
    this.driverInstance.setConfig(this.config);
    const { popover, ...args } = step;
    this.driverInstance.highlight({
      ...args,
      popover: {
        onNextClick: (el, step, opts) => {
          opts.driver.destroy();
        },
        onCloseClick: (el, step, opts) => {
          opts.driver.destroy();
        },
        ...popover,
      },
    });
  }

  /**
   * Observes the resize event of a specified DOM element once and triggers a refresh on the driver instance.
   * The observation is automatically disconnected after 2 seconds, regardless of whether a resize occurs.
   * This method is useful for ensuring that the driver instance is aware of any changes in the size of the element.
   *
   * @param target - The target element to observe. Can be:
   *   - A number (used as an index to retrieve the element from `this.config.steps`)
   *   - A string (used as a CSS selector to query the element)
   *   - An Element instance
   *   - A function returning an Element
   *
   * If the target element is not found, a debug message is logged and observation is skipped.
   */
  private observeElementResizeOnce(
    target: number | string | Element | (() => Element)
  ) {
    let element: Element;

    if (typeof target === "number") {
      // the element of step is a css selector
      const selector = this.config.steps[target]?.element as string;
      if (!selector) {
        this.debug("No element found for index:", target);
        return;
      }

      element = document.querySelector(selector);
    } else if (typeof target === "string") {
      element = document.querySelector(target);
    } else if (typeof target === "function") {
      element = target();
    } else {
      element = target as Element;
    }

    if (!element) {
      this.debug("No valid target element found for observation.");
      return;
    }

    const observer = new ResizeObserver(() => this.driverInstance.refresh());

    observer.observe(element);

    // whatever the target resize or not, we need to disconnect the observer after a while
    setTimeout(() => {
      observer.disconnect();
    }, 2000);
  }

  private debug(message: string, ...args: any[]) {
    if (
      window.MasaBlazor &&
      window.MasaBlazor.debug &&
      window.MasaBlazor.debug.includes("driverjs")
    ) {
      console.debug(`[Driverjs] ${message}`, ...args);
    }
  }
}

export function init(config: Config) {
  return new DriverWrapper(config);
}
