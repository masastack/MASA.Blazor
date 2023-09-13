# 交叉观察者

**IntersectJSModule** 使用了 [Intersection Observer API](https://developer.mozilla.org/en-US/docs/Web/API/Intersection_Observer_API)。它提供了一个易于使用的接口，用于检测元素是否在用户的视口中可见。这也用于 [MLazy](/blazor/labs/lazy) 组件。

## 使用

滚动窗口并观察彩色的点。注意当 [MCard](/blazor/components/cards) 进入视图时，它从错误变为成功。

<masa-example file="Examples.js_modules.intersection_observer.Usage"></masa-example>
