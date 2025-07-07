import { ECharts, EChartsResizeOption } from "echarts";

class EChartsProxy {
  instance: ECharts;
  intersectionObserver: IntersectionObserver;
  resizeObserver: ResizeObserver;
  dotNetHelper: DotNet.DotNetObject;

  constructor(
    elOrString: HTMLDivElement | HTMLCanvasElement,
    theme?: string,
    initOptions?: any
  ) {
    this.instance = echarts.init(elOrString, theme, initOptions);

    this.intersectionObserver = new IntersectionObserver((entries) => {
      if (entries.some((e) => e.isIntersecting)) {
        this.instance.resize();
      }
    });

    this.resizeObserver = new ResizeObserver((entries) => {
      this.instance.resize();
    });

    this.intersectionObserver.observe(this.instance.getDom());
    this.resizeObserver.observe(this.instance.getDom());
  }

  setDotNetObjectReference(
    dotNetHelper: DotNet.DotNetObject,
    events: string[]
  ) {
    this.dotNetHelper = dotNetHelper;

    events.forEach((e) => this.#registerEvent(e));
  }

  getOriginInstance() {
    return this.instance;
  }

  setOption(
    option: any,
    notMerge: boolean = false,
    lazyUpdate: boolean = false
  ) {
    this.instance.setOption(option, notMerge, lazyUpdate);
  }

  setJsonOption(
    option: any,
    notMerge: boolean = false,
    lazyUpdate: boolean = false
  ) {
    this.instance.setOption(eval("option=" + option), notMerge, lazyUpdate);
  }

  showLoading(opts?: object){
    this.instance.showLoading('default', opts);
  }
  
  hideLoading(){
    this.instance.hideLoading();
  }

  resize(opts?: EChartsResizeOption) {
    this.instance.resize(opts);
  }

  dispose() {
    if (this.instance.isDisposed()) return;

    this.intersectionObserver.disconnect();

    this.resizeObserver.disconnect();

    this.instance.dispose();
  }

  #registerEvent(eventName: string) {
    this.instance.on(eventName, (params: any) => {
      const {
        componentType,
        seriesType,
        seriesIndex,
        seriesName,
        name,
        dataIndex,
        data,
        dataType,
        value,
        color,
      } = params;

      this.dotNetHelper.invokeMethodAsync(
        "OnEvent",
        eventName,
        eventName === "globalout"
          ? null
          : {
              componentType,
              seriesType,
              seriesIndex,
              seriesName,
              name,
              dataIndex,
              data,
              dataType,
              value: Array.isArray(value) ? value : [value],
              color,
            }
      );
    });
  }
}

function init(elOrString, theme, initOptions) {
  if (!elOrString) return null;
  return new EChartsProxy(elOrString, theme, initOptions);
}

export { init };
