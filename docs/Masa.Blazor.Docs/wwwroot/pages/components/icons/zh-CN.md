---
title: Icons（图标）
desc: "**MIcon** 组件提供了大量字形来为应用程序的各个方面提供上下文。 对有关所有可用图标的列表，请访问官方[Material Design 图标](https://materialdesignicons.com/) 页面。如果要使用这些图标，只需使用 **mdi-** 前缀再加上图标名称即可。"
related:
  - /blazor/features/icon-fonts
  - /blazor/components/buttons
  - /blazor/components/cards
---

## 使用

图标有两个主题（浅色和深色）和五种不同的尺寸（`x-small`、`small`、`medium`（默认）、`large` 和 `x-large`）。

<icons-usage></icons-usage>

## 示例

### 属性

#### 颜色

使用色彩辅助器，设置`Color`属性您可以改变标准的暗黑和明亮的主题的图标的颜色。

<masa-example file="Examples.components.icons.Color"></masa-example>

### 事件

#### 单击

设置`Click`属性可以将任何单击事件绑定到 **MIcon** 将自动将光标更改为指针。

<masa-example file="Examples.components.icons.Click"></masa-example>

### 其他

#### 按钮

可以在按钮内部使用图标来强调操作。

<masa-example file="Examples.components.icons.Button"></masa-example>

#### Font Awesome

同样支持 [Font Awesome](https://fontawesome.com/icons/)。 只需使用 **fa-** 预定图标名称。 请注意，您仍然需要在您的项目中包含 **Font Awesome** 图标。关于如何安装的更多信息，请导航到[安装页面](/blazor/features/icon-fonts#font-awesome-5-icons)。

<masa-example file="Examples.components.icons.FontAwesome"></masa-example>

#### Material Design

[Material Design](https://material.io/tools/icons/?style=baseline) 也是支持的。有关如何安装的更多信息，请浏览[这里](/blazor/features/icon-fonts#material-icons)。

<masa-example file="Examples.components.icons.MaterialDesign"></masa-example>

#### SVG

如果您想要在 **MIcon** 组件中使用SVG图标，仅支持传入SVG的 `path`。多个 `path` 也是支持的。

<masa-example file="Examples.components.icons.Svg"></masa-example>