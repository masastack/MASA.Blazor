---
title: Sticky（固定）
desc: 当滚动时使元素固定在视口上。
---

## 示例 {#examples}

### 属性 {#props}

#### 禁用 {#disabled}

<masa-example file="Examples.labs.sticky.Disabled"></masa-example>

#### 偏移 {#offset}

设置 `OffsetTop` 属性可以使元素在固定时距离顶部的距离，设置 `OffsetBottom` 属性可以使元素在固定时距离底部的距离，单位为像素。

<masa-example file="Examples.labs.sticky.Offset"></masa-example>

#### 滚动目标 {#scroll-target}

默认情况下，监听滚动的容器是窗口（`window`）。你可以设置 `ScrollTarget` 属性自定义滚动容器。

<masa-example file="Examples.labs.sticky.ScrollTarget"></masa-example>

### 插槽 {#contents}

#### ChildContent

提供 `bool` 类型的上下文，用于判断元素是否固定。

<masa-example file="Examples.labs.sticky.ChildContent"></masa-example>

