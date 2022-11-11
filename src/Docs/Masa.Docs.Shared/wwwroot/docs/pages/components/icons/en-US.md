---
title: Icons
desc: "The **MIcon** component provides a large set of glyphs to provide context to various aspects of your application. For a list of all available icons, visit the official [Material Design Icons](https://materialdesignicons.com/) page. To use any of these icons simply use the `mdi-` prefix followed by the icon name."
related:
  - /features/icon-fonts
  - /components/buttons
  - /components/cards
---

## Usage

**MIcon** come in two themes (`light` and `dark`), and five different sizes (`x-small`, `small`, `medium` (default), `large`, and `x-large`).

<icons-usage></icons-usage>

## Examples

### Props

#### Color

Using the color helper, setting the `Color` property you can change the color of the icons for the standard dark and light themes.

<example file="" />

### Events

#### Click

Setting the `Click` property to bind any click event to **MIcon** will automatically change the cursor to a pointer.

<example file="" />

### Misc

#### Button

Icons can be used inside of buttons to add emphasis to the action.

<example file="" />

#### Font Awesome

[Font Awesome](https://fontawesome.com/icons/) is also supported. Simply use the `fa-` prefixed icon name. Please note
that you still need to include the Font Awesome icons in your project.

<example file="" />

#### Material Design

[Material Design](https://material.io/tools/icons/?style=baseline) is also supported.

<example file="" />

#### SVG

If you want to use SVG icons with `MIcon` component, only support input the `path` of SVG.

<example file="" />