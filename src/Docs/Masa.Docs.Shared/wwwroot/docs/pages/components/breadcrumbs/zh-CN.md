---
title: Breadcrumbs（面包屑导航）
desc: "Breadcrumbs 组件适用于页面层级导航"
related:
  - /components/buttons
  - /components/navigation-drawers
  - /components/icons
---

## 使用

默认情况下，面包屑导航使用文本分隔符。 也可以是任何字符串。

<breadcrumbs-usage></breadcrumbs-usage>

## 注意

<!--alert:info-->
默认情况下，`MBreadcrumbs` 将禁用路由联动。可以通过 **Linkage** 属性开启路由联动。

## Examples

### Props

#### 分割线

可以使用 divider 属性来设置面包屑分隔符。

<example file="" />

#### 路由联动

<example file="" />

#### 大号

大的面包屑具有较大的字体。

<example file="" />

### 插槽

#### 图标分隔符

对于图标变量，面包屑可以使用Material设计中的任何图标。

<example file="" />

#### 项目

您可以使用 **item** 插槽自定义每个面包屑。

<example file="" />
