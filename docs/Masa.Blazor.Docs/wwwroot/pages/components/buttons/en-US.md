---
title: Buttons
desc: "The **MButton** component replaces the standard html button with a material design theme and a multitude of options. Any color helper class can be used to alter the background or text color."
related:
  - /blazor/components/button-groups
  - /blazor/components/icons
  - /blazor/components/floating-action-buttons
---

## Usage

Buttons in their simplest form contain uppercase text, a slight elevation, hover effect, and a ripple effect on click.

<buttons-usage></buttons-usage>

## Anatomy

The recommended placement of elements inside of `MButton` is:

* Place text in the center
* Place visual content around container text

![Button Anatomy](https://cdn.masastack.com/stack/doc/masablazor/anatomy/btn-anatomy.png)

| Element / Area | Description |
| - | - |
| 1. Container | In addition to text, the Button container typically holds a [MIcon](blazor/components/icons/) component |
| 2. Icon (optional) | Leading media content intended to improve visual context |
| 3. Text | A content area for displaying text and other inline elements |

## Caveats

<app-alert type="warning" content="**MButton** is the only component that behaves differently when using the `Dark` prop. Normally components use the `Dark` prop to denote that they have a dark colored background and need their text to be white. While this will work
for **MButton** , it is advised to only use the prop when the button **IS ON** a colored background due to the disabled state
blending in with white backgrounds. If you need white text, simply add the `white--text` class."></app-alert>

## Examples

### Props

#### Block

`Block` buttons extend the full available width.

<masa-example file="Examples.components.buttons.Block"></masa-example>

#### Depressed

`Depressed` buttons still maintain their background color, but have no box shadow.

<masa-example file="Examples.components.buttons.Depressed"></masa-example>

#### Floating

Floating buttons are rounded and usually contain an icon.

<masa-example file="Examples.components.buttons.Floating"></masa-example>

#### Icon

Icons can be used for the primary content of a button. This property makes the button rounded and applies the `Text`
prop styles.

<masa-example file="Examples.components.buttons.Icon"></masa-example>

#### Loaders

Using the loading prop, you can notify a user that there is processing taking place. The default behavior is to use
a **MProgressCircular** component but this can be customized.

<masa-example file="Examples.components.buttons.Loaders"></masa-example>

#### Outlined

`Outlined` buttons inherit their borders from the current color applied.

<masa-example file="Examples.components.buttons.Outlined"></masa-example>

#### Plain

`Plain` buttons have a lower baseline opacity that reacts to `hover` and `focus`.

<masa-example file="Examples.components.buttons.Plain"></masa-example>

#### Rounded

`Rounded` buttons behave the same as regular buttons but have rounded edges.

<masa-example file="Examples.components.buttons.Rounded"></masa-example>

#### Sizing

Buttons can be given different sizing options to fit a multitude of scenarios.

<masa-example file="Examples.components.buttons.Size"></masa-example>

#### Text

Text buttons have no box shadow and no background. Only on hover is the container for the button shown. When used with
the `Color` prop, the supplied color is applied to the button text instead of the background.

<masa-example file="Examples.components.buttons.Text"></masa-example>

#### Tile

`Tile` buttons behave the same as regular buttons but have no border radius.

<masa-example file="Examples.components.buttons.Tile"></masa-example>

### Misc

#### Raised

`Raised` buttons have a box shadow that increases when clicked. This is the default style.

<masa-example file="Examples.components.buttons.Raised"></masa-example>


