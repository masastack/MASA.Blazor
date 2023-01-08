---
title: Dividers（分隔线）
desc: "**MDivider** 组件用于分隔列表或布局的各个部分。"
related:
  - /blazor/components/lists
  - /blazor/components/navigation-drawers
  - /blazor/components/toolbars
---

## 使用

最简单的分隔线是显示一条水平线。

<dividers-usage></dividers-usage>

## 示例

### 属性

#### 缩进

`Inset` 属性令分隔线向右缩进72px。 这将使他们与列表项保持一致。

<masa-example file="Examples.components.dividers.Inset"></masa-example>

#### 垂直

垂直分隔线为您提供了更多用于独特布局的工具。

<masa-example file="Examples.components.dividers.Vertical"></masa-example>

### 插槽

#### ChildContent

<masa-example file="Examples.components.dividers.ChildContent"></masa-example>

### 其他

#### 纵向视图

创建自定义卡片以适应任何用例。

<masa-example file="Examples.components.dividers.View"></masa-example>

#### 副标题

分割线和副标题可以帮助分解内容，并可以使用相同的 `Inset` 属性来相互对齐。

<masa-example file="Examples.components.dividers.SubHeaders"></masa-example>

