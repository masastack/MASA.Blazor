---
title: Avatars
desc: "The **MAvatar** component is usually used to display user profile pictures. This component allows you to dynamically add and set the border radius of response images, icons and text. The `tile` variable can be used to display an avatar with no border radius."
related:
  - /blazor/components/badges
  - /blazor/components/icons
  - /blazor/components/lists
---

## Usage

**MAvatar** in their simplest form display content within a circular container.

<avatars-usage></avatars-usage>

## Anatomy

The recommended placement of elements inside of `MAvatar` is:

* Place a [MImage](blazor/components/images/) or [MIcon](blazor/components/images/) component within the default *slot*
* Place textual content within the default *slot*

![Avatar Anatomy](https://cdn.masastack.com/stack/doc/masablazor/anatomy/avatar-anatomy.png)

| Element / Area | Description |
| - | - |
| 1. Container | The Avatar container that typically holds a [MIcon](blazor/components/icons/) or [MImage](blazor/components/images/) component |

## Examples

### Props

#### Size

The `Size` prop allows you to define the `Height` and `Width` of **MAvatar**. This prop scales both evenly with an aspect ratio of 1. `Height` and `Width` props will override this prop.

<masa-example file="Examples.components.avatars.Size"></masa-example>

#### Tile

The `Tile` prop removes the border radius from **MAvatar** leaving you with a simple square avatar.

<masa-example file="Examples.components.avatars.Tile"></masa-example>

### Slots

#### Default

The **MAvatar** default slot will accept the **MIcon** component, an image, or text. Mix and match these with other props to create something unique.

<masa-example file="Examples.components.avatars.Default"></masa-example>

### Misc

#### Advanced usage

Combine you with other components and you can build beautiful user interfaces.

<masa-example file="Examples.components.avatars.Other"></masa-example>

#### Menu

Example: Combining avatars and menus.

<masa-example file="Examples.components.avatars.Menu"></masa-example>

#### Profile Card

Using the `Tile` prop, we can create a business card.

<masa-example file="Examples.components.avatars.BusinessCard"></masa-example>




