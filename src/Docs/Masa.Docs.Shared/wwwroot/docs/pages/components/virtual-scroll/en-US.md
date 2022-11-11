---
category: Components
type: VirtualScroll
title: Virtual scroller
cols: 1
related:
  - /components/lists
  - /components/data-tables
  - /components/data-iterators
--- 

# Virtual scroller

The MVirtualScroll component displays a virtual, infinite list. It supports dynamic height and scrolling vertically.

## Usage

<virtual-scroll-usage></virtual-scroll-usage>

## Anatomy

## API

- [MVirtualScroll](/api/MVirtualScroll)

## Examples

### Props

#### Bench

By default **MvirtualScroll** will not prerender other items that appear outside the viewable range。 Using the **OverscanCount** property will cause the scrollbar to render additional items as substitute。rehydrating it with new data
。

<example file="" />

#### UserDirectory

**MvirtualScroll** component can render an unlimited amount of items by rendering only what it needs to fill the scroller’s viewport，**ItemSize** property to set the pixel height of each item。

<example file="" />