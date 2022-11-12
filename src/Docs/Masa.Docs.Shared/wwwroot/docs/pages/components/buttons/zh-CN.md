---
title: Buttons（按钮）
desc: "**MButton**（按钮）组件采 Material Design 设计主题风格，并增加众多的配置选项替换了标准的 html 按钮。 任何颜色助手类都可以用来改变背景或文字颜色。"
related:
  - /components/button-groups
  - /components/icons
  - /components/floating-action-buttons
---

## 使用

最简单的按钮包含大写文本、轻微的仰角、悬停效果和单击时的波纹效果。

<buttons-usage></buttons-usage>

## Anatomy

## 注意

<!--alert:warning--> 
当使用 `Dark` 属性时，**MButton** 是唯一一种拥有不同行为的组件。 通常来说，组件使用 `Dark` 属性来表示他们将有深色背景，文本也需要是白色的。 虽然这对
**MButton** 也是起作用的，但由于禁用状态与白色背景容易造成混淆，建议仅在按钮为彩色背景时使用此属性。 If you need white text, simply add the `white--text` class.

## 示例

### 属性

#### 块状

添加 `Block` 属性，按钮将延占满可用的宽度。

<example file="" />

#### 凹陷

`Depressed` 按钮依然保持其背景色，但没有框阴影。

<example file="" />

#### 浮动

浮动按钮是圆形的，通常包含一个图标。

<example file="" />

#### 图标

图标可作为按钮的主要内容。此属性使按钮变圆并应用 `Text` 属性样式。

<example file="" />

#### 加载器

使用加载道具，可以通知用户正在进行处理。默认行为是使用 **MProgressCircular** 组件，但这可以自定义。

<example file="" />

#### 轮廓

添加 `Outlined` 属性，按钮的边框颜色将继承自当前应用的按钮颜色。

<example file="" />

#### Plain(朴实)

应用 `Plain` 属性， 按钮将会有较低的基准不透明度, 以响应 `hover` (悬停) 和 `focus`(聚焦) 事件。

<example file="" />

#### 圆角

`Rounded` 按钮的行为与常规按钮相同，但具有圆形边缘。

<example file="" />

#### 大小

可以为按钮提供不同的大小调整选项，以适应多种场景。

<example file="" />

#### 文本

文本按钮没有框阴影和背景。只在悬停时显示按钮的容器。与 `Color` 属性一起使用时，提供的颜色将应用于按钮文本而不是背景。

<example file="" />

#### 方块

`Tile` 按钮的行为与常规按钮相同，但没有边框半径。

<example file="" />

### 其他

#### 突起

`Raised` 按钮有一个框阴影，单击时会增加。这是默认样式。

<example file="" />