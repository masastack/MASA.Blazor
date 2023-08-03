---
title: Infinite scroll
desc: "Scrolling to the bottom of the list automatically loads more data."
related:
  - /blazor/components/lists
  - /blazor/components/progress-circular
  - /blazor/components/virtual-scroller
---

## Usage

When the component rendered at the first time, or the user page scrolled to the bottom `Threshold` (default is 250px),
the component will call the `OnLoad` event.

<masa-example file="Examples.components.infinite_scroll.Usage"></masa-example>

Using the `Status` property of the `InfiniteScrollLoadEventArgs` parameter provided by the `OnLoad` event to set the
status of the component.

| Status    | Description                                                                               | Content           |
|-----------|-------------------------------------------------------------------------------------------|-------------------|
| `Ok`      | Content was loaded successfully                                                           | `LoadMoreContent` |
| `Error`   | Something went wrong when loading content                                                 | `ErrorContent`    |
| `Empty`   | There is no more content to load                                                          | `EmptyContent`    |
| `Loading` | Content is currently loading. This status should only be set internally by the component. | `LoadingContent`  |

## Examples

### Props

#### Color

Using `Color` to set the color of the state.

<masa-example file="Examples.components.infinite_scroll.Color"></masa-example>

#### Manual

Automatic loading by default, you can set the `Manual` property to change to manual loading mode.

<masa-example file="Examples.components.infinite_scroll.Manual"></masa-example>

### Contents

#### Custom content for specified status

Through the `ErrorContent`, `LoadingContent`, `LoadMoreContent`, `EmptyContent` slots, you can customize the content of
the specified state.

<masa-example file="Examples.components.infinite_scroll.CustomContent"></masa-example>

#### Custom default content

Or through the default slot, customize the content of the entire component.

<masa-example file="Examples.components.infinite_scroll.ChildContent"></masa-example>

### Misc

#### Use VirtualScroller

<masa-example file="Examples.components.infinite_scroll.VirtualScroller"></masa-example>


