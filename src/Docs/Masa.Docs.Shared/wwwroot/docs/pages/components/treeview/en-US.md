---
category: Components
type: Treeview
title: Treeview
cols: 1
related:
  - /components/lists
  - /components/list-item-groups
  - /components/timelines
---

# Treeview

The **MTreeview** component is useful for displaying large amounts of nested data.

## Usage

A basic example

<treeview-usage></treeview-usage>

## API

- [MVirtualScroll](/api/MTreeview)

## Examples

### Props

#### Activatable

Treeview nodes can be activated by clicking on them.

<example file="" />

#### Color

Setting `Color` prop You can control the text and background color of the active treeview node.

<example file="" />

#### Dense

`Dense` mode provides more compact layout with decreased heights of the items.

<example file="" />

#### Hoverable

`Hoverable` prop treeview nodes can have a hover effect.

<example file="" />

#### ItemDisabled

Setting `ItemDisabled` prop allows to control which node’s property disables the node when set to `true`.

<example file="" />

#### OpenAll

Setting `OpenAll` prop Treeview nodes can be pre-opened on page load.

<example file="" />

#### Rounded

Setting `Rounded` prop You can make treeview nodes rounded.

<example file="" />

#### Selectable

Setting `Selectable` prop You can easily select treeview nodes and children.

<example file="" />

#### SelectColor

Setting `SelectColor` prop You can control the color of the selected node checkbox.

<example file="" />

#### Select type

Treeview now supports two different selection types. The default type is `Leaf`, which will only include leaf nodes in
the `@bind-Value` array, but will render parent nodes as either partially or fully selected. The alternative mode is
`Independent`, which allows one to select parent nodes, but each node is independent of its parent and children.

<example file="" />

#### Shaped

Setting `Shaped` prop treeview’s have rounded borders on one side of the nodes.

<example file="" />

### Slots

#### Append and label

Using the the `LabelContent`, and an `AppendContent` slots we are able to create an intuitive file explorer.

<example file="" />

### Misc

#### Search and filter

Easily filter your treeview by using the `Search` prop. You can easily apply your custom filtering function if you
need case-sensitive or fuzzy filtering by setting the `Filter` prop. This works similar to the **MAutocomplete**
component.

<example file="" />

#### Selectable icons

Customize the `on`, `off` and `indeterminate` icons for your selectable tree. Combine with other advanced
functionality like API loaded items.

<example file="" />