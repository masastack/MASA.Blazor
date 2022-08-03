let instance
let timeout

export function init(container, theme, initOptions, option) {
  if (instance && instance.isDisposed === false) return

  if (echarts && container) {
    if (initOptions && initOptions.renderer) {
      initOptions.renderer = initOptions.renderer.toLowerCase()
    }

    console.log('theme', theme, 'initOptions', initOptions, 'option', option)

    instance = echarts.init(container, theme, initOptions);
    instance.setOption(option, true);

    window.addEventListener('resize', onResize);
  }
}

export function dispose() {
  console.log('start dispose')
  if (instance?.dispose) {
    instance.dispose();
    window.removeEventListener('resize', onResize)
    
    console.log('dispose success')
  }
}

function onResize() {
  window.clearTimeout(timeout);
  timeout = window.setTimeout(function () {
    if (instance && instance.resize) {
      instance.resize();
    }
  }, 300);
}