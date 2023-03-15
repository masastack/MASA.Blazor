---
title: Cards
desc: "The MCard component is a versatile component that can be used for anything from a panel to a static image. The card component has numerous helper components to make markup as easy as possible. Components that have no listed options use MASA Blazor functional component option for faster rendering and serve as markup sugar to make building easier."
related:
  - /blazor/components/buttons
  - /blazor/components/images
  - /blazor/styles-and-animations/text-and-typography
---

## Usage

A card has four basic components, `MCardTitle`, `MCardSubtitle`, `MCardText` 和 `MCardActions`.

<cards-usage></cards-usage>

## Anatomy

The recommended placement of elements inside of `MCard` is:

* Place `MCardTitle`, `MCardSubtitle` or other title text on top
* Place `MCardText` and other forms of media below the card header
* Place `MCardActions` after card content

![Card Anatomy](https://cdn.masastack.com/stack/doc/masablazor/anatomy/card-anatomy.png)

| Element / Area | Description |
| - | - |
| 1. Container | The Card container holds all `MCard` components. Composed of 3 major parts: `MCardItem`, `MCardText`, and `MCardActions` |
| 2. Title (optional) | A heading with increased **font-size** |
| 3. Subtitle (optional) | A subheading with a lower emphasis text color |
| 4. Text (optional) | A content area with a lower emphasis text color |
| 5. Actions (optional) | A content area that typically contains one or more [MButton](blazor/components/buttons) components |

## SubComponents

#### MCardActions

A container used to place actions for cards, such as [MButton] (/components/buttons) or [MMenu] (/components/menu). Use a special margin on the button at the same time
So that they match other card content areas.

#### MCardSubtitle

Provide default **font size** and **fill** for card subtitles. Font size can be overwritten with [typesetting] (/stylesandimages/text-and-typography).

#### MCardText

Primarily used for **text content** in a card. Applies padding for text, reduces its font-size to .875rem.

#### MCardTitle

Provide default **font size** and **padding** for card subtitles. Font size can be overwritten with [typesetting] (/stylesandimages/text-and-typography).

## Examples

### Props

#### Loading

Cards can be set to a loading state when processing a user action. This disables further actions and provides visual feedback with an indeterminate  [MProgressLinear](/blazor/components/progress-linear)

<masa-example file="Examples.components.cards.Loading"></masa-example>

#### Outlined

An outlined card has 0 elevation and contains a soft border.

<masa-example file="Examples.components.cards.Outlined"></masa-example>

### Misc

#### Card Reveal

Using [ExpandTransition](/blazor/styles-and-animations/transitions) and a @click event you can have a card that reveals more information once the button is clicked, activating the hidden card to be revealed.

<masa-example file="Examples.components.cards.CardReveal"></masa-example>

#### Content wrapping

The MCard component is useful for wrapping content.

<masa-example file="Examples.components.cards.ContentWrapping"></masa-example>

#### Custom actions

With a simple conditional, you can easily add supplementary text that is hidden until opened.

<masa-example file="Examples.components.cards.CustomActions"></masa-example>

#### Grids

Using grids, you can create beautiful layouts.

<masa-example file="Examples.components.cards.Grids"></masa-example>

#### Horizontal cards

Using **MCol** , you can create customized horizontal cards. Use the contain property to shrink the MImage to fit inside the container, instead of covering.

<masa-example file="Examples.components.cards.HorizontalCards"></masa-example>

#### Information card

Cards are entry points to more detailed information. To keep things concise, ensure to limit the number of actions the user can take.

<masa-example file="Examples.components.cards.InformationCard"></masa-example>

#### Twitter card

The **MCard** component has multiple children components that help you build complex examples without having to worry about spacing. This example is comprised of the **MCardTitle**, **MCardText** and **MCardActions** components.

<masa-example file="Examples.components.cards.TwitterCard"></masa-example>

#### Weather card

Using [MListItems](/blazor/components/lists) and a [MSlider](/blazor/components/sliders), we are able to create a unique weather card. The list components ensure that we have consistent spacing and functionality while the slider component allows us to provide a useful interface of selection to the user.

<masa-example file="Examples.components.cards.WeatherCard"></masa-example>

#### Loading

Use an indeterminate [MProgressLinear](/blazor/components/progress-linear) to indicate a loading state.

<masa-example file="Examples.components.cards.Loading"></masa-example>