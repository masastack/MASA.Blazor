---
title: Date pickers（日期选择器）
desc: "**MDatePicker** 是一个功能齐全的日期选择组件, 它让用户选择一个日期或日期范围。"
related:
  - /blazor/components/buttons
  - /blazor/components/text-fields
  - /blazor/components/time-pickers
---

## 使用

日期选择器有两种方向变化，纵向（默认）和横向。 默认情况下，当日期（用于日期选择器）或月份（用于月份选择器）选中时它们将发出`input`事件，但使用`Reactive`，它们甚至可以在单击年/月之后更新模型。

<masa-example file="Examples.components.date_pickers.Usage"></masa-example>

## 注意

<app-alert type="warning" content="**MDatePicker** 接受ISO 8601 **日期** 字符串(YYYY-MM-DD)。 有关 ISO 8601 和其他标准的更多信息，请访问 ISO（国际标准化组织） [国际标准](https://www.iso.org/standards.html) 官方网页。"></app-alert>

## 示例

### 属性

#### 允许的日期

使用`AllowedDates`属性，您可以使用数组、对象和函数指定允许的日期。

<masa-example file="Examples.components.date_pickers.AllowedDates"></masa-example>

#### 颜色

可以使用 `Color` 和 `HeaderColor` 设置日期选择器颜色。 如果没有提供 `HeaderColor`, 头部将使用 `Color` 值。

<masa-example file="Examples.components.date_pickers.Colors"></masa-example>

#### 高度(z轴)

**MDatePicker** 组件支持最高高度(z轴)值为24。
欲了解更多关于高度的信息，请访问官方的 [Material Design elevations](https://material.io/design/environment/elevation.html) 页面。

<masa-example file="Examples.components.date_pickers.Elevation"></masa-example>

#### 图标

使用`Color`属性，您可以覆盖选择器中使用的默认图标。

<masa-example file="Examples.components.date_pickers.Icons"></masa-example>

#### 多选

日期选择器可以使用 `Multiple` 选择多个日期。 如果使用 `Multiple` ，日期选择器就会要求它的model是一个数组。

<masa-example file="Examples.components.date_pickers.Multiple"></masa-example>

#### 选取的日期

您可以使用 `OnPickerDateUpdate` ，它是显示的月份/年份（取决于选择器类型和激活的视图），以便在其更改时执行某些操作。

<masa-example file="Examples.components.date_pickers.PickerDate"></masa-example>

#### 范围

使用 `Range` 选择日期范围。 当使用 `Range` 日期选择器要求其model是长度为2的数组或空数组。

<masa-example file="Examples.components.date_pickers.Range"></masa-example>

#### 只读

可以添加 `Readonly` 来禁用选择新日期。

<masa-example file="Examples.components.date_pickers.Readonly"></masa-example>

#### 显示当前月份

默认情况下，当前日期是使用边框按钮显示的 - `ShowCurrent` 允许您删除边框或选择其他日期显示为当前日期。

<masa-example file="Examples.components.date_pickers.ShowCurrent"></masa-example>

#### 显示相邻月份

默认情况下，上个月和下个月的天数不可见。它们可以使用 `ShowAdjacentMonths` 属性显示。

<masa-example file="Examples.components.date_pickers.ShowSiblingMonths"></masa-example>

#### 宽度

使用 `Width` 属性，您可以指定选择器的宽度或使其全宽度。

<masa-example file="Examples.components.date_pickers.Width"></masa-example>

### 事件

#### 日期事件

您可以使用数组或函数指定事件。要更改事件的默认颜色，请使用`EventColor`参数。如果您想显示多个事件指示器，您的`Events`函数可以返回一个颜色数组(material或css)。

<masa-example file="Examples.components.date_pickers.DateEvents"></masa-example>

### 其他

#### 激活的选择器

您可以创建一个生日选择器-默认情况下从年份选择器开始，限制日期范围，并在选择日期后关闭选择器菜单，使之成为完美的生日选择器。

<masa-example file="Examples.components.date_pickers.ActivePicker"></masa-example>

#### 对话框和菜单

将选择器集成到 **MTextField** 中时，建议使用 `Readonly`。 这将防止手机键盘触发。 要节省垂直空间，还可以隐藏选择器标题。

拾取器公开一个允许您挂起保存和取消功能的插槽。 这将保持一个用户取消时可以替换的旧值。

<masa-example file="Examples.components.date_pickers.DialogAndMenu"></masa-example>

#### 自定义格式

如果需要以自定义格式（不同于YYYY-MM-DD）显示日期，则需要使用 `Formatting` 属性格式化功能。

<masa-example file="Examples.components.date_pickers.Formatting"></masa-example>

#### 国际化

使用 **Locale** 设置一个[BCP 47](https://tools.ietf.org/html/bcp47)语言标记，并且使用 **FirstDayOfWeek** 设置一周的第一天。

<masa-example file="Examples.components.date_pickers.Internationalization"></masa-example>

#### 方向

日期选择器有两种方向变化，纵向（默认）和横向。

<masa-example file="Examples.components.date_pickers.Orientation"></masa-example>