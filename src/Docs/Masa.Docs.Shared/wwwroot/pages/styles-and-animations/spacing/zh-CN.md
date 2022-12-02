# Spacing（间距）

不通过创建新类的方式来更新您的布局. 间距辅助类在修改元素的 padding 和 margin 时非常有用。

## 使用

**如何运行**

辅助类应用到元素的 **margin** 或 **padding** 范围是从 0 到 16. 每个尺寸增量都与 Material Design 边距规范一致. 这些类可以通过 `{property}{direction}-{size}` 格式使用。

**property** 应用间距类型:

* `m` - 应用 `margin`
* `p` - 应用 `padding`

**direction** 指定了该属性所应用的侧边:

* `t` - 应用 `margin-top` 和 `padding-top` 的间距
* `b` - 应用 `margin-bottom` 和 `padding-bottom` 的间距
* `l` - 应用 `margin-left` 和 `padding-left` 的间距
* `r` - 应用 `margin-right` 和 `padding-right` 的间距
* `s` - 应用 `margin-left/padding-left` (LTR模式) 和 `margin-right/padding-right(RTL模式)` 的间距
* `e` - 应用 `margin-right/padding-right` (LTR模式) 和 `margin-left/padding-left(RTL模式)` 的间距
* `x` - 应用 `*-left` 和 `*-right` 的间距
* `y` - 应用 `*-top` 和 `*-bottom` 的间距
* `a` - 在所有方向应用该间距

**size** 以4px增量控制间距属性:

* `0` - 通过设置为 0 来消除所有 `margin` 或 `padding`.
* `1` - 设置 `margin` 或 `padding` 为 4px
* `2` - 设置 `margin` 或 `padding` 为 8px
* `3` - 设置 `margin` 或 `padding` 为 12px
* `4` - 设置 `margin` 或 `padding` 为 16px
* `5` - 设置 `margin` 或 `padding` 为 20px
* `6` - 设置 `margin` 或 `padding` 为 24px
* `7` - 设置 `margin` 或 `padding` 为 28px
* `8` - 设置 `margin` 或 `padding` 为 32px
* `9` - 设置 `margin` 或 `padding` 为 36px
* `10` - 设置 `margin` 或 `padding` 为 40px
* `11` - 设置 `margin` 或 `padding` 为 44px
* `12` - 设置 `margin` 或 `padding` 为 48px
* `13` - 设置 `margin` 或 `padding` 为 52px
* `14` - 设置 `margin` 或 `padding` 为 56px
* `15` - 设置 `margin` 或 `padding` 为 60px
* `16` - 设置 `margin` 或 `padding` 为 64px
* `n1` - 设置 `margin` 为 -4px
* `n2` - 设置 `margin` 为 -8px
* `n3` - 设置 `margin` 为 -12px
* `n4` - 设置 `margin` 为 -16px
* `n5` - 设置 `margin` 为 -20px
* `n6` - 设置 `margin` 为 -24px
* `n7` - 设置 `margin` 为 -28px
* `n8` - 设置 `margin` 为 -32px
* `n9` - 设置 `margin` 为 -36px
* `n10` - 设置 `margin` 为 -40px
* `n11` - 设置 `margin` 为 -44px
* `n12` - 设置 `margin` 为 -48px
* `n13` - 设置 `margin` 为 -52px
* `n14` - 设置 `margin` 为 -56px
* `n15` - 设置 `margin` 为 -60px
* `n16` - 设置 `margin` 为 -64px
* `auto` - 设置间距为 auto

使用 playground 来了解不同的辅助类能做些什么. 要了解**他们如何工作**，请参阅下面的部分。

<masa-example file="Examples.styles_and_animations.spacing.Start"></masa-example>

### 其他

#### 断点

MASA Blazor 使用 Flexbox 通过12点网格系统建构. 间距用来创建应用程序内容的特定布局. 它包含5个媒体断点查询来确定特定屏幕的大小或方向: **xs**, **sm**, **md**, **lg** 和 **xl**. 下面的 视图断点 表中定义了默认解析。

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

助手类在给定的断点应用 **margin** 或 **padding**。 这些类可以通过 `{property}{direction}-{breakpoint}-{size}` 格式使用。 这不适用于 **xs** ,因为它是推断出来的; 例如: `ma-xs-2` 等于 `ma-2`.

<masa-example file="Examples.styles_and_animations.spacing.Breakpoints"></masa-example>

#### 水平布局

使用边距助手类，你可以轻松地水平居中内容。

<masa-example file="Examples.styles_and_animations.spacing.Horizontal"></masa-example>

#### 负边距

同样也可以使用从 **1** 到 **16** 间隔的负边距.

<masa-example file="Examples.styles_and_animations.spacing.Margin"></masa-example>