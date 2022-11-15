---
title: Banners
desc: "The component is used to send intermittent messages of 1-2 actions to the user. It has two variables, single-line and multi-line (implied). These icons can be used with your messages and actions."
related:
  - /components/alerts
  - /components/icons
  - /components/snackbars
---

## Usage

Banners can have 1-2 lines of text, actions and icon.

<banners-usage></banners-usage>

## Anatomy

## Examples

### Props

#### Single line

Single-line MBanner is used for small amount of information and is recommended for desktop only implementations. You can optionally enable the sticky prop to ensure the content is pinned to the screen (note: does not work in IE11). 

<masa-example file="Examples.banners.SingleLine"></masa-example>

### Events

#### Icon click

The icon on the banner emits a click:icon event when clicked, which has a custom icon slot.

<masa-example file="Examples.banners.IconClick"></masa-example>

### Slots

#### Actions

The Actions slot has a dismiss function in its range, and you can use it to hide the banner easily.

<masa-example file="Examples.banners.Actions"></masa-example>

#### Icon

The icon slot allows you to clearly control the content and functions it contains.

<masa-example file="Examples.banners.Icon"></masa-example>

### Misc

#### Two line

Two-line MBanner can store larger amount of data, use it for big messages. This is recommend mobile implementations.

<masa-example file="Examples.banners.TwoLine"></masa-example>
