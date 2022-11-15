---
title: Carousels
desc: "The **MCarousel** component is used to display large numbers of visual content on a rotating timer."
related:
  - /components/parallax
  - /components/images
  - /components/windows
---

## Usage

The **MCarousel** component expands upon MWindow by providing additional features targeted at displaying images.

<carousels-usage></carousels-usage>

## Examples

### Props

#### Custom delimiters

Use any available icon as your carousel¡¯s slide delimiter.

<masa-example file="Examples.carousels.CustomDelimiters"></masa-example>

#### Customized arrows

Arrows can be customized by using **PrevContent** and **NextContent** contents.

<masa-example file="Examples.carousels.CustomizedArrows"></masa-example>

#### Custom transition

The **MCarouselItem** component can have its **transition/reverse-transition** changed.

<masa-example file="Examples.carousels.CustomTransition"></masa-example>

#### Cycle

With the `Cycle` parameter you can have your slides automatically transition to the next available every 6s (default).

<masa-example file="Examples.carousels.Cycle"></masa-example>

#### Hide controls

You can hide the carousel navigation controls with `ShowArrows="false"`.

<masa-example file="Examples.carousels.HideControls"></masa-example>

#### Hide delimiters

You can hide the bottom controls with **HideDelimiters** parameter.

<masa-example file="Examples.carousels.HideDelimiters"></masa-example>

#### Model

You can control carousel with `Value`.

<masa-example file="Examples.carousels.Model"></masa-example>