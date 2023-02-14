---
title: Treeview
desc: "The **MTreeview** component is useful for displaying large amounts of nested data."
related:
  - /blazor/components/lists
  - /blazor/components/list-item-groups
  - /blazor/components/timelines
---

## Usage

<masa-example file="Examples.components.treeview.Usage"></masa-example>

## Examples

### Props

#### Activatable

Treeview nodes can be activated by clicking on them.

<masa-example file="Examples.components.treeview.Activatable"></masa-example>

#### Color

Setting `Color` prop You can control the text and background color of the active treeview node.

<masa-example file="Examples.components.treeview.Color"></masa-example>

#### Dense

`Dense` mode provides more compact layout with decreased heights of the items.

<masa-example file="Examples.components.treeview.Dense"></masa-example>

#### Hoverable

`Hoverable` prop treeview nodes can have a hover effect.

<masa-example file="Examples.components.treeview.Hoverable"></masa-example>

#### ItemDisabled

Setting `ItemDisabled` prop allows to control which node’s property disables the node when set to `true`.

<masa-example file="Examples.components.treeview.ItemDisabled"></masa-example>

#### LoadChildren

You can dynamically load child data by supplying a callback to the **LoadChildren** prop. This callback will be executed the first time a user tries to expand an item that has a children property that is an empty array.

<masa-example file="Examples.components.treeview.LoadChildren"></masa-example>

#### OpenAll

Setting `OpenAll` prop Treeview nodes can be pre-opened on page load.

<masa-example file="Examples.components.treeview.OpenAll"></masa-example>

#### Rounded

Setting `Rounded` prop You can make treeview nodes rounded.

<masa-example file="Examples.components.treeview.Rounded"></masa-example>

#### Selectable

Setting `Selectable` prop You can easily select treeview nodes and children.

<masa-example file="Examples.components.treeview.Selectable"></masa-example>

#### SelectColor

Setting `SelectColor` prop You can control the color of the selected node checkbox.

<masa-example file="Examples.components.treeview.SelectColor"></masa-example>

#### Select type

Treeview now supports two different selection types. The default type is `Leaf`, which will only include leaf nodes in
the `@bind-Value` array, but will render parent nodes as either partially or fully selected. The alternative mode is
`Independent`, which allows one to select parent nodes, but each node is independent of its parent and children.

<masa-example file="Examples.components.treeview.SelectType"></masa-example>

#### Shaped

Setting `Shaped` prop treeview’s have rounded borders on one side of the nodes.

<masa-example file="Examples.components.treeview.Shaped"></masa-example>

### Slots

#### Append and label

Using the the `LabelContent`, and an `AppendContent` slots we are able to create an intuitive file explorer.

<masa-example file="Examples.components.treeview.AppendAndLabel"></masa-example>

### Misc

#### Search and filter

Easily filter your treeview by using the `Search` prop. You can easily apply your custom filtering function if you
need case-sensitive or fuzzy filtering by setting the `Filter` prop. This works similar to the **MAutocomplete**
component.

<masa-example file="Examples.components.treeview.SearchAndFilter"></masa-example>

#### Selectable icons

Customize the `on`, `off` and `indeterminate` icons for your selectable tree. Combine with other advanced
functionality like API loaded items.

<masa-example file="Examples.components.treeview.SelectableIcons"></masa-example>