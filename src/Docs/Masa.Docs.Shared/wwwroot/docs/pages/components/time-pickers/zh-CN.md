---
title: Time pickers（时间选择器）
desc: "**MTimePicker** 是独立的组件，可以用于许多现有的 MASA Blazor 组件。 它为用户提供了选择时间的视觉表现。"
related:
  - /components/buttons
  - /components/date-pickers
  - /components/text-fields
---

## 使用

时间选择器默认情况下启用了浅色主题。

<time-pickers-usage></time-pickers-usage>

## 示例

### 属性

#### 允许的时间

您可以使用数组、对象和函数指定允许的时间。 您也可以指定时间步进/精度/间隔 - 例如10分钟。

<example file="" />

#### 标题中的AM/PM

您可以移动 AM/PM 切换到选择器的标题。

<example file="" />

#### 颜色

时间选择器颜色可以使用 `Color` 和 `HeaderColor` 属性设定。如果没有提供 `HeaderColor` 属性, 头部将使用 `Color` 属性值。

<example file="" />

#### 禁用

您无法使用已禁用的选择器。

<example file="" />

#### 高度(z轴)

通过设置 `Elevation` 从 0 到 24 来突出 **MTimePicker** 组件。 高度(z轴)将修改 `box-shadow` css 属性。

<example file="" />

#### 格式化

时间选择器可以切换为24小时格式。 请注意， `Format` 只定义选取器的显示方式，选取器的值 (model) 总是以24小时格式。

<example file="" />

#### 无标题

您可以删除选择器的标题。

<example file="" />

#### 范围

这是一个用 `Min` 和 `Max` 合并选择器的例子。

<example file="" />

#### 只读

使用`Readonly` 属性，设置只读选择器的行为与禁用的一样，但看起来像默认的。

<example file="" />

#### 可滚动

使用`Scrollable` 属性，您可以使用鼠标滚轮编辑时间选择器的值。

<example file="" />

## 使用秒

使用`UseSeconds` 属性，时间选择器可以输入秒数。

<example file="" />

#### 宽度

使用`Width` 属性，您可以指定选择器的宽度或使其为全宽度。

<example file="" />

### 其他

#### 对话框和菜单

由于选择器的灵活性，您可以真正按照自己的意愿进行输入。

<example file="" />