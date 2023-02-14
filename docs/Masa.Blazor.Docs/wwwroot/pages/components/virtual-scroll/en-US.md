---
title: Virtual scroller
desc: "The MVirtualScroll component displays a virtual, infinite list. It supports dynamic height and scrolling vertically."
related:
  - /blazor/components/lists
  - /blazor/components/data-tables
  - /blazor/components/data-iterators
--- 

## Anatomy

## Examples

### Props

#### Bench

By default **MVirtualScroll** will not prerender other items that appear outside the viewable range。 Using the **OverscanCount** property will cause the scrollbar to render additional items as substitute。rehydrating it with new data.

<masa-example file="Examples.components.virtual_scroll.Bench"></masa-example>

#### UserDirectory

**MVirtualScroll** component can render an unlimited amount of items by rendering only what it needs to fill the scroller’s viewport，**ItemSize** property to set the pixel height of each item.

<masa-example file="Examples.components.virtual_scroll.UserDirectory"></masa-example>