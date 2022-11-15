import*as i from"echarts";function n(n,t,o){const e=i.init(n,t,o);window.onresize=function(){e.resize()}}function t(i,n,t=!1,o=!1){i.setOption(n,t,o)}function o(i){i.dispose()}function e(i,n,t){i.resize({width:n,height:t})}export{o as dispose,n as init,e as resize,t as setOption};
//# sourceMappingURL=echarts-proxy.js.map
