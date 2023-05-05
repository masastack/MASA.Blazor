---
title: Date pickers month（月份选择器）
desc: "**MDatePicker**  可以用作一个独立的月份选择器组件。"
related:
  - /blazor/components/date-pickers
  - /blazor/components/menus
  - /blazor/components/time-pickers
---

## 使用

月份选择器有两种方向变化：纵向 (默认) 和横向。

<masa-example file="Examples.components.date_pickers_month.Usage"></masa-example>

## 注意

<app-alert type="warning" content="**MDatePicker** 接受ISO 8601 **日期** 字符串(YYYY-MM-DD)。 有关 ISO 8601 和其他标准的更多信息，请访问 ISO（国际标准化组织） [国际标准](https://www.iso.org/standards.html) 官方网页。"></app-alert>

## 示例

### 属性

#### 允许的月份

您可以使用数组、对象或函数指定允许的月份。

<masa-example file="Examples.components.date_pickers_month.AllowedDates"></masa-example>

#### 颜色

可以使用 `Color` 和 `HeaderColor` 设置日期选择器颜色。 如果没有提供 `HeaderColor`, 头部将使用 `Color` 值。

<masa-example file="Examples.components.date_pickers_month.Colors"></masa-example>

#### 图标

使用 `Icons`属性，您可以覆盖选择器中使用的默认图标。

<masa-example file="Examples.components.date_pickers_month.Icons"></masa-example>

#### 多选

日期选择器可以使用 `Multiple` 选择多个日期。 如果使用 `Multiple` 属性，日期选择器就会要求 `@bind-Value` 是一个数组。

<masa-example file="Examples.components.date_pickers_month.Multiple"></masa-example>

#### 只读

使用 `Readonly`属性，来禁用选择新日期。

<masa-example file="Examples.components.date_pickers_month.Readonly"></masa-example>

#### 显示当前月份

默认情况下，当前日期使用轮廓按钮显示 `ShowCurrent` 属性允许您删除边框或选择不同的日期显示为当前日期。

<masa-example file="Examples.components.date_pickers_month.ShowCurrent"></masa-example>

#### 宽度

使用 `Width`属性，您可以指定选择器的宽度或使其全宽度。

<masa-example file="Examples.components.date_pickers_month.Width"></masa-example>

### 其他

#### 对话框和菜单

将选择器集成到 **MTextField** 中时，建议使用 `Readonly`。 这将防止手机键盘触发。 要节省垂直空间，还可以隐藏选择器标题。

选择器公开了一个插槽，允许您连接到保存和取消功能。这将保留一个旧值，如果用户取消，该值可以被替换。

<masa-example file="Examples.components.date_pickers_month.DialogAndMenu"></masa-example>

#### 国际化(TODO)

日期选择器通过JavaScript日期对象支持国际化。使用区域设置道具指定BCP 47语言标记，然后使用每周第一天属性设置每周第一天。

<masa-example file="Examples.components.date_pickers_month.Internationalization"></masa-example>

#### 方向

使用 `Orientation`属性，控制日期选择器两种方向变化，纵向（默认）和横向。

<masa-example file="Examples.components.date_pickers_month.Orientation"></masa-example>