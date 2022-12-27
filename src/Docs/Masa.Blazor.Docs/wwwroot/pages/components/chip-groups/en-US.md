---
title: Chip Groups
desc: "The **MChipGroup** supercharges the **MChip** component by providing groupable functionality. It is used for creating groups of selections using chips."
related:
  - /blazor/components/chips
  - /blazor/components/slide-groups
  - /blazor/components/item-groups
---

## Usage

Chip groups make it easy for users to select filtering options for more complex implementations. By default MChipGroup will overflow to the right but can be changed to a column only mode.

<chip-groups-usage></chip-groups-usage>

## Examples

### Props

#### Column

Chip groups with `Column` prop can wrap their chips.

<masa-example file="Examples.components.chip_groups.Column"></masa-example>

#### FilterResults

Easily create chip groups that provide additional feedback with the `Filter` prop. This creates an alternative visual style that communicates to the user that the chip is selected.

<masa-example file="Examples.components.chip_groups.FilterResults"></masa-example>

#### Mandatory

Chip groups with `Mandatory` prop must always have a value selected.

<masa-example file="Examples.components.chip_groups.Mandatory"></masa-example>

#### Multiple

Chip groups with `Multiple` prop can have many values selected.

<masa-example file="Examples.components.chip_groups.Multiple"></masa-example>

### Misc

#### Product card

**MChip** components can have an explicit value for their `@bind-Value`. This is passed to the **MChipGroup** component and is useful when you don't want to use the chip index as its value.

<masa-example file="Examples.components.chip_groups.ProductCard"></masa-example>

#### Toothbrush card

Chip groups allow the creation of custom interfaces that perform the same actions as an item group or radio controls, but are stylistically different.

<masa-example file="Examples.components.chip_groups.ToothbrushCard"></masa-example>