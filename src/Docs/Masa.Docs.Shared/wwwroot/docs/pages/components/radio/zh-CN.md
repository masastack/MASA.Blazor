---
title: Radio（单选按钮）
desc: "**MRadio** 组件是一个简单的单选按钮。 与 **MRadioGroup** 组件结合时，您可以提供分组的功能，允许用户从一组预定义的选项中进行选择。"
related:
  - /components/button-groups
  - /components/forms
  - /components/checkboxes
---

## 使用

虽然 **MRadio** 可以单独使用，但它最好与 **MRadioGroup** 一起使用。 在 **MRadioGroup** 上使用 @bind-Value，您可以访问组内所选单选按钮的值。

<radio-usage></radio-usage>

## 示例

### 属性

#### 颜色

**MRadio** 可以使用 `Color` 属性设置颜色 颜色可以是[内置颜色](/stylesandanimations/colors)或自定义来着色。

<example file="" />

#### 布局

单选框组可以使用 `column` 或者 `row` 属性设置为一列或者一行的形式。 默认设置为列。

<example file="" />

#### 必填项

默认情况下，按钮组不是必填的。 可以使用 `Mandatory` 属性改变这种状态。

<example file="" />

#### 插槽

### 标签

单选组标签可以在 `LabelContent` 中定义，允许使用 HTML 内容。

<example file="" />