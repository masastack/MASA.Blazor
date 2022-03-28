
export function init(container, option) {
    window.devicePixelRatio = 2

    if (echarts && container) {
        container._chart = echarts.init(container, null, { renderer: 'svg' });
        container._chart.setOption(option, true);

        if (!container._init) {
            container._init = true;
            window.addEventListener('resize', onResize);
        }
    }

    function onResize() {
        if (!container) {
            window.removeEventListener('resize', onResize);
            return;
        }

        window.clearTimeout(container._chart_timer);
        container._chart_timer = window.setTimeout(function () {
            if (container && container._chart) {
                container._chart.resize();
            }
        }, 300);
    };
}

