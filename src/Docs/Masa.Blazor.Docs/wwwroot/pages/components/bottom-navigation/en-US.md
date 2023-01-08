---
title: Bottom navigation
desc: "The **MBottomNavigation** component is an alternative to the sidebar. It is primarily used for mobile applications ."
related:
  - /blazor/components/buttons
  - /blazor/components/icons
  - /blazor/components/tabs
---

## Usage

While **MBottomNavigation** is meant to be used with router, you can also programmatically control the active state of the buttons by using the value property. A button is given a default value of its *index* with **MBottomNavigation**.

<bottom-navigation-usage></bottom-navigation-usage>

## Examples

### Props

#### Color

The `Color` parameter applies a color to the background of the bottom navigation. We recommend using the `Light` and `Dark` parameters to properly contrast text color.

<masa-example file="Examples.components.bottom_navigation.Color"></masa-example>

#### Grow

Using the `Grow` property forces [MButton](/blazor/components/buttons) components to fill all available space. Buttons have a maximum width of 168px per the [Bottom Navigation MD specification](https://material.io/components/bottom-navigation#specs).

<masa-example file="Examples.components.bottom_navigation.Grow"></masa-example>

#### Hide on scroll

The **MBottomNavigation** component hides when scrolling up when using the `HideOnScroll` parameter. This is similar to the [scrolling techniques](https://material.io/archive/guidelines/patterns/scrolling-techniques.html) that are supported in [MAppBar](/blazor/components/app-bars). In the following example, scroll up and down to see this behavior.

<masa-example file="Examples.components.bottom_navigation.HideOnScroll"></masa-example>

#### Horizontal

Adjust the style of buttons and icons by using the `Horizontal` parameter. This positions button text inline with the provided [MIcon](/blazor/components/icons).

<masa-example file="Examples.components.bottom_navigation.Horizontal"></masa-example>

#### Scroll threshold

Modify the `ScrollThreshold` parameter to increase the distance a user must scroll before the **MBottomNavigation** is hidden.

<masa-example file="Examples.components.bottom_navigation.ScrollThreshold"></masa-example>

#### Shift

The `Shift` parameter hides button text when not active. This provides an alternative visual style to the **MBottomNavigation** component.

<masa-example file="Examples.components.bottom_navigation.Shift"></masa-example>

#### Toggle

The display state of **MBottomNavigation** can be toggled using the `InputValue` parameter. You can also control the currently active button using **@bind-Value**.

<masa-example file="Examples.components.bottom_navigation.Toggle"></masa-example>

