---
title: Grid System
desc: "Masa.Blazor comes with a 12 point grid system built using flexbox. The grid is used to create specific layouts within an application’s content. It contains 5 types of media breakpoints that are used for targeting specific screen sizes or orientations, xs, sm, md, lg and xl.
These resolutions are defined below in the Viewport Breakpoints table and can be modified by customizing the [Breakpoint service](features/breakpoints)"
related:
  - /stylesandanimations/flex                      
  - /features/breakpoints
  - /stylesandanimations/display-helpers
---

<div
  class="overflow-hidden mb-12 overflow-hidden m-sheet m-sheet--outlined theme--light rounded"
>
  <div class="m-data-table theme--light">
    <div class="m-data-table__wrapper">
      <table>
        <caption class="pa-4">
          Material Design Breakpoints
        </caption>
        <thead>
          <tr class="text-left">
            <th>Device</th>
            <th>Code</th>
            <th>Type</th>
            <th>Range</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>
              <span
                aria-hidden="true"
                class="m-icon notranslate m-icon--left theme--light"
                ><svg
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 24 24"
                  role="img"
                  aria-hidden="true"
                  class="m-icon__svg"
                >
                  <path
                    d="M17,19H7V5H17M17,1H7C5.89,1 5,1.89 5,3V21A2,2 0 0,0 7,23H17A2,2 0 0,0 19,21V3C19,1.89 18.1,1 17,1Z"
                  ></path></svg></span
              ><span>Extra small</span>
            </td>
            <td><strong>xs</strong></td>
            <td>Small to large phone</td>
            <td>&lt; 600px</td>
          </tr>
          <tr>
            <td>
              <span
                aria-hidden="true"
                class="m-icon notranslate m-icon--left theme--light"
                ><svg
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 24 24"
                  role="img"
                  aria-hidden="true"
                  class="m-icon__svg"
                >
                  <path
                    d="M19,18H5V6H19M21,4H3C1.89,4 1,4.89 1,6V18A2,2 0 0,0 3,20H21A2,2 0 0,0 23,18V6C23,4.89 22.1,4 21,4Z"
                  ></path></svg></span
              ><span>Small</span>
            </td>
            <td><strong>sm</strong></td>
            <td>Small to medium tablet</td>
            <td>600px &gt; &lt; 960px</td>
          </tr>
          <tr>
            <td>
              <span
                aria-hidden="true"
                class="m-icon notranslate m-icon--left theme--light"
                ><svg
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 24 24"
                  role="img"
                  aria-hidden="true"
                  class="m-icon__svg"
                >
                  <path
                    d="M4,6H20V16H4M20,18A2,2 0 0,0 22,16V6C22,4.89 21.1,4 20,4H4C2.89,4 2,4.89 2,6V16A2,2 0 0,0 4,18H0V20H24V18H20Z"
                  ></path></svg></span
              ><span>Medium</span>
            </td>
            <td><strong>md</strong></td>
            <td>Large tablet to laptop</td>
            <td>960px &gt; &lt; 1264px*</td>
          </tr>
          <tr>
            <td>
              <span
                aria-hidden="true"
                class="m-icon notranslate m-icon--left theme--light"
                ><svg
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 24 24"
                  role="img"
                  aria-hidden="true"
                  class="m-icon__svg"
                >
                  <path
                    d="M21,16H3V4H21M21,2H3C1.89,2 1,2.89 1,4V16A2,2 0 0,0 3,18H10V20H8V22H16V20H14V18H21A2,2 0 0,0 23,16V4C23,2.89 22.1,2 21,2Z"
                  ></path></svg></span
              ><span>Large</span>
            </td>
            <td><strong>lg</strong></td>
            <td>Desktop</td>
            <td>1264px &gt; &lt; 1904px*</td>
          </tr>
          <tr>
            <td>
              <span
                aria-hidden="true"
                class="m-icon notranslate m-icon--left theme--light"
                ><svg
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 24 24"
                  role="img"
                  aria-hidden="true"
                  class="m-icon__svg"
                >
                  <path
                    d="M21,17H3V5H21M21,3H3A2,2 0 0,0 1,5V17A2,2 0 0,0 3,19H8V21H16V19H21A2,2 0 0,0 23,17V5A2,2 0 0,0 21,3Z"
                  ></path></svg></span
              ><span>Extra large</span>
            </td>
            <td><strong>xl</strong></td>
            <td>4k and ultra-wide</td>
            <td>&gt; 1904px*</td>
          </tr>
        </tbody>
        <tfoot>
          <tr>
            <td colspan="4" class="text-caption text-center grey--text">
              <em>* -16px on desktop for browser scrollbar</em>
            </td>
          </tr>
          <tr>
            <td colspan="4" class="text-right text--secondary">
              <small class="d-block mr-n1 mb-n6"
                ><a
                  href="https://material.io/design/layout/responsive-layout-grid.html"
                  rel="noopener noreferrer"
                  target="_blank"
                  class="text-decoration-none d-inline-flex align-center"
                  ><span
                    aria-hidden="true"
                    class="m-icon notranslate mr-1 theme--light"
                    style="
                      font-size: 16px;
                      height: 16px;
                      width: 16px;
                      color: inherit;
                    "
                    ><svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 24 24"
                      role="img"
                      aria-hidden="true"
                      class="m-icon__svg"
                      style="font-size: 16px; height: 16px; width: 16px"
                    >
                      <path
                        d="M21,12C21,9.97 20.33,8.09 19,6.38V17.63C20.33,15.97 21,14.09 21,12M17.63,19H6.38C7.06,19.55 7.95,20 9.05,20.41C10.14,20.8 11.13,21 12,21C12.88,21 13.86,20.8 14.95,20.41C16.05,20 16.94,19.55 17.63,19M11,17L7,9V17H11M17,9L13,17H17V9M12,14.53L15.75,7H8.25L12,14.53M17.63,5C15.97,3.67 14.09,3 12,3C9.91,3 8.03,3.67 6.38,5H17.63M5,17.63V6.38C3.67,8.09 3,9.97 3,12C3,14.09 3.67,15.97 5,17.63M23,12C23,15.03 21.94,17.63 19.78,19.78C17.63,21.94 15.03,23 12,23C8.97,23 6.38,21.94 4.22,19.78C2.06,17.63 1,15.03 1,12C1,8.97 2.06,6.38 4.22,4.22C6.38,2.06 8.97,1 12,1C15.03,1 17.63,2.06 19.78,4.22C21.94,6.38 23,8.97 23,12Z"
                      ></path></svg></span
                  ><span>规格</span></a
                ></small
              >
            </td>
          </tr>
        </tfoot>
      </table>
    </div>
  </div>
</div>

## Usage

The Masa.Blazor grid is heavily inspired by the [Bootstrap grid](https://getbootstrap.com/docs/4.0/layout/grid/). It is
integrated by using a series of containers, rows, and columns to layout and align content. If you are new to
flexbox, [Read the CSS Tricks flexbox guide](https://css-tricks.com/snippets/css/a-guide-to-flexbox/#flexbox-background)
for background, terminology, guidelines, and code snippets.

<example file="" />

## Sub-components

- **MContainer**：**MContainer** provides the ability to center and horizontally pad your site’s contents. You can also use the `fluid` prop to fully extend the container across all viewport and device sizes.
- **MCol**：**MCol** is a content holder that must be a direct child of `MRow`.
- **MRow**：**MRow** is a wrapper component for **MCol**. It utilizes `Flex` properties to control the layout and flow of its inner columns. It uses a standard gutter of **24px**. This can be reduced with the **Dense** prop or removed completely with `NoGutters`.
- **MSpacer**：**MSpacer**  is a basic yet versatile spacing component used to distribute remaining width in-between a parents child components. When placing a single `MSpacer` before or after the child components, the components will push to the right and left of its container.  When more than one **MSpacer**'s are used between multiple components, the remaining width is evenly distributed between each spacer.

## Helper Classes

`FillHeight` applies `height: 100%` to an element. When applied to **MContainer** it will also `align-items: center`.

## Caveats

<!--alert:info--> 
1.x grid system has been deprecated in favor of the 2.x grid system.
<!--alert:info--> 

<!--alert:info--> 
Breakpoints based props on grid components work in an `andUp` fashion. With this in mind the `xs` breakpoint is assumed and has been removed from the props context. This applies to **offset**、**justify**、**align** and single breakpoint props on `MCol`.

- Props like `justify-sm` and `justify-md` exist,but `justify-xs` does not, it is simply `justify`
- The `xs` prop does not exist on **MCol**. The equivalent to this is the `cols` prop.
<!--alert:info--> 

<!--alert:info--> 
When using the grid system with IE11 you will need to set an explicit `height` as `min-height` will not suffice and cause undesired results.
<!--alert:info--> 

## Examples

### Props

#### Align

Change the vertical alignment of flex items and their parents using the `Align` and `AlignSelf` properties.

<example file="" />

#### BreakpointSizing

Columns will automatically take up an equal amount of space within their parent container. This can be modified using
the `Cols` prop. You can also utilize the `Sm`, `Md`, `Lg`, and `Xl` props to further define how the column will be sized in
different viewport sizes.

<example file="" />

#### Justify

Change the horizontal alignment of flex items using the `Justify` property.

<example file="" />

#### No gutters

You can remove the negative margins from **MRow** and the padding from its direct **MCol** children using the `NoGutters` property.

<example file="" />

#### Offset

Offsets are useful for compensating for elements that may not be visible yet, or to control the position of content.
Just as with breakpoints, you can set an offset for any available sizes. This allows you to fine tune your application
layout precisely to your needs.

<example file="" />

#### OffsetBreakpoint

Offset can also be applied on a per breakpoint basis.

<example file="" />

#### Order

You can control the ordering of grid items. As with offsets, you can set different orders for different sizes. Design
specialized screen layouts that accommodate to any application.

<example file="" />

#### OrderFirstAndLast

You can also designate explicitly `First` or `Last` which will assign `-1` or `13` values respectively to the order CSS property.

<example file="" />

### Misc

#### Column wrapping

When more than 12 columns are placed within a given row (that is not using the `.flex-nowrap` utility class), each group of extra columns will wrap onto a new line.

<example file="" />

#### Equal width columns

You can break equal width columns into multiple lines. While there are workarounds for older browser versions, there was
a [Safari flexbox bug](https://github.com/philipwalton/flexbugs#11-min-and-max-size-declarations-are-ignored-when-wrapping-flex-items)
. This shouldn’t be necessary if you’re up-to-date.

<example file="" />

#### Grow and Shrink

By default, flex components will automatically fill the available space in a row or column. They will also shrink
relative to the rest of the flex items in the flex container when a specific size is not designated. You can define the
column width of the **MCol** by using the cols prop and providing a value from `1 to 12`.

<example file="" />

#### Margin helpers

Using the [auto margin helper utilities](/stylesandanimations/flex) you can force sibling columns away from each other.

<example file="" />

#### Nested grid

Grids can be nested, similar to other frameworks, in order to achieve very custom layouts.

<example file="" />

#### One column width

When using the auto-layout, you can define the width of only one column and still have its siblings to automatically resize around it.

<example file="" />

#### Row and column breakpoints

Dynamically change your layout based upon resolution. **(resize your screen and watch the top `row` layout change on sm, md, and lg breakpoints)**

<example file="" />

#### Spacers

The `MSpacer` component is useful when you want to fill available space or make space between two components.

<example file="" />

#### Unique layouts

The power and flexibility of the Masa.Blazor grid system allows you to create amazing user interfaces.

<example file="" />

#### Variable content width

Assigning breakpoint width for columns can be configured to resize based upon the nature width of their content.

<example file="" />