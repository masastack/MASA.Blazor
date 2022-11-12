---
title: DragZone（拖放）
desc: "**MDragZone** 组件基于[Sorttable.js](https://github.com/SortableJS/Sortable)实现的可拖放功能"
tag: js-proxy
---

## 使用

拖拽组件和dom对象。

<drag-zone-usage></drag-zone-usage>

<!--alert:info-->
使用前需要引入 `Sorttable.js`：
<br />
`<script src="https://cdn.masastack.com/npm/sortable/Sortable.min.js"></script>`。
<!--/alert:info-->

## 示例

### 属性

#### 拖拉区域名称

拖拉区域名称。

<example file="" />

### 参数配置

参数配置。

<example file="" />

#### 拖拉

通过指定 `Pull` 属性的值进行拖拉配置。 `true` 允许拖拉，`false` 禁止拖拉，`clone`  克隆。

<example file="" />

#### 子元素是否可以排序

通过指定 `Sort` 属性的值指定子元素是否可以排序。 `true` 允许排序，`false` 不允许排序。

<example file="" />

### 其他

#### 多区域

多区域跨区域拖放。

<example file="" />

#### 克隆

选中的元素克隆拖放。

<example file="" />

#### 过滤

对元素进行过滤，不符合的不能拖放。

<example file="" />

