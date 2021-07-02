
export function init(container, option) {
    window.devicePixelRatio = 2

    var chart = echarts.init(container, null, { renderer: 'svg' });
    chart.setOption(option);
}