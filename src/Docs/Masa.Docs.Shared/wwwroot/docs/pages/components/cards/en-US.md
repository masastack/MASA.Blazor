---
title: Cards
desc: "The MCard component is a versatile component that can be used for anything from a panel to a static image. The card component has numerous helper components to make markup as easy as possible. Components that have no listed options use MASA Blazor functional component option for faster rendering and serve as markup sugar to make building easier."
related:
  - /components/buttons
  - /components/images
  - /stylesandanimations/text-and-typography
---

## 使用

卡中有4个基本组件。 `MCardTitle`, `MCardSubtitle`, `MCardText` 和 `MCardActions`

<cards-usage></cards-usage>

## Anatomy

## SubComponents

#### MCardActions

A container used to place actions for cards, such as [MButton] (/components/buttons) or [MMenu] (/components/menu). Use a special margin on the button at the same time
So that they match other card content areas.

#### MCardSubtitle

Provide default **font size** and **fill** for card subtitles. Font size can be overwritten with [typesetting] (/stylesandimages/text-and-typography).

#### MCardText

It is mainly used for **text content** in cards. Apply padding to the text and reduce its font size to. 875rem.

#### MCardTitle

Provide default font size and padding for card subtitles. Font size can be overwritten with [typesetting] (/stylesandimages/text-and-typography).

## Examples

### Props

#### Loading

Cards can be set to a loading state when processing a user action. This disables further actions and provides visual feedback with an indeterminate  [MProgressLinear](/components/progress-linear)

<example file="" />

#### Outlined

An outlined card has 0 elevation and contains a soft border.

<example file="" />

### Misc

#### Card Reveal

Using [ExpandTransition](/stylesandanimations/transitions) and a @click event you can have a card that reveals more information once the button is clicked, activating the hidden card to be revealed.

<example file="" />

#### Content wrapping

The MCard component is useful for wrapping content.

<example file="" />

#### Custom actions

With a simple conditional, you can easily add supplementary text that is hidden until opened.

<example file="" />

#### Grids

Using grids, you can create beautiful layouts.

<example file="" />

#### Horizontal cards

Using **MCol** , you can create customized horizontal cards. Use the contain property to shrink the MImage to fit inside the container, instead of covering.

<example file="" />

#### Information card

Cards are entry points to more detailed information. To keep things concise, ensure to limit the number of actions the user can take.

<example file="" />