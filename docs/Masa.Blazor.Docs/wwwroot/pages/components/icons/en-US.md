---
title: Icons
desc: "The **MIcon** component provides a large set of glyphs to provide context to various aspects of your application. For a list of all available icons, visit the official [Material Design Icons](https://materialdesignicons.com/) page. To use any of these icons simply use the `mdi-` prefix followed by the icon name."
related:
  - /blazor/features/icon-fonts
  - /blazor/components/buttons
  - /blazor/components/cards
---

## Usage

**MIcon** come in two themes (`light` and `dark`), and five different sizes (`x-small`, `small`, `medium` (default), `large`, and `x-large`).

<icons-usage></icons-usage>

## Examples

### Props

#### Color

Using the color helper, setting the `Color` property you can change the color of the icons for the standard dark and light themes.

<masa-example file="Examples.components.icons.Color"></masa-example>

### Events

#### Click

Setting the `Click` property to bind any click event to **MIcon** will automatically change the cursor to a pointer.

<masa-example file="Examples.components.icons.Click"></masa-example>

### Misc

#### Button

Icons can be used inside of buttons to add emphasis to the action.

<masa-example file="Examples.components.icons.Button"></masa-example>

#### Font Awesome

[Font Awesome](https://fontawesome.com/icons/) is also supported. Simply use the `fa-` prefixed icon name. Please note
that you still need to include the Font Awesome icons in your project. For more information on how to install it, please navigate to the [installation page](/blazor/features/icon-fonts#font-awesome-5-icons).

<masa-example file="Examples.components.icons.FontAwesome"></masa-example>

#### Material Design

[Material Design](https://material.io/tools/icons/?style=baseline) is also supported. For more information on how to install it please [navigate here](/blazor/features/icon-fonts#material-icons).

<masa-example file="Examples.components.icons.MaterialDesign"></masa-example>

#### SVG

If you want to use SVG icons with `MIcon` component, only support input the `path` of SVG. Multi `path`s is also supported.

<masa-example file="Examples.components.icons.Svg"></masa-example>