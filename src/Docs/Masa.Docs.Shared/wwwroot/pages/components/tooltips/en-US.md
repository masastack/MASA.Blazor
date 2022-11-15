---
category: Components
type: Tooltip
title: Tooltips
cols: 1
related:
  - /components/badges
  - /components/icons
  - /components/menus
---

# Tooltips

The **MTooltip** component is useful for conveying information when a user hovers over an element. You can also
programmatically control the display of tooltips through a `@bind-Value`. When activated, tooltips display a text
label identifying an element, such as a description of its function.

## Usage

Tooltips can wrap any element.

<tooltips-usage></tooltips-usage>

## API

- [MTooltip](/api/MTooltip)

## Caveats

<!--alert:info-->
In order to correctly position **MTooltip**, a position support (` top ` | 'bottom ` |' left ` | 'right') is required.

## Examples

### Props

#### Alignment

A tooltip can be aligned to any of the four sides of the activator element.

<example file="" />

#### Visibility

Tooltip visibility can be programmatically changed using `@bind-Value`.

<example file="" />