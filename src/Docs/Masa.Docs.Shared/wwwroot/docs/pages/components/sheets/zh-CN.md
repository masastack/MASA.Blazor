---
title: Sheets（工作表）
desc: "**MSheet** 组件是许多组件的基础，如 [MCard](/components/cards), [MToolbar](/components/toolbars)等等。 可用属性是Material Design的基础 - 纸和立面（阴影）的概念。"
related:
  - /components/cards
  - /components/grid-system
  - /stylesandanimations/elevation
---

## 使用

**MSheet** 组件是一块可改变的 paper ，为 **MASA Blazor** 的功能提供了基础。 
例如，`rounded` 和 `shape` 等属性会修改 `border-radius` 属性，而 `elevation` 会增加/减少 `box-shadow`。

<sheets-usage></sheets-usage>

## 示例

### 属性

#### 颜色

纸张和基于它们的部件可以有不同的尺寸和颜色。

**MSheet** 组件接受自定义[Material Design Color](/stylesandanimations/colors)值，例如 `warning`、`amber darken-3`
和 `deep-purple accent`，以及 *rgb*、*rgba*和*十六进制*值。

<example file="" />

#### 高度(z轴)

纸张和基于它们的部件可以有不同的尺寸和颜色。

**MSheet** 组件接受一个介于 **0(默认值) 至 24** 之间的自定义高度(z轴)。 _elevation_ 属性修改 `box-shadow` 属性。 更多信息位于
MD [Elevation Design Specification](https://material.io/design/environment/elevation.html)。

<example file="" />

#### 圆角

纸张和基于它们的部件可以有不同的尺寸和颜色。

`Rounded` 增加了默认 _4px_ 的 `border-radius`。 默认情况下， **MSheet** 组件没有边框半径。 通过提供自定义的圆角值来定义半径的大小和位置；例如，圆角值 _tr-xl_ _l-pill_ 等于
_rounded-tr-xl_ _rounded-l-pill_。 关于圆角样式的其他信息在 [Border Radius](/stylesandanimations/border-radius) 页。

<example file="" />
