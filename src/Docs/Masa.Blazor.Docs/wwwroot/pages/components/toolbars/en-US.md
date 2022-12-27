---
title: Toolbars
desc: "The **MToolbar** component is pivotal to any graphical user interface (GUI), as it generally is the primary source of site navigation.The toolbar component works great in conjunction with [MNavigationDrawer](/blazor/components/navigation-drawers) and [MCard](/blazor/components/cards)."
related:
  - /blazor/components/buttons
  - /blazor/components/footers
  - /blazor/components/tabs
---

## Usage

A toolbar is a flexible container that can be used in a number of ways. By default, the toolbar is 64px high on desktop and 56px high on mobile. 
There are a number of helper components available to use with the toolbar. 
The **MToolbarTitle** is used for displaying a title and **MToolbarItems** allow **MButton** to extend full height.

<toolbars-usage></toolbars-usage>

## Anatomy

## Caveats

<app-alert type="warning" content="When **MButton** with `Icon` prop is used inside **MToolbar** and **MAppbar**, they will automatically increase their size and apply negative margins to ensure appropriate spacing according to material design specifications.
If you choose to wrap your buttons in any container, such as `div` , you need to apply a negative margin to the container for proper alignment."></app-alert>

## Examples

### Props

#### Background

Toolbars can display a background as opposed to a solid color using the src prop. This can be modified further by using
the img slot and providing your own [MImage](/blazor/components/images) component. Backgrounds can be faded using
a [MAppBar](/blazor/components/app-bars).

<masa-example file="Examples.components.toolbars.Background"></masa-example>

#### Collapse

Toolbars can be collapsed to save screen space.

<masa-example file="Examples.components.toolbars.Collapse"></masa-example>

#### Dense toolbars

Dense toolbars reduce their height to 48px. When using in conjunction with the prominent prop, will reduce height to 96px.

<masa-example file="Examples.components.toolbars.DenseToolbars"></masa-example>

#### Extended

Rating can be given different sizing options to fit a multitude of scenarios.

<masa-example file="Examples.components.toolbars.Extended"></masa-example>

#### Extenstion height

The extension’s height can be customized.

<masa-example file="Examples.components.toolbars.ExtensitionHeight"></masa-example>

#### Floating with search

A floating toolbar is turned into an inline element that only takes up as much space as needed. This is particularly useful when placing toolbars over content.

<masa-example file="Examples.components.toolbars.FloatingWithSearch"></masa-example>

#### Light And Dark

Toolbars come in 2 variants, light and dark. Light toolbars have dark tinted buttons and dark text whereas dark toolbars have white tinted buttons and white text.

<masa-example file="Examples.components.toolbars.LightAndDark"></masa-example>

#### Prominent toolbars

Prominent toolbars increase the **MToolbar** height to 128px and positions the **MToolbarTitle** towards the bottom of the
container. This is expanded upon in [MApp](/blazor/components/application) with the ability to shrink a prominent toolbar
to a `dense` or `short` one.

<masa-example file="Examples.components.toolbars.ProminentToolbars"></masa-example>

### Misc

#### Contextual action bar

It is possible to update the appearance of a toolbar in response to changes in app state. In this example, the color and content of the toolbar changes in response to user selections in the **MSelect**.

<masa-example file="Examples.components.toolbars.ContextualActionBar"></masa-example>

#### Flexible and card toolbar

In this example we offset our card onto the extended content area of a toolbar using the extended prop.

<masa-example file="Examples.components.toolbars.FlexibleAndCardToolbar"></masa-example>

#### Variations

A **MToolbar** has multiple variations that can be applied with themes and helper classes. These range from light and dark themes, colored and transparent.

<masa-example file="Examples.components.toolbars.Variations"></masa-example>