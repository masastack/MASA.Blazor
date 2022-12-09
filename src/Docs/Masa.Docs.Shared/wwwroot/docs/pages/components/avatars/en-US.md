---
title: Avatars
desc: "The **MAvatar** component is usually used to display user profile pictures. This component allows you to dynamically add and set the border radius of response images, icons and text. The `tile` variable can be used to display an avatar with no border radius."
related:
  - /components/badges
  - /components/icons
  - /components/lists
---

## Usage

**MAvatar** in their simplest form display content within a circular container.

<avatars-usage></avatars-usage>

## Anatomy

## API

- [MAvatar](/api/MAvatar)

## Examples

### Props

#### Size

The `Size` prop allows you to define the `Height` and `Width` of **MAvatar**. This prop scales both evenly with an aspect ratio of 1. `Height` and `Width` props will override this prop.

<example file="" />

#### Tile

The `Tile` prop removes the border radius from **MAvatar** leaving you with a simple square avatar.

<example file="" />

### Slots

#### Default

The **MAvatar** default slot will accept the **MIcon** component, an image, or text. Mix and match these props with other props to create something unique.

<example file="" />

### Misc

#### Advanced usage

Combine you with other components and you can build beautiful user interfaces.

<example file="" />

Example: Combining avatars and menus.

<example file="" />

#### Profile Card

Using the `Tile` prop, we can create a business card.

<example file="" />




