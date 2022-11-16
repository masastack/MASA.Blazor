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

<masa-example file="Examples.components.drag_zone.Group"></masa-example>

#### Options

Parameter configuration.

<masa-example file="Examples.components.drag_zone.Options"></masa-example>

#### Pull

Drag and drop configuration by specifying the value of the `Pull` property. `true` allows dragging, `false` disables dragging。

<masa-example file="Examples.components.drag_zone.Pull"></masa-example>

#### Put

Determines whether child elements can be sorted by specifying the value of the `Sort` property. `true` allows sorting, `false` does not allow sorting.

<masa-example file="Examples.components.drag_zone.Put"></masa-example>

#### Sort

Whether child elements can be sorted.

<masa-example file="Examples.components.drag_zone.Sort"></masa-example>

### Misc

#### MultiyZone

Drag and drop across multiple regions.

<masa-example file="Examples.components.drag_zone.MultiyZone"></masa-example>

#### Clone

Selected element clone drag and drop.

<masa-example file="Examples.components.drag_zone.Clone"></masa-example>

#### Filter

Filter the elements, those that do not match cannot be dragged and dropped.

<masa-example file="Examples.components.drag_zone.Filter"></masa-example>

