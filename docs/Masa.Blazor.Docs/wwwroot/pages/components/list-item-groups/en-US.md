---
title: List Item Groups
desc: "The **MListItemGroup** provides the ability to create a group of selectable **MListItem**. The **MListItemGroup** component utilizes **MItemGroup** at its core to provide a clean interface for interactive lists."
related:
  - /blazor/components/lists
  - /blazor/components/item-groups
  - /blazor/components/cards
---

## Usage

By default, the `ListItemGroup` operates similarly to **ItemGroup**. If a `Value` is not provided, the group will provide a default based upon its index.

<masa-example file="Examples.components.list_item_groups.Usage"></masa-example>

## Examples

### Props

#### ActiveClass

You can select multiple items at one time.

<masa-example file="Examples.components.list_item_groups.ActiveClass"></masa-example>

#### Mandatory

At least one item must be selected to use the `Multiple` property.

<masa-example file="Examples.components.list_item_groups.Mandatory"></masa-example>

#### Multiple

Using the `Multiple` property you can select multiple items at once.

<masa-example file="Examples.components.list_item_groups.Multiple"></masa-example>

### Misc

#### Flat list

You can easily disable the default highlighting of selected **MListItem**s. This creates a lower profile for a user’s choices.

<masa-example file="Examples.components.list_item_groups.FlatList"></masa-example>

#### Selection controls

Using the default slot, you can access an items internal state and toggle it. Since the `Active` property is a boolean, we use the `IsActive` prop on the checkbox to link its state to the **MListItem**.

<masa-example file="Examples.components.list_item_groups.SelectionControls"></masa-example>