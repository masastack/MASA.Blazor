import SwiperClass from "swiper";
import { SwiperOptions } from "swiper/types/swiper-options";

declare const Swiper: SwiperClass;

class SwiperProxy {
  handle: DotNet.DotNetObject;
  swiper: SwiperClass;

  // This flag is used to solve the issue where Blazor has not finished rendering when calling the update method,
  // resulting in no UI changes after the update. Previously, it required a manual delay(await Task.Delay(1000);) before
  // calling the update method to resolve this issue.
  updateAgainInSlidesUpdatedEvent: boolean;

  constructor(
    el: HTMLElement,
    swiperOptions: SwiperOptions,
    handle: DotNet.DotNetObject
  ) {
    if (!handle) {
      throw new Error("the handle from .NET cannot be null");
    }

    if (!el) {
      handle.dispose();
      return;
    }

    if (el._swiper) {
      el._swiper.instance.destroy(true);
      delete el._swiper;
    }

    swiperOptions ??= {};

    if (swiperOptions.pagination) {
      swiperOptions.pagination["type"] =
        swiperOptions.pagination["type"].toLowerCase();
    }

    const swiper = new (Swiper as any)(el, swiperOptions);
    this.swiper = swiper;
    this.handle = handle;
    this.swiper.on("activeIndexChange", e => this.onRealIndexChange(e, this));
    this.swiper.on("slidesUpdated", e => {
      if (this.updateAgainInSlidesUpdatedEvent) {
        this.updateAgainInSlidesUpdatedEvent = false;
        this.swiper.update();
      }
    });

    el._swiper = {
      instance: this.swiper,
      handle: handle,
    };
  }

  slideTo(index: number, speed?: number, runCallbacks?: boolean) {
    this.swiper.slideToLoop(index, speed, runCallbacks);
  }

  slideNext(speed?: number) {
    this.swiper.slideNext(speed);
  }

  slidePrev(speed?: number) {
    this.swiper.slidePrev(speed);
  }

  update() {
    this.updateAgainInSlidesUpdatedEvent = true;
    this.swiper.update();
  }

  dispose() {
    this.swiper && this.swiper.destroy(true);
    this.handle.dispose();
  }

  invokeVoid(prop: string, ...args: any[]) {
    if (this.swiper[prop] && typeof this.swiper[prop] === "function") {
      this.swiper[prop](...args);
    }
  }

  async onRealIndexChange(e: SwiperClass, that: SwiperProxy) {
    if (that.handle) {
      await that.handle.invokeMethodAsync(
        "OnIndexChanged",
        e.originalParams.loop ? e.realIndex : e.activeIndex
      );
    }
  }
}

function init(
  el: HTMLElement,
  swiperOptions: SwiperOptions,
  dotnetHelper: DotNet.DotNetObject
) {
  return new SwiperProxy(el, swiperOptions, dotnetHelper);
}

export { init };
