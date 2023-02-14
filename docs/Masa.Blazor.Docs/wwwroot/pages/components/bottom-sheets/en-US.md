---
title: Bottom sheets
desc: "The bottom sheet is a modified **MDialog** that slides from the bottom of the screen, similar to a **MBottomNavigation**. Whereas a bottom navigation component is for buttons and specific application level actions, a bottom sheet can contain anything."
related:
  - /blazor/components/dialogs
  - /blazor/components/lists
  - /blazor/components/menus
---

## Usage

Here we display an example list of actions that could be present in an application.

<bottom-sheets-usage></bottom-sheets-usage>

## Examples

### Props

#### Inset

Bottom sheets can be inset, reducing their maximum width on desktop to 70%. This can be further reduced manually using the `Width` prop.

<masa-example file="Examples.components.bottom_sheets.Inset"></masa-example>

#### Model

Bottom sheets can be controlled using **@bind-Value**. You can use it to close them or if you can’t use **ActivatorContent** slot.

<masa-example file="Examples.components.bottom_sheets.Model"></masa-example>

#### Persistent

Persistent bottom sheets can’t be closed by clicking outside them.

<masa-example file="Examples.components.bottom_sheets.Persistent"></masa-example>

### Misc

#### Music Player

Using a inset bottom sheet, you can make practical components such as this simple music player.

<masa-example file="Examples.components.bottom_sheets.MusicPlayer"></masa-example>

#### Open In List

By combining a functional list into a bottom sheet, you can create a simple ‘OpEN IN’ component.

<masa-example file="Examples.components.bottom_sheets.OpenInList"></masa-example>



