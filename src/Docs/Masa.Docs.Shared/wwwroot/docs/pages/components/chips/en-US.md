---
title: Chips
desc: "The **MChip** component is used to convey small pieces of information. Using the close property, the chip becomes
interactive, allowing user interaction. This component is used by the [MChipGroup](/components/chip-groups) for advanced selection options."
related:
  - /components/avatars
  - /components/icons
  - /components/selects
---

## Usage

Chips come in the following variations: closeable, filter, outlined, pill. The default slot of v-chip will also accept avatars and icons alongside text.

<chips-usage></chips-usage>

## Examples

### Props

#### Closable

The chip that can be closed can be controlled by Close by default. If you want to know when the chip is closed, you can also listen to the OnCloseClick event.

<example file="" />

#### Colored

Any color in the Material Design palette can be used to change the chip color.

<example file="" />

#### Draggable

Use the `Draggable` property to make **MChip** components draggable with the mouse.

<example file="" />

#### Filter

The **MChip** component has a `Filter` option, which will show you additional icons when the chip is active. You can use `FilterIcon` to customize.

<example file="" />

#### Label

Label chips use the Card border-radius.

<example file="" />

#### NoRipple

MChip can not render ripple effects when ripple prop is set to false.

<example file="" />

#### Outlined

The Outline chip inherits its border color from the current text color.

<example file="" />

#### Sizes

**MChip**  components can have different sizes from `XSmall` to `XLarge`.

<example file="" />

### Events

#### Actions

The chip sheet can be used as an actionable item. As long as there is an click event, the chip will become interactive and methods can be called.

<example file="" />

### Contents

#### Icon

Chips can use text or any icon available in the Material Icons font library.

<example file="" />

### Misc

#### Custom list

Using a custom list allows us to always display the available options while providing the same search and selection capabilities.

<example file="" />

#### Expandable

Chip can be combined with **MMenu** to enable a set of specific operations for chip.

<example file="" />

#### Filtering

chip sheets are very suitable for providing auxiliary operations for specific tasks. In this example, we search a list of items and collect a subset of information to display available keywords.

<example file="" />

#### In selects

Select to display the selected data using a piece of chip. Try adding your own label below.

<example file="" />



