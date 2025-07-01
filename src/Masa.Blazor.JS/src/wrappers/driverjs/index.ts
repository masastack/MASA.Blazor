import "driver.js/dist/driver.css";

import { Config, driver, DriveStep } from "driver.js";

export function init() {
  return {
    create(config: Config = {}) {
      const driverObj = driver(config);
      driverObj.drive();
      return driverObj;
    },
  };
}

export function drive(config: Config, stepIndex?: number) {
  driver(config).drive(stepIndex);
}

export function highlight(config: Config, step: DriveStep) {
  console.log("highlight step", step);
  const { popover, ...args } = step;
  driver(config).highlight({
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
