---
title: Sortable（可排序的）
desc: "基于 [Sortablejs](https://github.com/SortableJS/Sortable) 的现代浏览器和触摸设备的可重新排序的拖放列表。"
tag: "JS代理"
---

## 使用

<masa-example file="Examples.labs.sortable.Default"></masa-example>

## 示例

### 属性

#### 分组

要从一个列表拖动元素到另一个列表，两个列表必须具有相同的 `Group` 值。

<masa-example file="Examples.labs.sortable.Group"></masa-example>

#### 拖动手柄

定义一个拖动手柄，它是每个列表元素的一个区域，允许它在周围拖动。

<masa-example file="Examples.labs.sortable.Handle"></masa-example>

#### 自定义标记

您可以为根元素和项目元素定义标记。

<masa-example file="Examples.labs.sortable.Tag"></masa-example>

#### 动画

<masa-example file="Examples.labs.sortable.Animation"></masa-example>

## Sortable 提供者

为了更高级的使用，你可以使用 **MSortableProvider** 来创建一个不依赖内置元素的拖拽列表。

`Items` 只是提供拖放的列表，`@bind-Order` 用于设置和更新排列的顺序。

当用户通过代码去更新 `Items` 时，需要同时更新 `Order`。

当用户通过鼠标或触摸拖动时，会触发 `OnAdd` 或 `OnRemove` 事件，此时需要更新 `Items`，但不用更改 `Order`。

<masa-example file="Examples.labs.sortable.SortableProvider"></masa-example>
