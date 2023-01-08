---
title: Images
desc: "The **MImage** component is packed with features to support rich media."
related:
  - /blazor/components/grid-system
  - /blazor/components/aspect-ratios
  - /blazor/components/parallax
---

## Usage

**MImage** component is used to display a responsive image with lazy-load and placeholder

<images-usage></images-usage>

## Caveats

<app-alert type="info" content="The MImage component uses the Intersect directive which requires a Polyfill for IE11 and Safari. If a browser that does not support this functionality is detected, the image will still load as normal."></app-alert>

## Examples

### Props

#### Width

If you want to change the aspect ratio of the image, you can set the `Contain` property to a fixed aspect ratio.

<masa-example file="Examples.components.images.AspectRatio"></masa-example>

#### Contain

If the provided aspect ratio doesn’t match that of the actual image, the default behavior is to fill as much space as
possible, clipping the sides of the image. Enabling the **Contain** prop will prevent this, but will result in empty space
at the sides.

<masa-example file="Examples.components.images.Contain"></masa-example>

#### Gradients

The `Gradient` prop can be used to apply a simple gradient overlay to the image. More complex gradients should be written
as a class on the content slot instead.

<masa-example file="Examples.components.images.Gradients"></masa-example>

#### Height

**MImage** will automatically grow to the size of its `Src`, preserving the correct aspect ratio. You can limit this
with the `Height` and `MaxHeight` props.

<masa-example file="Examples.components.images.Height"></masa-example>

### Contents

#### Placeholder

**MImage** has a special `PlaceholderContent` slot for placeholder to display while image’s loading. Note: the example
below has bad `Src` which won’t load for you to see placeholder.

<masa-example file="Examples.components.images.Placeholder"></masa-example>

### Misc

#### Grid

You can use **MImage** to make, for example, a picture gallery.

<masa-example file="Examples.components.images.Grid"></masa-example>