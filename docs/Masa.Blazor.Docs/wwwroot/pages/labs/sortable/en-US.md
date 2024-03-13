---
title: Sortable
desc: "Reorderable drag-and-drop lists for modern browsers and touch devices. Base on [Sortablejs](https://github.com/SortableJS/Sortable)."
tag: "JS Proxy"
---

## Usage

<masa-example file="Examples.labs.sortable.Default"></masa-example>

## Examples

### Props

#### Group

To drag elements from one list into another, both lists must have the same `Group` value.

<masa-example file="Examples.labs.sortable.Group"></masa-example>

#### Handle

Define a drag handler, which is an area of every list element that allows it to be dragged around.

<masa-example file="Examples.labs.sortable.Handle"></masa-example>

#### Tag

You can define the tag for root element and item element.

<masa-example file="Examples.labs.sortable.Tag"></masa-example>

#### Animation

<masa-example file="Examples.labs.sortable.Animation"></masa-example>

## Sortable Provider

For advanced usage, you can use the **MSortableProvider** to create a drag-and-drop list that does not depend on built-in elements.

`Items` provides the list for drag and drop, `@bind-Order` is used to set and update the order of the list.

When the user updates `Items` through code, `Order` needs to be updated at the same time.

When the user drags with the mouse or touch, the `OnAdd` or `OnRemove` event is triggered, and `Items` needs to be updated at this time, but `Order` does not need to be changed.

<masa-example file="Examples.labs.sortable.SortableProvider"></masa-example>
