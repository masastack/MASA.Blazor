---
title: Range sliders（范围滑块）
desc: "**MRangeSlider** 组件是一个更好的可视化数字输入工具。 它用于收集数字数据。"
related:
  - /blazor/components/forms
  - /blazor/components/selects
  - /blazor/components/sliders
---

## 使用

滑块表示一根条上的一系列值，用户可以从中选择一个值。 滑块组件适用于调节音量、亮度，或者图像滤镜的强度。

<range-sliders-usage></range-sliders-usage>

## 示例

### 属性

#### 禁用

您不能与 `Disabled` 滑块交互。

<masa-example file="Examples.components.range_sliders.Disabled"></masa-example>

#### 最大值和最小值

您可以通过  `Min` 和 `Max` 设置滑块的最小值和最大值。

<masa-example file="Examples.components.range_sliders.MinAndMax"></masa-example>

#### 步骤

**MRangeSlider** 组件可以有 1 以外的步骤。这对于一些需要以或多或少的精度调整值的应用程序很有帮助。

<masa-example file="Examples.components.range_sliders.Step"></masa-example>

#### 垂直滑块

您可以使用 `Vertical` 属性将滑块切换为垂直方向。 如果您需要更改滑块的高度，请使用 CSS。

<masa-example file="Examples.components.range_sliders.VerticalSliders"></masa-example>

### 插槽

#### Thumb标签

使用 `TickLabels` 属性和 **ThumbLabelContent**，您可以创建一个非常个性化的解决方案。

<masa-example file="Examples.components.range_sliders.ThumbLabel"></masa-example>