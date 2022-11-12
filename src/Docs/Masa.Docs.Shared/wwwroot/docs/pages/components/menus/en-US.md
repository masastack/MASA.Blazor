---
title: Menus
desc: "The **MMenu** component shows a menu at the position of the element used to activate it."
related:
  - /components/dialogs
  - /components/tooltips
  - /stylesandanimations/transitions
---

## Usage

Remember to put the element that activates the menu in the **ActivatorContent**.

<menus-usage></menus-usage>

## Examples

### Props

#### Absolute

Menus can also be placed absolutely on top of the activator element using the `Absolute` prop. Try clicking anywhere on the image.

<example file="" />

#### AbsoluteWithoutActivator

Menus can also be used without an activator by using `Absolute` together with the props `PositionX` and `PositionY`. Try
right-clicking anywhere on the image.

<example file="" />

#### CloseOnClick

Menu can be closed when lost focus.

<example file="" />

#### CloseOnContentClick

You can configure whether **MMenu** should be closed when its content is clicked.

<example file="" />

#### Disabled

You can disable the menu. `Disabled` menus can’t be opened.

<example file="" />

#### Offset X

**MMenu** can be offset by the X axis to make the activator visible.

<example file="" />

#### Offset Y

**MMenu** can be offset by the Y axis to make the activator visible.

<example file="" />

#### OpenOnHover

Menus can be accessed using hover instead of clicking with the `OpenOnHover` prop.

<example file="" />

#### Rounded

Menus can have their border-radius set by the `Rounded` prop. Additional information about rounded classes is on the
[Border Radius](/stylesandanimations/border-radius) page.

<example file="" />

### Contents

#### Activator and tooltip

With the `RenderFragment` syntax, nested activators such as those seen with a **MMenu** and **MTooltip** attached to the
same activator button, need a particular setup in order to function correctly. Note: this same syntax is used for other
nested activators such as **MDialog** / **MTooltip**.

<example file="" />

### Misc

#### Custom transitions

Masa.Blazor comes with 3 standard transitions, **scale**, **slide-x** and **slide-y**. You can also create your own and
pass it as the transition argument.

<example file="" />

#### Popover menu

A menu can be configured to be static when opened, allowing it to function as a popover. This can be useful when there
are multiple interactive items within the menu contents.

<example file="" />

#### Use In components

Menus can be placed within almost any component.

<example file="" />