---
title: Item Groups
desc: "The **MItemGroup** provides the ability to create a group of selectable items out of any component. This is the baseline functionality for components such as **MTabs** and **MCarousel**."
related:
  - /blazor/components/button-groups
  - /blazor/components/carousels
  - /blazor/components/tabs
---

## Usage

The core usage of the **MItemGroup** is to create groups of anything that should be controlled by a `Value`.

<masa-example file="Examples.components.item_groups.Usage"></masa-example>

## Examples

### Props

#### Active class

The `ActiveClass` property allows you to set custom CSS class on active items.

<masa-example file="Examples.components.item_groups.ActiveClass"></masa-example>

#### Mandatory

`Mandatory` item groups must have at least 1 item selected.

<masa-example file="Examples.components.item_groups.Mandatory"></masa-example>

#### Multiple

Multiple items can be selected using the `Multiple` property item group.

<masa-example file="Examples.components.item_groups.Multiple"></masa-example>

### Misc

#### Chips

Easily hook up a custom chip group.

<masa-example file="Examples.components.item_groups.Chips"></masa-example>

#### Selection

Icons can be used as toggle buttons when they allow selection, or deselection, of a single choice, such as marking an item as a favorite.

<masa-example file="Examples.components.item_groups.Selection"></masa-example>