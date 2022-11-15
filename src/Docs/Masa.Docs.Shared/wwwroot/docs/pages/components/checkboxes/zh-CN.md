---
title: Checkboxes（复选框）
desc: "**MCheckbox** 组件使用户能够在两个不同的值之间进行选择。它们非常类似于开关，可以用在复杂的表单和检查表中。"
related:
  - /components/switches
  - /components/forms
  - /components/text-fields
---

## 使用

最简单形式的 **Checkbox** 提供了两个值之间的切换。

<checkboxes-usage></checkboxes-usage>

## 示例

### 属性

#### 颜色

**MCheckbox** 可以通过使用颜色属性的任何内置颜色和上下文名称来着色。

<masa-example file="Examples.checkboxes.Color"></masa-example>

#### 布尔值

单个 **MChecbox** 将有一个布尔值作为其 `value`。

<masa-example file="Examples.checkboxes.Boolean"></masa-example>

#### 状态

**MCheckbox** 可以有不同的状态，例如 `default`, `disabled`, 和 `indeterminate`.

<masa-example file="Examples.checkboxes.States"></masa-example>

### 插槽

#### 标签

**MCheckbox** 文本可以在 **LabelContent** 中定义 - 这将允许使用HTML内容。

<masa-example file="Examples.checkboxes.LabelContent"></masa-example>

### 其他

#### 内联输入文本

您可以将 **MCheckbox** 与其他组件（如 **MTextField** ）对齐。

<masa-example file="Examples.checkboxes.InlineTextField"></masa-example>
