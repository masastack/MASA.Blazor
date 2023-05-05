# Display helpers（显示辅助）

The display helpers allow you to control the display of content. This includes being conditionally visible based upon the current viewport, or the actual element display type.

## 示例

### 其他

#### 显示

<div
  class="overflow-hidden mb-12 overflow-hidden m-sheet m-sheet--outlined theme--light rounded"
>
  <div class="m-data-table theme--light">
    <div class="m-data-table__wrapper">
      <table>
        <caption class="pa-4">
          Material Design 断点
        </caption>
        <thead>
          <tr class="text-left">
            <th>设备</th>
            <th>代码</th>
            <th>类型</th>
            <th>像素范围</th>
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
              ><span>Extra small (超小号)</span>
            </td>
            <td><strong>xs</strong></td>
            <td>小型号到大型号的手机</td>
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
              ><span>Small (小号)</span>
            </td>
            <td><strong>sm</strong></td>
            <td>小型号到中型号的平板</td>
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
              ><span>Medium (中号)</span>
            </td>
            <td><strong>md</strong></td>
            <td>大型号平板到手提电脑</td>
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
              ><span>Large (大号)</span>
            </td>
            <td><strong>lg</strong></td>
            <td>桌面端</td>
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
              ><span>Extra large (超大号)</span>
            </td>
            <td><strong>xl</strong></td>
            <td>4K 和超宽屏幕</td>
            <td>&gt; 1904px*</td>
          </tr>
        </tbody>
        <tfoot>
          <tr>
            <td colspan="4" class="text-caption text-center grey--text">
              <em>桌面端上浏览器滚动条的宽度为 * -16px </em>
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

当为显示辅助类设置一个特定断点时，它将应用于所有屏幕从指定值开始的宽度。 例如， `d-lg-flex` 将适用于 `lg` 和 `xl` 的屏幕尺寸。

<masa-example file="Examples.styles_and_animations.display_helpers.Show"></masa-example>

#### Visibility

根据当前 **viewport** 的宽度上限有条件的显示元素。 断点实用类始终自下而上应用。 这意味着如果你有 `.d-none`, 它将应用于所有断点。 然而， `.d-md-none` 将仅应用于 `md` 及以上。

您还可以使用横向显示辅助类基于当前 **viewport** 宽度上限来显示元素。 这些类可以使用以下格式 `hidden-{breakpoint}-{condition}` 使用。

基于以下 条件 应用类:

1.`only` - 只在 `xs` 至 `xl` 断点隐藏元素

2.`and down` - 在指定的断点和以下隐藏元素, 从 sm 到 lg 断点

3.`and down` - 在指定的断点和以上隐藏元素, 从 sm 到 lg 断点

此外, 可以使用 `only` 条件确定目标 媒体类型 。 目前支持 `hidden-screen-only` 和 `hidden-print-only`

<div
  class="overflow-hidden mb-4 m-sheet m-sheet--outlined theme--light rounded"
>
  <div class="m-data-table theme--light">
    <div class="m-data-table__wrapper">
      <table>
        <thead>
          <tr>
            <th>屏幕大小</th>
            <th>类</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>全部隐藏</td>
            <td><code>.d-none</code></td>
          </tr>
          <tr>
            <td>仅在 xs 大小时隐藏</td>
            <td><code>.d-none .d-sm-flex</code></td>
          </tr>
          <tr>
            <td>仅在 sm 大小时隐藏</td>
            <td><code>.d-sm-none .d-md-flex</code></td>
          </tr>
          <tr>
            <td>仅在 md 大小时隐藏</td>
            <td><code>.d-md-none .d-lg-flex</code></td>
          </tr>
          <tr>
            <td>仅在 lg 大小时隐藏</td>
            <td><code>.d-lg-none .d-xl-flex</code></td>
          </tr>
          <tr>
            <td>仅在 xl 大小时隐藏</td>
            <td><code>.d-xl-none</code></td>
          </tr>
          <tr>
            <td>全部可见</td>
            <td><code>.d-flex</code></td>
          </tr>
          <tr>
            <td>仅在 xs 大小时可见</td>
            <td><code>.d-flex .d-sm-none</code></td>
          </tr>
          <tr>
            <td>仅在 sm 大小时可见</td>
            <td><code>.d-none .d-sm-flex .d-md-none</code></td>
          </tr>
          <tr>
            <td>仅在 md 大小时可见</td>
            <td><code>.d-none .d-md-flex .d-lg-none</code></td>
          </tr>
          <tr>
            <td>仅在 lg 大小时可见</td>
            <td><code>.d-none .d-lg-flex .d-xl-none</code></td>
          </tr>
          <tr>
            <td>仅在 xl 大小时可见</td>
            <td><code>.d-none .d-xl-flex</code></td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</div>

<masa-example file="Examples.styles_and_animations.display_helpers.Visibility"></masa-example>

#### 打印显示

你还可以在打印时更改显示属性。

* `.d-print-none`
* `.d-print-inline`
* `.d-print-inline-block`
* `.d-print-block`
* `.d-print-table`
* `.d-print-table-row`
* `.d-print-table-cell`
* `.d-print-flex`
* `.d-print-inline-flex`

打印功能类也可以与无打印显示功能组合。

<masa-example file="Examples.styles_and_animations.display_helpers.PrintDisplay"></masa-example>

