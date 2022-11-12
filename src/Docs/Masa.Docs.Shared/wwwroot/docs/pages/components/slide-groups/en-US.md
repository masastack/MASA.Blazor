---
title: Slide Groups
desc: "The **MSlideGroup** component is used to display pseudo paginated information. It uses [**MItemGroup**](/components/item-groups) at its core and provides a baseline for components such as [**MTabs**](/components/tabs) and [**MChipGroup**](/components/chip-groups)."
related:
  - /components/icons
  - /components/carousels
  - /components/tabs
---

## Usage

Similar to the **MWindow** component, **MSideGroup** lets items to take up as much space as needed, allowing the user to move horizontally through the provided information.

<slide-groups-usage></slide-groups-usage>

## Examples

### Props

#### Active class

The `ActiveClass` property allows you to set custom CSS class on active items.

<example file="" />

#### Center active

Using the `CenterActive` prop will make the active item always centered.

<example file="" />

#### Custom icons

You can add your custom pagination icons instead of arrows using the `NextIcon` and `PrevIcon` props.

<example file="" />

#### Mandatory

The `Mandatory` prop will make the slide group require at least 1 item must be selected.

<example file="" />

#### Multiple

You can select multiple items by setting the `Multiple` prop.

<example file="" />

### Misc

#### Pseudo Carousel

Customize the slide group to creatively display information on sheets. Using the selection, we can display auxillary information easily for the user.

<example file="" />