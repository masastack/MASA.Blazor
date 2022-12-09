---
title: List Item Groups（列表项目组）
desc: "**MListItemGroup** 提供创建一组可选择的 **MListItem** 的能力。**MListItemGroup** 组件利用其核心的 **MItemGroup** 来为交互式列表提供一个简洁的接口。"
related:
  - /components/lists
  - /components/item-groups
  - /components/cards
---

## 使用

最简单形式的副标题显示带有默认主题的副标题。

<list-item-groups-usage></list-item-groups-usage>

## 示例

### 属性

#### 激活类

您可以设置一个在选择项目时将添加的类。

<example file="" />

#### 必填项

使用`Multiple`属性必须至少选定一项。

<example file="" />

#### 多选

使用`Multiple`属性您可以一次选择多个项目。

<example file="" />

### 其他

#### 扁平列表

您可以一次选择多个项目。

<example file="" />

#### 选择控件

使用默认插槽，您可以访问项目内部状态并进行切换。 因为 `Active` 属性是一个布尔值, 我们使用复选框上的 `IsActive` 属性 将其状态链接到 **MListItem**。

<example file="" />