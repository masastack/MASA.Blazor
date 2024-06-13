import SwiperClass from "swiper";
import { SwiperOptions } from "swiper/types/swiper-options";

import { BaseProxy } from "../baseProxy";

declare const Swiper: SwiperClass;

class SwiperProxy extends BaseProxy<SwiperClass> {
  handle: DotNet.DotNetObject;

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
    super(swiper);

    this.handle = handle;
    this.target.on("realIndexChange", (e) => this.onRealIndexChange(e, this));

    el._swiper = {
      instance: this.target,
      handle: handle,
    };
  }

  slideTo(index: number, speed?: number, runCallbacks?: boolean) {
    this.target.slideToLoop(index, speed, runCallbacks);
  }

  slideNext(speed?: number) {
    this.target.slideNext(speed);
  }

  slidePrev(speed?: number) {
    this.target.slidePrev(speed);
  }

  dispose() {
    this.target && this.target.destroy(true);
    this.handle.dispose();
  }

  async onRealIndexChange(e: SwiperClass, that: SwiperProxy) {
    if (that.handle) {
      await that.handle.invokeMethodAsync("OnIndexChanged", e.realIndex);
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
