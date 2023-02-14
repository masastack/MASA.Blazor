---
title: Pagination（分页）
desc: "**MPagination** 组件用于分离长数据集，以便用户消化信息。 根据提供的数据量，分页组件将自动缩放。 要维护当前页面，只需提供 Value 属性。"
related:
  - /blazor/components/data-iterators
  - /blazor/components/data-tables
  - /blazor/components/lists
---

## 使用

分页默认根据设置的 `Length` 属性显示页数，两边有 `Prev` 和 `Next` 按钮帮助导航。

<masa-example file="Examples.components.paginations.Usage"></masa-example>

## 示例

### 属性

#### 圆形

`Circle` 属性为你提供了分页按钮的另一种样式。

<masa-example file="Examples.components.paginations.Circle"></masa-example>

#### 禁用

使用 `Disabled` 属性，可以手动禁用分页。

<masa-example file="Examples.components.paginations.Disabled"></masa-example>

#### 图标

上一页和下一页的图标可以通过 `PrevIcon` 和 `NextIcon` 属性自定义。

<masa-example file="Examples.components.paginations.Icon"></masa-example>

#### 长度

使用 `Length` 属性可以设置 **MPagination** 的长度，如果页面按钮的数量超过了父容器，分页将被从中截断。

<masa-example file="Examples.components.paginations.Length"></masa-example>

#### 最大可见分页数

你也可以通过 `TotalVisible` 属性手动设置最大可见分页数。

<masa-example file="Examples.components.paginations.TotalVisible"></masa-example>