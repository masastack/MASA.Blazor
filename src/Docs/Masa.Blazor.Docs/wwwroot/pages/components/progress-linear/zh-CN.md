---
title: Progress linear（进度条）
desc: "该组件用于将数据环传递给用户。 也可以将其置于不确定状态来描述加载。"
related:
  - /blazor/components/cards
  - /blazor/components/progress-circular
  - /blazor/components/lists
---

## 使用

最简单的形式， **MProgressLinear** 显示一个水平进度条。使用 `Value` 属性来控制进度。

<progress-linear-usage></progress-linear-usage>

## 示例

### 属性

#### 缓冲值

缓冲状态同时表示两个值。 主值由 `@bind-Value` 控制，而缓冲值则由 `BufferValue` 属性控制。

<masa-example file="Examples.components.progress_linear.BufferValue"></masa-example>

#### 颜色

您可以使用 `Color` 和 `BackgroundColor` 属性设置颜色。

<masa-example file="Examples.components.progress_linear.Color"></masa-example>

#### 不定线条

使用 `Indeterminate` 属性，**MProgressLinear** 会保持动画状态。

<masa-example file="Examples.components.progress_linear.Indeterminate"></masa-example>

#### 查询

当 `Query` 设置为 `true` 时，`Query` 属性值由不确定的真实性控制。

<masa-example file="Examples.components.progress_linear.Query"></masa-example>

#### 反转

使用 `Reverse` 属性显示反转的进度条(LTR模式为从右到左，RTL模式为从左到右)。

<masa-example file="Examples.components.progress_linear.Reversed"></masa-example>

#### Rounded（圆角）

`Rounded` 属性是另一种样式，它为 **MProgressLinear** 组件添加了圆角。

<masa-example file="Examples.components.progress_linear.Rounded"></masa-example>

#### 流

`Stream` 属性可以使用 `BufferValue` 向用户表示正在进行一些操作。 您可以使用 `BufferValue` 和 `Value` 的任何组合来实现您的设计。

<masa-example file="Examples.components.progress_linear.Stream"></masa-example>

#### 有条纹的

使用 `Striped` 属性对 **MProgressLinear** 的值部分应用条纹背景。

<masa-example file="Examples.components.progress_linear.Striped"></masa-example>

### 插槽

#### 默认值

**MProgressLinear** 组件将在使用 `@bind-Value` 时响应用户输入。 您可以使用默认插槽或绑定本地model在进度内显示。 如果您在寻找线性组件上的高级功能，请查看 [MSlider](/blazor/components/sliders)。

<masa-example file="Examples.components.progress_linear.Default"></masa-example>

### 其他

#### 定值线条

进度条组件有一个由 `@bind-Value` 修改的确定状态。

<masa-example file="Examples.components.progress_linear.Determinate"></masa-example>

#### 文件加载器

**MProgressLinear** 组件有助于向用户解释他们正在等待响应。

<masa-example file="Examples.components.progress_linear.FileLoader"></masa-example>

#### 工具栏加载器

使用 `Absolute` 属性，我们可以将 **MProgressLinear** 组件定位在 **MToolbar** 的底部。 我们还使用了 `Active` 属性来控制进度条的可见性。

<masa-example file="Examples.components.progress_linear.ToolbarLoader"></masa-example>