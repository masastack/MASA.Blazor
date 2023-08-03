---
title: Overlays
desc: "The **MOverlay** component is used to provide emphasis on a particular element or parts of it. It signals to the user of a state change within the application and can be used for creating loaders, dialogs and more."
related:
  - /blazor/components/cards
  - /blazor/components/sheets
  - /blazor/components/dialogs
---

## Usage

In its simplest form, the **MOverlay** component will add a dimmed layer over your application.

<masa-example file="Examples.components.overlays.Usage"></masa-example>

## Examples

### Props

#### Contained

A `Contained` overlay is positioned absolutely and contained inside of its parent element.

<masa-example file="Examples.components.overlays.Contained"></masa-example>

#### Opacity

`Opacity` allows you to customize the transparency of components.

<masa-example file="Examples.components.overlays.Opacity"></masa-example>

### Misc

#### Advanced

Using the [MHover](/blazor/components/hover), we are able to add a nice scrim over the information card with additional actions the user can take.

<masa-example file="Examples.components.overlays.Advanced"></masa-example>

#### Loader

Using the **MOverlay** as a background, add a progress component to easily create a custom loader.

<masa-example file="Examples.components.overlays.Loader"></masa-example>