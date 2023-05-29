---
title: Lists
desc: "The **MList** component is used to display information. It can contain an avatar, content, actions, subheaders and much more. Lists present content in a way that makes it easy to identify a specific item in a collection. They provide a consistent styling for organizing groups of text and images." 
related:
  - /blazor/components/item-groups
  - /blazor/components/list-item-groups
  - /blazor/components/subheaders
---

## Usage

Lists come in three main variations. single-line (default), two-line and three-line. The line declaration specifies the
minimum height of the item and can also be controlled from `MList` with the same prop.

<lists-usage></lists-usage>

## Caveats

> If you want the `Href` property of **MListItem** to correlate with the route, you need to apply the `Routable` property on MList.

> If you want to find a list item with status, please check [**MListItemGroup**](/blazor/components/list-item-groups).

## Examples

### Props

#### Dense

`List` can be lowered with `Dense` property.

<masa-example file="Examples.components.lists.Dense"></masa-example>

#### Disabled

You cannot interact with disabled **MList**.

<masa-example file="Examples.components.lists.Disabled"></masa-example>

#### Flat

Items don’t change when selected in **MList** with `Flat` property.

<masa-example file="Examples.components.lists.Flat"></masa-example>

#### Nav

Lists can receive an alternative **Nav** styling that reduces the width **MListitem** takes up as well as adding a border
radius.

<masa-example file="Examples.components.lists.Nav"></masa-example>

#### Rounded

You can make **MList** items rounded.

<masa-example file="Examples.components.lists.Rounded"></masa-example>

#### Shaped

Shaped lists have rounded borders on one side of the **MListItem**.

<masa-example file="Examples.components.lists.ShapedLists"></masa-example>

#### Sub group

Using the **MListGroup** component you can create up to 2 levels in depth using the `SubGroup` prop.

<masa-example file="Examples.components.lists.SubGroup"></masa-example>

#### Three line

For three line lists, the subtitle will clamp vertically at 2 lines and then ellipsis.

<masa-example file="Examples.components.lists.ThreeLine"></masa-example>

#### Two lines and subheader

Lists can contain subheaders, dividers, and can contain 1 or more lines. The subtitle will overflow with ellipsis if it extends past one line.

<masa-example file="Examples.components.lists.TwoLinesAndSubheader"></masa-example>

### Contents

#### Expansion Lists

A list can contain a group of items which will display on click utilizing **MListGroup**'s `ActivatorContent`. Expansion
lists are also used within the [MNavigationDrawer](/blazor/components/navigation-drawers) component.

<masa-example file="Examples.components.lists.ExpansionLists"></masa-example>

### Misc

#### Action and item groups

A **ThreeLine** list with actions. Utilizing [MListItemGroup](/blazor/components/list-item-groups), easily connect actions to your tiles.

<masa-example file="Examples.components.lists.ActionsAndItemGroups"></masa-example>

#### Action stack

A list can contain a stack within an action. This is useful when you need to display meta text next to your action item.

<masa-example file="Examples.components.lists.ActionStack"></masa-example>

#### Card list

A list can be combined with a card.

<masa-example file="Examples.components.lists.CardList"></masa-example>

#### Simple avatar list

A simple list utilizing **MistItemIcon**, **MListItemTitle** and **MListItemAvatar**.

<masa-example file="Examples.components.lists.SimpleAvatarList"></masa-example>

#### Single line list

Here we combine **MListItemAvatar** and **MListItemIcon** in a single-line list.

<masa-example file="Examples.components.lists.SingleLineList"></masa-example>

#### Subheadings and dividers

Lists can contain multiple subheaders and dividers.

<masa-example file="Examples.components.lists.SubheadingsAndDividers"></masa-example>