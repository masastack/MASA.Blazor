---
category: Components
subtitle: 虚拟滚动条
type: 虚拟滚动条
title: Virtual scroller
cols: 1
related:
  - /components/lists
  - /components/data-tables
  - /components/data-iterators
---

# Virtual scroller（虚拟滚动条）

MVirtualScroll 组件显示一个虚拟，无限 的列表。 它支持动态高度和垂直滚动。

## 使用

<virtual-scroll-usage></virtual-scroll-usage>

## 解剖学

## API

- [MVirtualScroll](/api/MVirtualScroll)

## 示例

### 属性

#### 替补

 默认情况下 **MvirtualScroll** 不会预渲染出现在视图可见范围外的其它项。 使用 **OverscanCount** 属性会使滚动条渲染额外的项目作为替补。为了获得尽可能好的性能，建议尽量减小这个数字
。

<example file="" />

#### 用户目录

**MvirtualScroll** 组件通过仅渲染填充滚动条所需要的内容来实现无限数量项目的渲染，**ItemSize** 属性可以设置每个项的像素高度。

<example file="" />