---
title: Hover
desc: "The **MHover** component provides a clean interface for handling the hover state of any component."
related:
  - /blazor/components/cards
  - /blazor/components/images
  - /blazor/components/tooltips
---

## Usage

The **MHover** component is a wrapper that should contain only one child element, and can trigger an event when hovered over.

<hover-usage></hover-usage>

## Examples

### Props

#### Disabled

Set the `Disabled` property to disable the hover function.

<masa-example file="Examples.components.hover.Disabled"></masa-example>

#### Open

Delay the **MHover** event by combining or using the `OpenDelay` and `CloseDelay` properties alone.

<masa-example file="Examples.components.hover.Open"></masa-example>

### Misc

#### List

**MHover** can be used in conjunction with for to highlight individual items when the user interacts with the list.

<masa-example file="Examples.components.hover.List"></masa-example>

#### Transition

Create highly customized components in response to user interaction.

<masa-example file="Examples.components.hover.Transition"></masa-example>