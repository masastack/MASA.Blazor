---
title: Sheets
desc: "The **MSheet** component is the baseline for numerous components such as MCard, MToolbar, and more. The available properties form the foundation of Material Design—the concept of paper and elevation (shadows)."
related:
  - /components/cards
  - /components/grid-system
  - /stylesandanimations/elevation
---

## Usage

The **MSheet** component is a transformable piece of paper that provides a basic foundation for **MASA Blazor** features. 
For example, properties such as `rounded` and `shaped` modify the `border-radius` property while `elevation` increases/decreases `box-shadow`.

<sheets-usage></sheets-usage>

## Examples

### Props

#### Color

Sheets and components based on them can have different sizes and colors.

The **MSheet** component accepts custom [Material Design Color](/stylesandanimations/colors) values such
as `warning`, `amber darken-3`, and `deep-purple accent` —as well as _rgb_, _rgba_, and _hexadecimal_ values.

<example file="" />

#### Elevation

The **MSheet** component accepts a custom elevation between **0 and 24** (0 is default). The _elevation_ property modifies
the
`box-shadow` property. More information is located in the
MD [Elevation Design Specification](https://material.io/design/environment/elevation.html).

<example file="" />

#### Rounded

The `Rounded` prop adds a default `border-radius` of _4px_. By default, the **MSheet** component has no border-radius.
Customize the radius’s size and location by providing a custom rounded value; e.g. a rounded value of _tr-xl_ _l-pill_
equates to _rounded-tr-xl_ _rounded-l-pill_. Additional information is on the [Border Radius](/stylesandanimations/border-radius) page.

<example file="" />