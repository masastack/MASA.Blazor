---
title: Buttons（按钮）
desc: "**MButton**（按钮）组件采 Material Design 设计主题风格，并增加众多的配置选项替换了标准的 html 按钮。 任何颜色助手类都可以用来改变背景或文字颜色。"
related:
  - /blazor/components/button-groups
  - /blazor/components/icons
  - /blazor/components/floating-action-buttons
---

## 使用

最简单的按钮包含大写文本、少许的海拔、悬停效果和单击时的波纹效果。

<buttons-usage></buttons-usage>

## 组件结构解剖

建议在 `MButton` 内部放置元素：

* 将文本放在中心
* 在容器文本周围放置可视内容

![Button Anatomy](https://cdn.masastack.com/stack/doc/masablazor/anatomy/btn-anatomy.png)

| 元素 / 区域 | 描述 |
| - | - |
| 1. 容器 | 除了文本，Button容器通常还包含 [MIcon](blazor/components/icons/) 组件 |
| 2. 图标（可选） | 旨在改善视觉环境的主流媒体内容 |
| 3. 文本 | 用于显示文本和其他内联元素的内容区域 |

## 注意

<app-alert type="warning" content="当使用 `Dark` 属性时，**MButton** 是唯一一种拥有不同行为的组件。 通常来说，组件使用 `Dark` 属性来表示他们将有深色背景，文本也需要是白色的。 虽然这对
**MButton** 也是起作用的，但由于禁用状态与白色背景容易造成混淆，建议仅在按钮为彩色背景时使用此属性。"></app-alert>

## 示例

### 属性

#### 块状

添加 `Block` 属性的按钮将延占满可用的宽度。

<masa-example file="Examples.components.buttons.Block"></masa-example>

#### 凹陷

应用 `Depressed` 属性的按钮依然保持其背景色，但没有框阴影。

<masa-example file="Examples.components.buttons.Depressed"></masa-example>

#### 浮动

浮动按钮是圆形的，通常包含一个图标。

<masa-example file="Examples.components.buttons.Floating"></masa-example>

#### 图标

图标可作为按钮的主要内容。此属性使按钮变圆并应用 `Text` 属性样式。

<masa-example file="Examples.components.buttons.Icon"></masa-example>

#### 加载器

使用 `Loaders` 属性，可以通知用户正在进行处理。默认行为是使用 **MProgressCircular** 组件，但这可以自定义。

<masa-example file="Examples.components.buttons.Loaders"></masa-example>

#### 轮廓

添加 `Outlined` 属性，按钮的边框颜色将继承自当前应用的按钮颜色。

<masa-example file="Examples.components.buttons.Outlined"></masa-example>

#### Plain(朴实)

应用 `Plain` 属性， 按钮将会有较低的基准不透明度, 以响应 `hover` (悬停) 和 `focus`(聚焦) 事件。

<masa-example file="Examples.components.buttons.Plain"></masa-example>

#### 圆角

应用 `Rounded` 属性的按钮行为与常规按钮相同，但具有圆形边缘。

<masa-example file="Examples.components.buttons.Rounded"></masa-example>

#### 大小

可以使用 `Size` 属性为按钮提供不同的大小调整选项，以适应多种场景。

<masa-example file="Examples.components.buttons.Size"></masa-example>

#### 文本

应用 `Text` 属性的按钮没有框阴影和背景。只在悬停时显示按钮的容器。与 `Color` 属性一起使用时，提供的颜色将应用于按钮文本而不是背景。

<masa-example file="Examples.components.buttons.Text"></masa-example>

#### 方块

应用 `Tile` 属性的按钮行为与常规按钮相同，但没有边框半径。

<masa-example file="Examples.components.buttons.Tile"></masa-example>

### 其他

#### 突起

应用 `Raised` 属性的按钮有一个框阴影，单击时会增加。这是默认样式。

<masa-example file="Examples.components.buttons.Raised"></masa-example>