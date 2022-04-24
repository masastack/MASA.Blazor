---
order: 11
title:
  zh-CN: 横向屏幕切换
  en-US: Lateral screens
---

## zh-CN

当更改按钮的默认操作时，建议您显示一个过度来表示更改。 我们通过将`Key`属性绑定到一段数据上，该数据可以正确地向Masa Blazor过度系统发出操作变化的信号。 虽然您可以为此使用自定义转换，但请确保将`Mode`设置为 `OutIn`。

## en-US

When changing the default action of your button, it is recommended that you display a transition to signify a change. We do this by binding the `Key` prop to a piece of data that can properly signal a change in action to the Masa Blazor transition system. While you can use a custom transition for this, ensure that you set the `Mode` prop to `OutIn`.
