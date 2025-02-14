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

  // const driverObj = driver(config);

  // if (drive) {
  //   driverObj.drive();
  // }

  // return driverObj;
}

export function drive(config: Config, stepIndex?: number) {
  driver(config).drive(stepIndex);
}

export function highlight(config: Config, step: DriveStep) {
  driver(config).highlight(step);
}
