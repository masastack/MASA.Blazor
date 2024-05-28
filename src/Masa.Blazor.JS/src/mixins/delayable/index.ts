export default class Delayable {
  openDelay: number;
  closeDelay: number;
  dotNetHelper: DotNet.DotNetObject;

  openTimeout;
  closeTimeout;
  isActive: boolean;

  constructor(
    openDelay: number,
    closeDelay: number,
    dotNetHelper: DotNet.DotNetObject
  ) {
    this.openDelay = openDelay;
    this.closeDelay = closeDelay;
    this.dotNetHelper = dotNetHelper;
  }

  clearDelay() {
    clearTimeout(this.openTimeout);
    clearTimeout(this.closeTimeout);
  }

  runDelay(type: "open" | "close", cb?: () => void) {
    this.clearDelay();

    const delay = parseInt((this as any)[`${type}Delay`], 10);

    (this as any)[`${type}Timeout`] = setTimeout(
      cb ||
        (() => {
          const isActive = { open: true, close: false }[type];
          this.setActive(isActive);
        }),
      delay
    );
  }

  setActive(active: boolean) {
    if (this.isActive == active) {
      return;
    }

    this.isActive = active;
    this.dotNetHelper.invokeMethodAsync("SetActive", this.isActive);
  }

  resetDelay(openDelay: number, closeDelay: number) {
    this.openDelay = openDelay;
    this.closeDelay = closeDelay;
  }
}
