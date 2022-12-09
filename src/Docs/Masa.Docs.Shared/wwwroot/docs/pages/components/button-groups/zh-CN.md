---
title: Button Groups（按钮组）
desc: "**MButtonGroup** 组件是专门针对 **MButton** 构建的 **MItemGroup** 的简单包装器。"
related:
  - /components/buttons
  - /components/icons
  - /components/selection-controls
---

## 使用

切换按钮允许您创建一个样式化的按钮组，可以使用 `@bind-Value(s)` 下选择或切换。

<button-groups-usage></button-groups-usage>

## 示例

### 属性

#### 必填项

**MButtonGroup** 带有 `Mandatory` 属性将总是有一个（被选中的）值。

<example file="" />

#### 多选

**MButtonGroup** 带有 `Multiple` 属性将允许用户选中多个值并以数组的形式返回。

<example file="" />

#### 圆角

你可以使用 `Rounded` 属性让 **MButtonGroup** 变成圆角样式。

<example file="" />

### 其他

#### 工具栏(TODO:OverflowButton)

可与 **MToolbar** 轻松集成自定义按钮方案。

<example file="" />

#### WYSIWYG/所见即所得

对类似的操作进行分组，并设计自己的 WYSIWYG 组件。

<example file="" />
