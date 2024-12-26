---
title: Tooltip
desc: "The **MTooltip** component is useful for conveying information when a user hovers over an element. You can also programmatically control the display of tooltips through a `@bind-Value`. When activated, tooltips display a text label identifying an element, such as a description of its function."
related:
  - /blazor/components/badges
  - /blazor/components/icons
  - /blazor/components/menus
---

> Starting from v1.9.0, you can use the `Text` property to set a text type tooltip.

## Usage

Tooltips can wrap any element.

<masa-example file="Examples.components.tooltips.Usage"></masa-example>

## Examples

### Props

#### Alignment

A tooltip can be aligned to any of the four sides of the activator element.

<masa-example file="Examples.components.tooltips.Alignment"></masa-example>

#### Visibility

Tooltip visibility can be programmatically changed using `@bind-Value`.

<masa-example file="Examples.components.tooltips.Visibility"></masa-example>