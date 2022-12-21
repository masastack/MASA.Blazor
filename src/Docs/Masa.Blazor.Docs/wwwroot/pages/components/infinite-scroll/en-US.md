---
title: Infinite scroll
desc: "Scrolling to the bottom of the list automatically loads more data."
related:
  - /components/lists
  - /components/progress-circular
  - /components/virtual-scroller
---

## Usage

When the `HasMore` prop is `true`, the component will call the defined `OnLoadMore` function when the user page scrolls to the bottom `Threshold` (default is 250px). Support click to retry when the request fails.

<masa-example file="Examples.components.infinite_scroll.Usage"></masa-example>

## Examples

### Contents

#### Custom content

<masa-example file="Examples.components.infinite_scroll.CustomContent"></masa-example>

### Misc

#### Use VirtualScroller

<masa-example file="Examples.components.infinite_scroll.VirtualScroller"></masa-example>


