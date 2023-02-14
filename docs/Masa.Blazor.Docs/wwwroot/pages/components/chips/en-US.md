---
title: Chips
desc: "The **MChip** component is used to convey small pieces of information. Using the close property, the chip becomes interactive, allowing user interaction. This component is used by the [MChipGroup](/blazor/components/chip-groups) for advanced selection options."
related:
  - /blazor/components/avatars
  - /blazor/components/icons
  - /blazor/components/selects
---

## Usage

Chips come in the following variations: closeable, filter, outlined, pill. The default slot of v-chip will also accept avatars and icons alongside text.

<chips-usage></chips-usage>

## Examples

### Props

#### Closable

The chip that can be closed can be controlled by Close by default. If you want to know when the chip is closed, you can also listen to the OnCloseClick event.

<masa-example file="Examples.components.chips.Closable"></masa-example>

#### Colored

Any color in the Material Design palette can be used to change the chip color.

<masa-example file="Examples.components.chips.Colored"></masa-example>

#### Draggable

Use the `Draggable` property to make **MChip** components draggable with the mouse.

<masa-example file="Examples.components.chips.Draggable"></masa-example>

#### Filter

The **MChip** component has a `Filter` option, which will show you additional icons when the chip is active. You can use `FilterIcon` to customize.

<masa-example file="Examples.components.chips.Filter"></masa-example>

#### Label

Label chips use the Card border-radius.

<masa-example file="Examples.components.chips.Label"></masa-example>

#### NoRipple

MChip can not render ripple effects when ripple prop is set to false.

<masa-example file="Examples.components.chips.NoRipple"></masa-example>

#### Outlined

The Outline chip inherits its border color from the current text color.

<masa-example file="Examples.components.chips.Outlined"></masa-example>

#### Sizes

**MChip**  components can have different sizes from `XSmall` to `XLarge`.

<masa-example file="Examples.components.chips.Sizes"></masa-example>

### Events

#### Actions

The chip sheet can be used as an actionable item. As long as there is an click event, the chip will become interactive and methods can be called.

<masa-example file="Examples.components.chips.ActionChips"></masa-example>

### Contents

#### Icon

Chips can use text or any icon available in the Material Icons font library.

<masa-example file="Examples.components.chips.Icon"></masa-example>

### Misc

#### Custom list

Using a custom list allows us to always display the available options while providing the same search and selection capabilities.

<masa-example file="Examples.components.chips.CustomList"></masa-example>

#### Expandable

Chip can be combined with **MMenu** to enable a set of specific operations for chip.

<masa-example file="Examples.components.chips.Expandable"></masa-example>

#### Filtering

chip sheets are very suitable for providing auxiliary operations for specific tasks. In this example, we search a list of items and collect a subset of information to display available keywords.

<masa-example file="Examples.components.chips.Filtering"></masa-example>

#### In selects

Select to display the selected data using a piece of chip. Try adding your own label below.

<masa-example file="Examples.components.chips.InSelects"></masa-example>



