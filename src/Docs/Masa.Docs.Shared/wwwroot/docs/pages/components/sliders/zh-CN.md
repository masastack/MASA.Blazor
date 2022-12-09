---
title: Sliders（滑块）
desc: "**MSlider** 组件是一个更好的可视化数字输入工具。 它用于收集数字数据。"
related:
  - /components/forms
  - /components/selects
  - /components/range-sliders
---

## 使用

滑块沿条反映一系列值，用户可以从中选择单个值。非常适合调整音量、亮度或应用图像过滤器等设置。

<sliders-usage></sliders-usage>

## 示例

### 属性

#### 颜色

您可以使用 `Color`、`TrackColor` 和 `ThumbColor*` 属性设置滑块的颜色。

<example file="" />

#### 禁用

您不能与 Disabled 滑块交互。

<example file="" />

#### 离散的

离散滑块提供一个显示当前准确数量的标签。 您可以使用 `Step` 属性让滑块只能选择部分值。

<example file="" />

#### 图标

您可以使用 `AppendIcon` 和 `PrependIcon` 属性添加图标到滑块。 您可以使用 `OnAppendClick` 和 `OnPrependClick` 绑定图标点击事件回调函数。

<example file="" />

#### 反向标签

具有 `InverseLabel` 属性的 **MSlider** 在其末尾显示标签。

<example file="" />

#### 最小值和最大值

您可以通过 `Min` 和 `Max` 属性设置滑块的最小值和最大值。

<example file="" />

#### 只读

您无法与带有 `Readonly` 属性的滑块进行交互，虽然它们看起来与普通滑块一样。

<example file="" />

#### 步骤

**MRangeSlider** 组件可以有 1 以外的步骤。这对于一些需要以或多或少的精度调整值的应用程序很有帮助。

<example file="" />

#### 缩略图标签

您可以在滑动时或始终使用 `ThumbLabel` 道具显示拇指标签。它可以通过设置 `ThumbColor` 属性来自定义颜色，并使用 `ThumbSize` 属性设置自定义大小。使用 `AlwaysDirty` 道具，它的颜色永远不会改变，即使在 `Min` 值上也是如此。

<example file="" />

#### 刻度

刻度线表示用户可以将滑块移动到的预定值。

<example file="" />

#### 验证

支持外部验证。

<example file="" />

#### 垂直滑块

您可以使用 `Vertical` 属性将滑块切换为垂直方向。 如果您需要更改滑块的高度，请使用 CSS。

<example file="" />

### 插槽

#### 附加代码

使用 `PrependContent` 和 `AppendContent` 插槽来轻松自定义 **MSlider** 以便适应任何情况。

<example file="" />