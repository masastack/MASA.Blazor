---
title: Slide Groups
desc: "The **MSlideGroup** component is used to display pseudo paginated information. It uses [MItemGroup](/blazor/components/item-groups) at its core and provides a baseline for components such as [MTabs](/blazor/components/tabs) and [MChipGroup](/blazor/components/chip-groups)."
related:
  - /blazor/components/icons
  - /blazor/components/carousels
  - /blazor/components/tabs
---

## Usage

Similar to the **MWindow** component, **MSideGroup** lets items to take up as much space as needed, allowing the user to move horizontally through the provided information.

<masa-example file="Examples.components.slide_groups.Usage"></masa-example>

## Examples

### Props

#### Active class

The `ActiveClass` property allows you to set custom CSS class on active items.

<masa-example file="Examples.components.slide_groups.ActiveClass"></masa-example>

#### Center active

Using the `CenterActive` prop will make the active item always centered.

<masa-example file="Examples.components.slide_groups.CenterActive"></masa-example>

#### Custom icons

You can add your custom pagination icons instead of arrows using the `NextIcon` and `PrevIcon` props.

<masa-example file="Examples.components.slide_groups.CustomIcons"></masa-example>

#### Mandatory

The `Mandatory` prop will make the slide group require at least 1 item must be selected.

<masa-example file="Examples.components.slide_groups.Mandatory"></masa-example>

#### Multiple

You can select multiple items by setting the `Multiple` prop.

<masa-example file="Examples.components.slide_groups.Multiple"></masa-example>

### Misc

#### Pseudo Carousel

Customize the slide group to creatively display information on sheets. Using the selection, we can display auxillary information easily for the user.

<masa-example file="Examples.components.slide_groups.PseudoCarousel"></masa-example>