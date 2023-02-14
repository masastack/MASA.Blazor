---
title: DragZone（拖放）
desc: "**MDragZone** 组件基于[Sorttable.js](https://github.com/SortableJS/Sortable)实现的可拖放功能"
tag: "JS代理"
---

## 使用

拖拽组件和dom对象。

<masa-example file="Examples.components.drag_zone.Usage"></masa-example>

<app-alert type="info" content='使用前需要引入 `Sorttable.js`：`<script src="https://cdn.masastack.com/npm/sortable/Sortable.min.js"></script>`。'></app-alert>


## 示例

### 属性

#### 拖拉区域名称

拖拉区域名称。

<masa-example file="Examples.components.drag_zone.Group"></masa-example>

#### 参数配置

参数配置。

<masa-example file="Examples.components.drag_zone.Options"></masa-example>

#### 拖拉

通过指定 `Pull` 属性的值进行拖拉配置。 `true` 允许拖拉，`false` 禁止拖拉。

<masa-example file="Examples.components.drag_zone.Pull"></masa-example>

#### 拖放

通过指定 `Put` 属性的值进行拖放配置：true 允许拖放 ，false禁止拖放。

<masa-example file="Examples.components.drag_zone.Put"></masa-example>

#### 子元素是否可以排序

通过指定 `Sort` 属性的值指定子元素是否可以排序。 `true` 允许排序，`false` 不允许排序。

<masa-example file="Examples.components.drag_zone.Sort"></masa-example>

### 其他

#### 多区域

多区域跨区域拖放。

<masa-example file="Examples.components.drag_zone.MultiyZone"></masa-example>

#### 克隆

选中的元素克隆拖放。

<masa-example file="Examples.components.drag_zone.Clone"></masa-example>

#### 过滤

对元素进行过滤，不符合的不能拖放。

<masa-example file="Examples.components.drag_zone.Filter"></masa-example>

