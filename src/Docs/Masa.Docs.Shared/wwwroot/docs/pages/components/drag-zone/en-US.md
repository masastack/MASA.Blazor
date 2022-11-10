---
title: DragZone（拖放）
desc: "The `MDragZone` component is based on [Sorttable.js](https://github.com/SortableJS/Sortable) to implement drag and drop functionality"
tag: js-proxy
---

## Usage

Drag and drop components and dom objects.

<drag-zone-usage></drag-zone-usage>

<!--alert:info-->
You need to reference the package of Sorttable.js before using it ：
<br />
`<script src="https://cdn.masastack.com/npm/sortable/Sortable.min.js"></script>`.
<!--/alert:info-->

## Examples

### Props

#### Group

Drag area name.

<example file="" />

#### Options

Parameter configuration.

<example file="" />

#### Pull

Drag and drop configuration by specifying the value of the `Pull` property. `true` allows dragging, `false` disables dragging, `clone` clones.

<example file="" />

#### Put

Determines whether child elements can be sorted by specifying the value of the `Sort` property. `true` allows sorting, `false` does not allow sorting.

<example file="" />

#### Sort

Whether child elements can be sorted.

<example file="" />

### Misc

#### MultiyZone

Drag and drop across multiple regions.

<example file="" />

#### Clone

Selected element clone drag and drop.

<example file="" />

#### Filter

Filter the elements, those that do not match cannot be dragged and dropped.

<example file="" />

