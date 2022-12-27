---
title: Textareas（多行文本框）
desc: "多行文本框组件用于收集大量文本数据。"
related:
  - /blazor/components/forms
  - /blazor/components/selects
  - /blazor/components/text-fields
---

## 使用

**MTextarea** 最简单的形式是多行文本字段，对于大量文本非常有用。

<textareas-usage></textareas-usage>

## 示例

### 属性

#### 自动增长

当使用 `AutoGrow` 属性时，当包含的文本超过其大小时，多行文本框的大小将自动增加。

<masa-example file="Examples.components.textareas.AutoGrow"></masa-example>

#### 背景色

使用 `BackgroundColor` 和 `Color` 属性可以让您可以更好地控制 **MTextarea** 的样式。

<masa-example file="Examples.components.textareas.BackgroundColor"></masa-example>

#### 浏览器自动补全

`Autocomplete` 属性为您提供了使浏览器能够预测用户输入的选项。

<masa-example file="Examples.components.textareas.BrowserAutocomplete"></masa-example>

#### 可清除

您可以使用 `Clearable` 属性从 **MTextarea** 组件中清除文本，并可以使用`ClearableIcon` 属性自定义与 `Clearable` 一起使用的图标。

<masa-example file="Examples.components.textareas.Clearable"></masa-example>

#### 计数器

`Counter` 属性通知用户 **MTextarea** 的字符限制。

<masa-example file="Examples.components.textareas.Counter"></masa-example>

#### 图标

`AppendIcon` 和 `PrependIcon` 属性有助于将上下文添加到 **MTextarea** 。

<masa-example file="Examples.components.textareas.Icon"></masa-example>

#### 禁止缩放

**MTextarea** 可以选择使用 `NoResize` 属性保持相同的大小，而不管其内容的大小。

<masa-example file="Examples.components.textareas.NoResize"></masa-example>

#### 行数

`Rows` 属性允许您定义 **MTextarea** 有多少行，当与 `RowHeight` 属性结合使用时，您可以通过定义行的高度来进一步自定义行。

<masa-example file="Examples.components.textareas.Row"></masa-example>

### 其他

#### 注册表单

使用其他输入样式，您可以创建易于构建和易于使用的令人惊叹的界面。

<masa-example file="Examples.components.textareas.SignupForm"></masa-example>
