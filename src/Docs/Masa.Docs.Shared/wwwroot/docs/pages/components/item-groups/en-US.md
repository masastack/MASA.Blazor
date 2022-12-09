---
title: Item Groups
desc: "The **MItemGroup** provides the ability to create a group of selectable items out of any component. This is the baseline
functionality for components such as **MTabs** and **MCarousel**."
related:
  - /components/button-groups
  - /components/carousels
  - /components/tabs
---

## Usage

The core usage of the **MItemGroup** is to create groups of anything that should be controlled by a `Value`.

<item-groups-usage></item-groups-usage>

## Examples

### Props

#### Active class

The `ActiveClass` property allows you to set custom CSS class on active items.

<example file="" />

#### Mandatory

`Mandatory` item groups must have at least 1 item selected.

<example file="" />

#### Multiple

Multiple items can be selected using the `Multiple` property item group.

<example file="" />

### Misc

#### Chips

Easily hook up a custom chip group.

<example file="" />

#### Selection

Icons can be used as toggle buttons when they allow selection, or deselection, of a single choice, such as marking an item as a favorite.

<example file="" />