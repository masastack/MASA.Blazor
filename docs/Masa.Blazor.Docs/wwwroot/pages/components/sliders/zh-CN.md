---
title: Sliders（滑块）
desc: "**MSlider** 组件是一个更好的可视化数字输入工具。 它用于收集数字数据。"
related:
  - /blazor/components/forms
  - /blazor/components/selects
  - /blazor/components/range-sliders
---

## 使用

滑块沿条反映一系列值，用户可以从中选择单个值。非常适合调整音量、亮度或应用图像过滤器等设置。

<sliders-usage></sliders-usage>

## 示例

### 属性

#### 颜色

您可以使用 `Color`、`TrackColor` 和 `ThumbColor` 属性设置滑块的颜色。

<masa-example file="Examples.components.sliders.Colors"></masa-example>

#### 禁用

您不能与应用了 `Disabled` 属性的滑块交互。

<masa-example file="Examples.components.sliders.Disabled"></masa-example>

#### 离散的

离散滑块提供一个显示当前准确数量的标签。 您可以使用 `Step` 属性让滑块只能选择部分值。

<masa-example file="Examples.components.sliders.Discrete"></masa-example>

#### 图标

您可以使用 `AppendIcon` 和 `PrependIcon` 属性添加图标到滑块。 您可以使用 `OnAppendClick` 和 `OnPrependClick` 绑定图标点击事件回调函数。

<masa-example file="Examples.components.sliders.Icons"></masa-example>

#### 反向标签

具有 `InverseLabel` 属性的 **MSlider** 在其末尾显示标签。

<masa-example file="Examples.components.sliders.InverseLabel"></masa-example>

#### 最小值和最大值

您可以通过 `Min` 和 `Max` 属性设置滑块的最小值和最大值。

<masa-example file="Examples.components.sliders.MinAndMax"></masa-example>

#### 只读

您无法与带有 `Readonly` 属性的滑块进行交互，虽然它们看起来与普通滑块一样。

<masa-example file="Examples.components.sliders.Readonly"></masa-example>

#### 步骤

**MSlider** 组件可以有 1 以外的步骤。这对于一些需要以或多或少的精度调整值的应用程序很有帮助。

<masa-example file="Examples.components.sliders.Step"></masa-example>

#### 缩略图标签

您可以在滑动时或始终使用 `ThumbLabel` 道具显示拇指标签。它可以通过设置 `ThumbColor` 属性来自定义颜色，并使用 `ThumbSize` 属性设置自定义大小。使用 `AlwaysDirty` 道具，它的颜色永远不会改变，即使在 `Min` 值上也是如此。

<masa-example file="Examples.components.sliders.Thumb"></masa-example>

#### 刻度

刻度线表示用户可以将滑块移动到的预定值。

<masa-example file="Examples.components.sliders.Ticks"></masa-example>

#### 验证

支持外部验证。

<masa-example file="Examples.components.sliders.Validation"></masa-example>

#### 垂直滑块

您可以使用 `Vertical` 属性将滑块切换为垂直方向。 如果您需要更改滑块的高度，请使用 CSS。

<masa-example file="Examples.components.sliders.VerticalSliders"></masa-example>

### 插槽

#### 附加代码

使用 `PrependContent` 和 `AppendContent` 插槽来轻松自定义 **MSlider** 以便适应任何情况。

<masa-example file="Examples.components.sliders.AppendAndPrepend"></masa-example>

#### 追加文本字段

滑块可以与它的 **AppendContent** 插槽中的其他组件合并，例如 **MTextField**，以便为组件添加额外的功能。

<masa-example file="Examples.components.sliders.AppendTextField"></masa-example>