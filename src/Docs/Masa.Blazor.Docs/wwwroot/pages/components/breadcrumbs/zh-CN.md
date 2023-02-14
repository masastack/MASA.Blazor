---
title: Breadcrumbs（面包屑导航）
desc: "**MBreadcrumbs** 组件适用于页面层级导航"
related:
  - /blazor/components/buttons
  - /blazor/components/navigation-drawers
  - /blazor/components/icons
---

## 使用

默认情况下，面包屑导航使用文本分隔符。 也可以是任何字符串。

<breadcrumbs-usage></breadcrumbs-usage>

## 注意

<app-alert type="info" content="默认情况下，**MBreadcrumbs** 将禁用路由联动。可以通过 `Linkage` 属性开启路由联动。"></app-alert>

## 示例

### 属性

#### 分割线

可以使用 `Divider` 属性来设置面包屑分隔符。

<masa-example file="Examples.components.breadcrumbs.Divider"></masa-example>

#### 路由联动

<masa-example file="Examples.components.breadcrumbs.Routable"></masa-example>

#### 大号

大的面包屑具有较大的字体。

<masa-example file="Examples.components.breadcrumbs.Large"></masa-example>

### 插槽

#### 图标分隔符

对于图标变量，面包屑可以使用Material设计中的任何图标。

<masa-example file="Examples.components.breadcrumbs.IconDividers"></masa-example>

#### 项目

您可以使用 **ItemContent** 插槽自定义每个面包屑。

<masa-example file="Examples.components.breadcrumbs.Item"></masa-example>
