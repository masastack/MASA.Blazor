---
title: Bottom sheets
desc: "The bottom sheet is a modified `MDialog` that slides from the bottom of the screen, similar to a `MBottomNavigation`. Whereas a bottom navigation component is for buttons and specific application level actions, a bottom sheet can contain anything."
related:
  - /components/dialogs
  - /components/lists
  - /components/menus
---

## Usage

Here we display an example list of actions that could be present in an application.

<bottom-sheets-usage></bottom-sheets-usage>

## API

- [MBottomSheet](/api/MBottomSheet)

## Examples

### Props

#### Inset

Bottom sheets can be inset, reducing their maximum width on desktop to 70%. This can be further reduced manually using the `Width` prop.

<example file="" />

#### Model

Bottom sheets can be controlled using **@bind-Value**. You can use it to close them or if you can’t use **ActivatorContent** slot.

<example file="" />

#### Persistent

Persistent bottom sheets can’t be closed by clicking outside them.

<example file="" />

### Misc

#### Music Player

Using a inset bottom sheet, you can make practical components such as this simple music player.

<example file="" />

#### Open In List

By combining a functional list into a bottom sheet, you can create a simple ‘OpEN IN’ component.

<example file="" />



