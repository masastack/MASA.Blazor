﻿---
title: Sticky
desc: Make elements stick of the viewport when scrolling. 
---

> The native `position: sticky` can only be applied to the nearest scrolling container, while the **MSticky** component can specify the scrolling container and get the state to achieve more functions. But its performance is not as good as the native `position: sticky`.

## Examples

### Props

#### Disabled

<masa-example file="Examples.labs.sticky.Disabled"></masa-example>

#### Offset

Set the `OffsetTop` property to make the element distance from the top when fixed, set the `OffsetBottom` property to make the element distance from the bottom when fixed, in pixels.

<masa-example file="Examples.labs.sticky.Offset"></masa-example>

#### Scroll target

By default, the container that listens for scrolling is the window. You can set the `ScrollTarget` property to customize the scrolling container.

<masa-example file="Examples.labs.sticky.ScrollTarget"></masa-example>

### Contents

#### ChildContent

Provide a `bool` type context to determine whether the element is fixed.

<masa-example file="Examples.labs.sticky.ChildContent"></masa-example>

