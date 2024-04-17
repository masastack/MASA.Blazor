---
title: Snackbars（消息条）
desc: "MSnackbar 组件用于向用户显示快速消息。 Snackbars 支持定位、移除延迟和回调。"
related:
  - /blazor/components/buttons
  - /blazor/styles-and-animations/colors
  - /blazor/components/forms
---

## 使用

**MSnackbar** 以最简单的形式向用户显示一个临时且可关闭的通知。

<masa-example file="Examples.components.snackbars.Usage"></masa-example>

## 示例

### 属性

#### 多行

`MultiLine`属性扩展了 **MSnackbar** 的高度，让您有更多的内容空间。

<masa-example file="Examples.components.snackbars.MultiLine"></masa-example>

#### 超时

`Timeout`属性允许您自定义 **MSnackbar** 隐藏之前的延迟。

<masa-example file="Examples.components.snackbars.Timeout"></masa-example>

#### 变体

使用 `Text`, `Shaped`, `Outlined` 等属性将不同样式应用于 **MSnackbar**。

<masa-example file="Examples.components.snackbars.Variants"></masa-example>

#### 垂直

`Vertical` 属性允许您堆叠 **MSnackbar** 的内容。

<masa-example file="Examples.components.snackbars.Vertical"></masa-example>

### 插槽

#### ActionContent

使用 `ActionContent` 插槽自定义操作按钮。

<masa-example file="Examples.components.snackbars.ActionContent"></masa-example>
