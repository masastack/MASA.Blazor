---
title: Lists
desc: "The **MList** component is used to display information. It can contain an avatar, content, actions, subheaders and much more. Lists present content in a way that makes it easy to identify a specific item in a collection. They provide a consistent styling for organizing groups of text and images." 
related:
  - /components/item-groups
  - /components/list-item-groups
  - /components/subheaders
---

## Usage

Lists come in three main variations. single-line (default), two-line and three-line. The line declaration specifies the
minimum height of the item and can also be controlled from `MList` with the same prop.

<lists-usage></lists-usage>

## Caveats

<!--alert:info-->
If you want to find a list item with status, please check [**MListItemGroup**](/components/list-item-groups)。

## Examples

### Props

#### Dense

`List` can be lowered with `Dense` property.

<example file="" />

#### Disabled

You cannot interact with disabled **MList**.

<example file="" />

#### Flat

Items don’t change when selected in **MList** with `Flat` property.

<example file="" />

#### Nav

Lists can receive an alternative **Nav** styling that reduces the width **MListitem** takes up as well as adding a border
radius.

<example file="" />

#### Rounded

You can make **MList** items rounded.

<example file="" />

#### Shaped

Shaped lists have rounded borders on one side of the **MListItem**.

<example file="" />

#### Sub group

Using the **MListGroup** component you can create up to 2 levels in depth using the `SubGroup` prop.

<example file="" />

#### Three line

For three line lists, the subtitle will clamp vertically at 2 lines and then ellipsis.

<example file="" />

#### Two lines and subheader

Lists can contain subheaders, dividers, and can contain 1 or more lines. The subtitle will overflow with ellipsis if it extends past one line.

<example file="" />

### Contents

#### Expansion Lists

A list can contain a group of items which will display on click utilizing **MListGroup**'s `ActivatorContent`. Expansion
lists are also used within the [MNavigationDrawer](/components/navigation-drawers) component.

<example file="" />

### Misc

#### Action and item groups

A **ThreeLine** list with actions. Utilizing [**MListItemGroup**](/components/list-item-groups), easily connect actions to your tiles.

<example file="" />

#### Action stack

A list can contain a stack within an action. This is useful when you need to display meta text next to your action item.

<example file="" />

#### Card list

A list can be combined with a card.

<example file="" />

#### Simple avatar list

A simple list utilizing **MistItemIcon**, **MListItemTitle** and **MListItemAvatar**.

<example file="" />

#### Single line list

Here we combine **MListItemAvatar** and **MListItemIcon** in a single-line list.

<example file="" />

#### Subheadings and dividers

Lists can contain multiple subheaders and dividers.

<example file="" />