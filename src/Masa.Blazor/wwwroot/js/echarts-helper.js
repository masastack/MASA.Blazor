let timeout
let echarts_instance_cache = {}

export function init(selector, theme, initOptions, option) {
  let instance = echarts_instance_cache[selector]

  if (instance && instance.isDisposed() === false) return;

  const container = document.querySelector(selector)

  if (echarts && container) {
    if (initOptions && initOptions.renderer) {
      initOptions.renderer = initOptions.renderer.toLowerCase()
    }

    instance = echarts.init(container, theme, initOptions);

    echarts_instance_cache[selector] = instance

    instance.setOption(option, true);

    window.addEventListener('resize', () => onResize(instance));
  }
}

export function dispose(selector) {
  const instance = echarts_instance_cache[selector]

  if (instance && instance.dispose) {
    instance.dispose();
    window.removeEventListener('resize', () => onResize(instance))
  }
}

export function setOption(selector, option) {
  const instance = echarts_instance_cache[selector]
  if (instance) {
    instance.setOption(option, true);
  }
}

function onResize(instance) {
  window.clearTimeout(timeout);
  timeout = window.setTimeout(function () {
    if (instance && instance.resize) {
      instance.resize();
    }
  }, 300);
}