---
title: Overlays
desc: "The **MOverlay** component is used to provide emphasis on a particular element or parts of it. It signals to the user of a state change within the application and can be used for creating loaders, dialogs and more."
related:
  - /components/cards
  - /components/sheets
  - /components/dialogs
---

## Usage

In its simplest form, the **MOverlay** component will add a dimmed layer over your application.

<overlays-usage></overlays-usage>


## API

- [MOverlay](/api/MOverlay)

## Examples

### Props

#### Absolute

`Absolute` overlays are positioned absolutely and contained inside of their parent element.

<example file="" />

#### Opacity

`Opacity` allows you to customize the transparency of components.

<example file="" />

#### Z-index

`ZIndex` gives you the ability to easily change the stack order of the **MOverlay** component.

<example file="" />

### Misc

#### Advanced

Using the [MHover](/components/hover), we are able to add a nice scrim over the information card with additional actions the user can take.

<example file="" />

#### Loader

Using the **MOverlay** as a background, add a progress component to easily create a custom loader.

<example file="" />