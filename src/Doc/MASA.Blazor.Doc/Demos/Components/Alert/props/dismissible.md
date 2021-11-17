---
order: 4
title:
  zh-CN: 可关闭
  en-US: Dismissible
---

## zh-CN

**Dismissible** 属性将会在提示框的尾部添加一个关闭按钮。点击此按钮将会将它的值设置为 false 且隐藏提示框。你也能够通过绑定 **@bind-Value** 的值为 true 来恢复提示框。关闭图标会自动应用
`aria-label`，可以通过修改 **CloseLabel** 属性或者改变本地设置 close 的值来进行更改它。

## en-US

The **Dismissible** prop adds a close button to the end of the alert component. Clicking this button will set its value
to false and effectively hide the alert. You can restore the alert by binding **@bind-Value** and setting it to true.
The close icon automatically has an `aria-label` applied that can be changed by modifying the **CloseLabel** prop or changing
close value in your locale.
