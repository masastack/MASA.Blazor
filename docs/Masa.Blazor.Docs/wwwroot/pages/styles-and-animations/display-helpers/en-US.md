# Display helpers

The display helpers allow you to control the display of content. This includes being conditionally visible based upon the current viewport, or the actual element display type.


## Examples

### Misc

#### Show

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

For example, `d-lg-flex` will work for screen sizes of `lg` and `xl`.

<masa-example file="Examples.styles_and_animations.display_helpers.Show"></masa-example>

#### Visibility

Conditionally display elements based on the current maximum width of **viewport**. Breakpoint utility classes are always applied from the bottom up. This means that if you have `.d-none`, it will be applied to all breakpoints. However, `.d-md-none` will only be applied to `md` and above.

You can also use the horizontal display helper class to display elements based on the current maximum width of the **viewport**. These classes can be used in the following format `hidden-{breakpoint}-{condition}`.

The application class is based on the following conditions:

1. `only`-hide elements only at breakpoints from `xs` to `xl`

2.`and down`-hide elements at the specified breakpoint and below, from sm to lg breakpoint

3.`and down`-hide elements at the specified breakpoint and above, from sm to lg breakpoint

In addition, you can use the `only` condition to determine the target media type. Currently supports `hidden-screen-only` and `hidden-print-only`

<div
  class="overflow-hidden mb-4 m-sheet m-sheet--outlined theme--light rounded"
>
  <div class="m-data-table theme--light">
    <div class="m-data-table__wrapper">
      <table>
        <thead>
          <tr>
            <th>Screen size</th>
            <th>Class</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>Hidden on all</td>
            <td><code>.d-none</code></td>
          </tr>
          <tr>
            <td>Hidden only on xs</td>
            <td><code>.d-none .d-sm-flex</code></td>
          </tr>
          <tr>
            <td>Hidden only on sm</td>
            <td><code>.d-sm-none .d-md-flex</code></td>
          </tr>
          <tr>
            <td>Hidden only on md</td>
            <td><code>.d-md-none .d-lg-flex</code></td>
          </tr>
          <tr>
            <td>Hidden only on lg</td>
            <td><code>.d-lg-none .d-xl-flex</code></td>
          </tr>
          <tr>
            <td>Hidden only on xl</td>
            <td><code>.d-xl-none</code></td>
          </tr>
          <tr>
            <td>Visible on all</td>
            <td><code>.d-flex</code></td>
          </tr>
          <tr>
            <td>Visible only on xs</td>
            <td><code>.d-flex .d-sm-none</code></td>
          </tr>
          <tr>
            <td>Visible only on sm</td>
            <td><code>.d-none .d-sm-flex .d-md-none</code></td>
          </tr>
          <tr>
            <td>Visible only on md</td>
            <td><code>.d-none .d-md-flex .d-lg-none</code></td>
          </tr>
          <tr>
            <td>Visible only on lg</td>
            <td><code>.d-none .d-lg-flex .d-xl-none</code></td>
          </tr>
          <tr>
            <td>Visible only on xl</td>
            <td><code>.d-none .d-xl-flex</code></td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</div>

<masa-example file="Examples.styles_and_animations.display_helpers.Visibility"></masa-example>

#### PrintDisplay

You can also change the display properties while printing.

* `.d-print-none`
* `.d-print-inline`
* `.d-print-inline-block`
* `.d-print-block`
* `.d-print-table`
* `.d-print-table-row`
* `.d-print-table-cell`
* `.d-print-flex`
* `.d-print-inline-flex`

The printing function class can also be combined with the non-printing display function.

<masa-example file="Examples.styles_and_animations.display_helpers.PrintDisplay"></masa-example>