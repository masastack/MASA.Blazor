---
title: Menus
desc: "The **MMenu** component shows a menu at the position of the element used to activate it."
related:
  - /blazor/components/dialogs
  - /blazor/components/tooltips
  - /blazor/styles-and-animations/transitions
---

## Usage

Remember to put the element that activates the menu in the **ActivatorContent**.

<masa-example file="Examples.components.menus.Usage"></masa-example>

## Examples

### Props

#### Absolute

Menus can also be placed absolutely on top of the activator element using the `Absolute` prop. Try clicking anywhere on the image.

<masa-example file="Examples.components.menus.Absolute"></masa-example>

#### AbsoluteWithoutActivator

Menus can also be used without an activator by using `Absolute` together with the props `PositionX` and `PositionY`. Try
right-clicking anywhere on the image.

<masa-example file="Examples.components.menus.AbsoluteWithoutActivator"></masa-example>

#### CloseOnClick

Menu can be closed when lost focus.

<masa-example file="Examples.components.menus.CloseOnClick"></masa-example>

#### CloseOnContentClick

You can configure whether **MMenu** should be closed when its content is clicked.

<masa-example file="Examples.components.menus.CloseOnContentClick"></masa-example>

#### Disabled

You can disable the menu. `Disabled` menus can’t be opened.

<masa-example file="Examples.components.menus.Disabled"></masa-example>

#### Offset X

**MMenu** can be offset by the X axis to make the activator visible.

<masa-example file="Examples.components.menus.OffsetX"></masa-example>

#### Offset Y

**MMenu** can be offset by the Y axis to make the activator visible.

<masa-example file="Examples.components.menus.OffsetY"></masa-example>

#### OpenOnHover

Menus can be accessed using hover instead of clicking with the `OpenOnHover` prop.

<masa-example file="Examples.components.menus.OpenOnHover"></masa-example>

#### Rounded

Menus can have their border-radius set by the `Rounded` prop. Additional information about rounded classes is on the
[Border Radius](/blazor/styles-and-animations/border-radius) page.

<masa-example file="Examples.components.menus.Rounded"></masa-example>

### Contents

#### Activator and tooltip

With the `RenderFragment` syntax, nested activators such as those seen with a **MMenu** and **MTooltip** attached to the
same activator button, need a particular setup in order to function correctly. Note: this same syntax is used for other
nested activators such as **MDialog** / **MTooltip**.

<masa-example file="Examples.components.menus.ActivatorAndTooltip"></masa-example>

### Misc

#### Custom transitions

Masa.Blazor comes with 3 standard transitions, **scale**, **slide-x** and **slide-y**. You can also create your own and
pass it as the transition argument.

<masa-example file="Examples.components.menus.CustomTransitions"></masa-example>

#### Popover menu

A menu can be configured to be static when opened, allowing it to function as a popover. This can be useful when there
are multiple interactive items within the menu contents.

<masa-example file="Examples.components.menus.PopoverMenu"></masa-example>

#### Use In components

Menus can be placed within almost any component.

<masa-example file="Examples.components.menus.UseInComponents"></masa-example>