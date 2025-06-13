---
title: Empty state
desc: "**MEmptyState** component is used to indicate that a list is empty or the search results are empty."
related:
  - /blazor/components/buttons
  - /blazor/components/icons
  - /blazor/components/avatars
---

## Usage

<masa-example file="Examples.components.empty_states.Usage"></masa-example>

## Examples

### Props

#### Content

The three main properties to configure the text content are `Headline`, `Title`, and `Text`.

<masa-example file="Examples.components.empty_states.Content"></masa-example>

#### Media

Add an icon or image to the empty state to help convey its purpose.

<masa-example file="Examples.components.empty_states.Media"></masa-example>

#### Actions

Add action buttons to the empty state so that users can take action.

<masa-example file="Examples.components.empty_states.Actions"></masa-example>

### Contents

#### ChildContent

The default slot is located between **Text** and **Actions**.

<masa-example file="Examples.components.empty_states.ChildContent"></masa-example>

#### TitleContent

<masa-example file="Examples.components.empty_states.TitleContent"></masa-example>

#### ActionsContent

If you need to customize the action buttons, you can use **ActionsContent**.

<masa-example file="Examples.components.empty_states.ActionsContent"></masa-example>


