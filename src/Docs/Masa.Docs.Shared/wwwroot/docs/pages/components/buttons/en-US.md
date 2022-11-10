---
title: Buttons
desc: "The **MButton** component replaces the standard html button with a material design theme and a multitude of options. Any color
helper class can be used to alter the background or text color."
related:
  - /components/button-groups
  - /components/icons
  - /components/floating-action-buttons
---

## Usage

Buttons in their simplest form contain uppercase text, a slight elevation, hover effect, and a ripple effect on click.

<buttons-usage></buttons-usage>

## Anatomy

## Caveats

<!--alert:warning--> 
**MButton** is the only component that behaves differently when using the **Dark** prop. Normally components use the `Dark` prop to denote that they have a dark colored background and need their text to be white. While this will work
for **MButton** , it is advised to only use the prop when the button **IS ON** a colored background due to the disabled state
blending in with white backgrounds. If you need white text, simply add the `white--text` class.

## Examples

### Props

#### Block

`Block` buttons extend the full available width.

<example file="" />

#### Depressed

`Depressed` buttons still maintain their background color, but have no box shadow.

<example file="" />

#### Floating

Floating buttons are rounded and usually contain an icon.

<example file="" />

#### Icon

Icons can be used for the primary content of a button. This property makes the button rounded and applies the `Text`
prop styles.

<example file="" />

#### Loaders

Using the loading prop, you can notify a user that there is processing taking place. The default behavior is to use
a **MProgressCircular** component but this can be customized.

<example file="" />

#### Outlined

`Outlined` buttons inherit their borders from the current color applied.

<example file="" />

#### Plain

`Plain` buttons have a lower baseline opacity that reacts to `hover` and `focus`.

<example file="" />

#### Rounded

`Rounded` buttons behave the same as regular buttons but have rounded edges.

<example file="" />

#### #Sizing

Buttons can be given different sizing options to fit a multitude of scenarios.

<example file="" />

#### Text

Text buttons have no box shadow and no background. Only on hover is the container for the button shown. When used with
the `Color` prop, the supplied color is applied to the button text instead of the background.

<example file="" />

#### Tile

`Tile` buttons behave the same as regular buttons but have no border radius.

<example file="" />

### Misc

#### Raised

`Raised` buttons have a box shadow that increases when clicked. This is the default style.

<example file="" />


