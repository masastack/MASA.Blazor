---
title: Floating Action Buttons（浮动动作按钮）
desc: "**MButton** 组件可以用作浮动动作按钮。 这提供了一个具有主要行为点的应用程序。 结合 **MSpeedDial** 组件, 您可以创建一组可供用户使用的功能"
related:
  - /components/button
  - /components/icons
  - /stylesandanimations/transitions
---

## 使用

浮动动作按钮可以附加到material上来表示应用程序中的提升操作。 在大多数情况下，将使用默认大小，而 `Small` 变量可用于与大小相似的元素保持连续性。

<floating-action-buttons-usage></floating-action-buttons-usage>

## 示例

### 其他

#### 显示时的动画

当第一次显示时，浮动动作按钮应该在屏幕上显示动画。 这里我们使用 **FabTransition** 。 您也可以使用由Masa Blazor或自己提供的自定义过渡。

<example file="" />

#### 横向屏幕切换

当更改按钮的默认操作时，建议您显示一个过渡来表示更改。 我们通过将`Key`属性绑定到一段数据上，该数据可以正确地向Masa Blazor过度系统发出操作变化的信号。 虽然您可以为此使用自定义转换，但请确保将`Mode`设置为 `OutIn`。

<example file="" />

#### 小型按钮

为了达到更好的视觉效果，我们可以使用小型按钮以适配列表的头像。

<example file="" />

#### 快速拨号

**MSpeedDial** 组件为浮动动作按钮提供了强大的 api，你可以尽情定制你想要的效果。

<example file="" />





