---
title: Icons（图标）
desc: "**MIcon** 组件提供了一系列用于为您的应用程序的各个方面提供上下文的胶卷。 对于所有可用图标的列表, 请访问官方的 [Material Design 图标](https://materialdesignicons.com/) 页面。要使用任何这些图标，只需使用 `mdi-` 前缀，然后是图标名称。"
related:
  - /features/icon-fonts
  - /components/buttons
  - /components/cards
---

## 使用

图标有两个主题（浅色和深色）和五种不同的尺寸（`x-small`、`small`、`medium`（默认）、`large` 和 `x-large`）。

## 示例

### 属性

#### 颜色

使用色彩辅助器，设置`Color`属性您可以改变标准的暗黑和明亮的主题的图标的颜色。

<example file="" />

### 事件

#### 单击

设置`Click`属性可以将任何单击事件绑定到 **MIcon** 将自动将光标更改为指针。

<example file="" />

### 其他

#### 按钮

图标可以在按钮内部使用，以增加动作的重点。

<example file="" />

#### Font Awesome

同样支持 [Font Awesome](https://fontawesome.com/icons/)。 只需使用 `fa-` 预定图标名称。 请注意，您仍然需要在您的项目中包含Font Awesome 图标。

<example file="" />

#### Material Design

[Material Design](https://material.io/tools/icons/?style=baseline) 也是支持的。

<example file="" />

#### SVG

如果您想要在 **MIcon** 组件中使用SVG图标，仅支持传入SVG的 `path`。

<example file="" />