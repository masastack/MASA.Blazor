# Spacing

Do not update your layout by creating new classes. Auxiliary auxiliary classes modify the filling and edges of elements.

## Start

**How ​​to run**

The **margin** or **padding** range of the auxiliary class applied to the element is from 0 to 16. Each size increment is consistent with the Material Design margin specification. These classes can be passed `{property}{direction}- {size}` format used.

**property** Application spacing type:

* `m`-apply `margin`
* `p`-apply `padding`

**direction** specifies the side to which this attribute applies:

* `t`-apply the spacing between `margin-top` and `padding-top`
* `b`-Apply the spacing between `margin-bottom` and `padding-bottom`
* `l`-apply the spacing between `margin-left` and `padding-left`
* `r`-apply the spacing between `margin-right` and `padding-right`
* `s`-Apply the spacing between `margin-left/padding-left` (LTR mode) and `margin-right/padding-right (RTL mode)`
* `e`-Apply the spacing between `margin-right/padding-right` (LTR mode) and `margin-left/padding-left (RTL mode)`
* `x`-apply the spacing between `*-left` and `*-right`
* `y`-apply the spacing between `*-top` and `*-bottom`
* `a`-apply the spacing in all directions

**size** Controls the spacing properties in 4px increments:

* `0`-Set to 0 to eliminate all `margin` or `padding`.
* `1`-Set `margin` or `padding` to 4px
* `2`-Set `margin` or `padding` to 8px
* `3`-Set `margin` or `padding` to 12px
* `4`-Set `margin` or `padding` to 16px
* `5`-Set `margin` or `padding` to 20px
* `6`-Set `margin` or `padding` to 24px
* `7`-Set `margin` or `padding` to 28px
* `8`-Set `margin` or `padding` to 32px
* `9`-Set `margin` or `padding` to 36px
* `10`-Set `margin` or `padding` to 40px
* `11`-Set `margin` or `padding` to 44px
* `12`-Set `margin` or `padding` to 48px
* `13`-Set `margin` or `padding` to 52px
* `14`-Set `margin` or `padding` to 56px
* `15`-Set `margin` or `padding` to 60px
* `16`-Set `margin` or `padding` to 64px
* `n1`-set `margin` to -4px
* `n2`-set `margin` to -8px
* `n3`-set `margin` to -12px
* `n4`-set `margin` to -16px
* `n5`-set `margin` to -20px
* `n6`-set `margin` to -24px
* `n7`-set `margin` to -28px
* `n8`-set `margin` to -32px
* `n9`-set `margin` to -36px
* `n10`-set `margin` to -40px
* `n11`-set `margin` to -44px
* `n12`-set `margin` to -48px
* `n13`-set `margin` to -52px
* `n14`-set `margin` to -56px
* `n15`-set `margin` to -60px
* `n16`-set `margin` to -64px
* `auto`-set the spacing to auto

Use the playground to learn what the different helper classes can do. To understand **how they work**, see the section below.

<masa-example file="Examples.styles_and_animations.spacing.Start"></masa-example>

### Misc

#### Breakpoints

MASA Blazor uses Flexbox to construct a 12-point grid system. Spacing is used to create a specific layout of application content. It contains 5 media breakpoint queries to determine the size or orientation of a specific screen: **xs**, **sm* *, **md**, **lg** and **xl**. The default resolution is defined in the view breakpoint table below.

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
              <em> * -16px on desktop for browser scrollbar</em>
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
                  ><span>Specification</span></a
                ></small
              >
            </td>
          </tr>
        </tfoot>
      </table>
    </div>
  </div>
</div>

The helper class applies **margin** or **padding** at a given breakpoint. These classes can be used in the format `{property}{direction}-{breakpoint}-{size}`. This does not apply to **xs** because it is inferred; for example: `ma-xs-2` is equal to `ma-2`.

<masa-example file="Examples.styles_and_animations.spacing.Breakpoints"></masa-example>

#### Horizontal

Using the margin helper classes you can easily center content horizontally.

<masa-example file="Examples.styles_and_animations.spacing.Horizontal"></masa-example>

#### Margin

It is also possible to use negative margins ranging from **1** to **16**.

<masa-example file="Examples.styles_and_animations.spacing.Margin"></masa-example>