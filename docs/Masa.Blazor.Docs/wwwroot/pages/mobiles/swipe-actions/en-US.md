---
title: Swipe actions
desc: "Add swipe actions to content, commonly used with the [MCell](/blazor/mobiles/cell) component."
release: v1.11.0
related:
  - /blazor/mobiles/cell
---

## Installation

```shell
dotnet add package Masa.Blazor.MobileComponents
```

## Usage

<masa-example file="Examples.mobiles.swipe_actions.Usage"></masa-example>

## Example

### Props

#### CloseOnClick

The Default behavior is to close automatically on click, suitable for scenarios where user interaction is not required.
In cases where manual closure is needed, set `CloseOnClick` to `false` and use the `Close` method from the provided
`SwipeActionContext`.

<masa-example file="Examples.mobiles.swipe_actions.CloseOnClick"></masa-example>

### Contents

#### ChildContent

You can use any content except the **MCell** component as the default content.
<masa-example file="Examples.mobiles.swipe_actions.ChildContent"></masa-example>

#### LeftContent
<masa-example file="Examples.mobiles.swipe_actions.LeftContent"></masa-example>