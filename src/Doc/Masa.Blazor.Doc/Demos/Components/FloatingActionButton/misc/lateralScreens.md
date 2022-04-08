---
order: 11
title:
  zh-CN: 横向屏幕切换
  en-US: Lateral screens
---

## zh-CN

当更改按钮的默认操作时，建议您显示一个过度来表示更改。 

## en-US

When changing the default action of your button, it is recommended that you display a transition to signify a change. We do this by binding the key prop to a piece of data that can properly signal a change in action to the Vue transition system. While you can use a custom transition for this, ensure that you set the mode prop to out-in.
