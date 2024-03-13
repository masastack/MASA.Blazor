---
title: Grid System
desc: "Masa.Blazor comes with a 12 point grid system built using flexbox. The grid is used to create specific layouts within an application’s content. It contains 5 types of media breakpoints that are used for targeting specific screen sizes or orientations, xs, sm, md, lg and xl. These resolutions are defined below in the Viewport Breakpoints table and can be modified by customizing the [Breakpoint service](/blazor/features/breakpoints)"
related:
  - /blazor/styles-and-animations/flex                      
  - /blazor/features/breakpoints
  - /blazor/styles-and-animations/display-helpers
---

<breakpoint-table></breakpoint-table>

## Usage

The Masa.Blazor grid is heavily inspired by the [Bootstrap grid](https://getbootstrap.com/docs/4.0/layout/grid/). It is
integrated by using a series of containers, rows, and columns to layout and align content. If you are new to
flexbox, [Read the CSS Tricks flexbox guide](https://css-tricks.com/snippets/css/a-guide-to-flexbox/#flexbox-background)
for background, terminology, guidelines, and code snippets.

<masa-example file="Examples.components.grids.Usage"></masa-example>

## Sub-components

- **MContainer**：**MContainer** provides the ability to center and horizontally pad your site’s contents. You can also use the `fluid` prop to fully extend the container across all viewport and device sizes.
- **MCol**：**MCol** is a content holder that must be a direct child of `MRow`.
- **MRow**：**MRow** is a wrapper component for **MCol**. It utilizes `Flex` properties to control the layout and flow of its inner columns. It uses a standard gutter of **24px**. This can be reduced with the **Dense** prop or removed completely with `NoGutters`.
- **MSpacer**：**MSpacer**  is a basic yet versatile spacing component used to distribute remaining width in-between a parents child components. When placing a single `MSpacer` before or after the child components, the components will push to the right and left of its container.  When more than one **MSpacer**'s are used between multiple components, the remaining width is evenly distributed between each spacer.

## Helper Classes

`fill-height` applies `height: 100%` to an element. When applied to **MContainer** it will also `align-items: center`.

## Caveats

<app-alert type="info" content="Breakpoints based props on grid components work in an `andUp` fashion. With this in mind the `xs` breakpoint is assumed and has been removed from the props context. This applies to **offset**、**justify**、**align** and single breakpoint props on `MCol`.For example: 1、Props like `justify-sm` and `justify-md` exist,but `justify-xs` does not, it is simply `justify`. 2、The `Xs` prop does not exist on **MCol**. The equivalent to this is the `Cols` prop."></app-alert>

<app-alert type="info" content="When using the grid system with IE11 you will need to set an explicit `height` as `min-height` will not suffice and cause undesired results."></app-alert>

## Examples

### Props

#### Align

Change the vertical alignment of flex items and their parents using the `Align` and `AlignSelf` properties.

<masa-example file="Examples.components.grids.Align"></masa-example>

#### BreakpointSizing

Columns will automatically take up an equal amount of space within their parent container. This can be modified using
the `Cols` prop. You can also utilize the `Sm`, `Md`, `Lg`, and `Xl` props to further define how the column will be sized in
different viewport sizes.

<masa-example file="Examples.components.grids.BreakpointSizing"></masa-example>

#### Justify

Change the horizontal alignment of flex items using the `Justify` property.

<masa-example file="Examples.components.grids.Justify"></masa-example>

#### No gutters

You can remove the negative margins from **MRow** and the padding from its direct **MCol** children using the `NoGutters` property.

<masa-example file="Examples.components.grids.NoGutters"></masa-example>

#### Offset

Offsets are useful for compensating for elements that may not be visible yet, or to control the position of content.
Just as with breakpoints, you can set an offset for any available sizes. This allows you to fine tune your application
layout precisely to your needs.

<masa-example file="Examples.components.grids.Offset"></masa-example>

#### OffsetBreakpoint

Offset can also be applied on a per breakpoint basis.

<masa-example file="Examples.components.grids.OffsetBreakpoint"></masa-example>

#### Order

You can control the ordering of grid items. As with offsets, you can set different orders for different sizes. Design
specialized screen layouts that accommodate to any application.

<masa-example file="Examples.components.grids.Order"></masa-example>

#### OrderFirstAndLast

You can also designate explicitly `First` or `Last` which will assign `-1` or `13` values respectively to the order CSS property.

<masa-example file="Examples.components.grids.OrderFirstAndLast"></masa-example>

### Misc

#### Column wrapping

When more than 12 columns are placed within a given row (that is not using the `.flex-nowrap` utility class), each group of extra columns will wrap onto a new line.

<masa-example file="Examples.components.grids.ColumnWrapping"></masa-example>

#### Equal width columns

You can break equal width columns into multiple lines. While there are workarounds for older browser versions, there was
a [Safari flexbox bug](https://github.com/philipwalton/flexbugs#11-min-and-max-size-declarations-are-ignored-when-wrapping-flex-items)
. This shouldn’t be necessary if you’re up-to-date.

<masa-example file="Examples.components.grids.EqualWidthColumns"></masa-example>

#### Grow and Shrink

By default, flex components will automatically fill the available space in a row or column. They will also shrink
relative to the rest of the flex items in the flex container when a specific size is not designated. You can define the
column width of the **MCol** by using the cols prop and providing a value from `1 to 12`.

<masa-example file="Examples.components.grids.GrowAndShrink"></masa-example>

#### Margin helpers

Using the [auto margin helper utilities](/blazor/styles-and-animations/flex) you can force sibling columns away from each other.

<masa-example file="Examples.components.grids.MarginHelpers"></masa-example>

#### Nested grid

Grids can be nested, similar to other frameworks, in order to achieve very custom layouts.

<masa-example file="Examples.components.grids.Nested"></masa-example>

#### One column width

When using the auto-layout, you can define the width of only one column and still have its siblings to automatically resize around it.

<masa-example file="Examples.components.grids.OneColumnWidth"></masa-example>

#### Row and column breakpoints

Dynamically change your layout based upon resolution. **(resize your screen and watch the top `row` layout change on sm, md, and lg breakpoints)**

<masa-example file="Examples.components.grids.RowAndColumnBreakpoints"></masa-example>

#### Spacers

The `MSpacer` component is useful when you want to fill available space or make space between two components.

<masa-example file="Examples.components.grids.Spacers"></masa-example>

#### Unique layouts

The power and flexibility of the Masa.Blazor grid system allows you to create amazing user interfaces.

<masa-example file="Examples.components.grids.UniqueLayouts"></masa-example>

#### Variable content width

Assigning breakpoint width for columns can be configured to resize based upon the nature width of their content.

<masa-example file="Examples.components.grids.VariableContentWidth"></masa-example>
