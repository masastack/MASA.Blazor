---
title: Date pickers month（月份选择器）
desc: "**MDatePicker**  可以用作一个独立的月份选择器组件。"
related:
  - /components/date-pickers
  - /components/menus
  - /components/time-pickers
---

## 使用

月份选择器有两种方向变化：纵向 (默认) 和横向。

<date-pickers-month-usage></date-pickers-month-usage>

## 注意

<!--alert:warning--> 
**MDatePicker** 接受ISO 8601 **日期** 字符串(YYYY-MM-DD)。 有关 ISO 8601 和其他标准的更多信息，请访问 ISO（国际标准化组织） [国际标准](https://www.iso.org/standards.html) 官方网页。

## 示例

### 属性

#### 允许的月份

您可以使用数组、对象或函数指定允许的月份。

<example file="" />

#### 颜色

可以使用 `Color` 和 `HeaderColor` 设置日期选择器颜色。 如果没有提供 `HeaderColor`, 头部将使用 `Color` 值。

<example file="" />

#### 图标

使用 `Icons`属性，您可以覆盖选择器中使用的默认图标。

<example file="" />

#### 多选

日期选择器可以使用 `Multiple` 选择多个日期。 如果使用 `Multiple` ，日期选择器就会要求它的model是一个数组。

<example file="" />

#### 只读

使用 `Readonly`属性，来禁用选择新日期。

<example file="" />

#### 显示当前月份

默认情况下，当前日期是使用边框按钮显示的 - `ShowCurrent` 允许您删除边框或选择其他日期显示为当前日期。

<example file="" />

#### 宽度

使用 `Width`属性，您可以指定选择器的宽度或使其全宽度。

<example file="" />

### 其他

#### 对话框和菜单

将选择器集成到 **MTextField** 中时，建议使用 `Readonly`。 这将防止手机键盘触发。 要节省垂直空间，还可以隐藏选择器标题。

拾取器公开一个允许您挂起保存和取消功能的插槽。 这将保持一个用户取消时可以替换的旧值。

<example file="" />

#### 国际化(TODO)

日期选择器通过JavaScript日期对象支持国际化。使用区域设置道具指定BCP 47语言标记，然后使用每周第一天属性设置每周第一天。

<example file="" />

#### 方向

使用 `Orientation`属性，控制日期选择器两种方向变化，纵向（默认）和横向。

<example file="" />