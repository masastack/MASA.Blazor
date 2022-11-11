---
title: Bottom navigation
desc: "The **MBottomNavigation** component is an alternative to the sidebar. It is primarily used for mobile applications and comes in three variants,  `icons` and `text`, and `shift`."
related:
  - /components/buttons
  - /components/icons
  - /components/tabs
---

## Usage

While **MBottomNavigation** is meant to be used with router, you can also programmatically control the active state of the buttons by using the value property. A button is given a default value of its *index* with **MBottomNavigation**.

<bottom-navigation-usage></bottom-navigation-usage>

## Examples

### Props

#### Color

The `Color` parameter applies a color to the background of the bottom navigation. We recommend using the `Light` and `Dark` parameters to properly contrast text color.

<example file="" />

#### Grow

Using the `Grow` property forces [MButton](/components/buttons) components to fill all available space. Buttons have a maximum width of 168px per the [Bottom Navigation MD specification](https://material.io/components/bottom-navigation#specs).

<example file="" />

#### Hide on scroll

The **MBottomNavigation** component hides when scrolling up when using the `HideOnScroll` parameter. This is similar to the [scrolling techniques](https://material.io/archive/guidelines/patterns/scrolling-techniques.html) that are supported in [MAppBar](/components/app-bars). In the following example, scroll up and down to see this behavior.

<example file="" />

#### Horizontal

Adjust the style of buttons and icons by using the `Horizontal` parameter. This positions button text inline with the provided [MIcon](/components/icons).

<example file="" />

#### Scroll threshold

Modify the `ScrollThreshold` parameter to increase the distance a user must scroll before the **MBottomNavigation** is hidden.

<example file="" />

#### Shift

The `Shift` parameter hides button text when not active. This provides an alternative visual style to the **MBottomNavigation** component.

<example file="" />

#### Toggle

The display state of **MBottomNavigation** can be toggled using the   `InputValue` parameter. You can also control the currently active button using **@bind-Value**.

<example file="" />

