---
title: Grid System（网格系统）
desc: "Masa.Blazor 配备了一个使用 flexbox 构建的 12 格网格系统。 网格用于在应用的内容中创建特定的布局。 它包含 5 种类型的媒体断点，用于针对特定的屏幕尺寸或方向，xs、sm、md、lg 和 xl。 这些分辨率在视口断点表中定义如下，可以通过自定义断点进行修改。"
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

## 使用

Masa.Blazor 网格深受 [Bootstrap 网格](https://getbootstrap.com/docs/4.0/layout/grid/) 的启发。 它使用一系列的容器、行、列来整合内容的布局和排列。 如果你不熟悉
flexbox，[阅读 CSS Tricks flexbox 指南](https://css-tricks.com/snippets/css/a-guide-to-flexbox/#flexbox-background)，了解背景、术语、指南和代码片段。

<example file="" />

## 子组件

- **MContainer**：**MContainer** 提供了将你的网站内容居中和水平填充的功能。 你还可以使用 `Fluid` 属性将容器在所有视口和设备尺寸上完全扩展。
- **MCol**：**MCol** 包裹内容，它必须是 **MRow** 的直接子集。
- **MRow**：**MRow** 是 **MCol** 的容器组件。 它使用 **Flex** 属性来控制其内栏的布局和流。 它使用的是 `24px` 的标准间隔。 这可以用 `Dense` 属性来减小，或者用 `NoGutters`
  来完全去除。
- **MSpacer**：**MSpacer** 是一个基本而又通用的间隔组件，用于分配父子组件之间的剩余宽度。 当一个 **MSpacer** 放置在子组件之前或之后时，组件将推到其容器的左右两侧。 当多个组件之间使用多个 **MSpacer** 时，剩余的宽度将均匀地分布在每个 spacer 之间。

## 辅助类

**FillHeight**将`height:100%`应用于元素。当应用于**MContainer**时，它还包括`align items:center`。

## 注意

<!--alert:info--> 
1.x 网格系统已被废弃，请改用 2.x 网格系统。 1.x 网格的文档可以在 v1.5 文档 中找到。
<!--alert:info--> 

<!--alert:info--> 
网格组件上基于断点的属性以 `andUp` 的方式工作。 考虑 `xs` 断点已经被删除的情况， 这将会影响到 `offset`、`justify`、`align` 和 **MCol** 上的断点属性。

- 像 `justify-sm` 和 `justify-md` 这样的属性仍然存在，但 `justify-xs` 会变成 `justify`。
- **MCol** 上不存在 `xs` 属性。 与此对应的是 `cols` 属性。
<!--alert:info--> 

<!--alert:info--> 
当在 IE11 使用网格系统时，你需要设置一个显式的 `height`，因为 `min-height` 不足进而导致非预期结果。
<!--alert:info--> 

## 示例

### 属性

#### 垂直对齐

使用 `Align` 和 `AlignSelf` 属性来改变 flex 项目及其父项的垂直对齐方式。

<example file="" />

#### 断点尺寸

列将自动占用其父容器内相等的空间。 这可以使用 `Cols` 属性来修改。 你还可以使用 `Sm`、`Md`、`Lg` 和 `Xl` 属性来进一步定义不同视口尺寸下的列占用空间。

<example file="" />

#### 水平对齐

使用 `Justify` 属性改变 flex 项目的水平对齐方式。

<example file="" />

#### 无间隔

你可以使用 `NoGutters` 属性从 **MRow** 中移除负值外边距，从其直接子 **MCol** 中移除内边距。

<example file="" />

#### 偏移

偏移对于控制元素不可见或控制内容位置很有用。 就像断点一样，你可以为任何可用的尺寸设置一个偏移。 这使你可以根据自己的需求精确地调整应用布局。

<example file="" />

#### 偏移断点

偏移也可以在每个断点的基础上设置。

<example file="" />

#### 排序

你可以控制网格项目的排序。 与偏移一样，你可以为不同的尺寸设置不同的顺序。 设计专门的屏幕布局，以适应任何应用。

<example file="" />

#### 先后排序

你也可以明确指定 `First` 或 `Last`，这将分别为 order CSS 属性分配 `-1` 或 `13` 值。

<example file="" />

### 其他

#### 换行列

当在给定的行中放置了超过 12 个列时（没有使用 `.flex-nowrap`），每一组额外的列都将被包入新的行。

<example file="" />

#### 等宽列

你可以把等宽列分成多行。
虽然旧版本的浏览器有解决办法，但仍有一个 [Safari flexbox 问题](https://github.com/philipwalton/flexbugs#11-min-and-max-size-declarations-are-ignored-when-wrapping-flex-items)。
如果你是最新的 Safari，无需担心这个问题。

<example file="" />

#### 增长与收缩

默认情况下，flex 组件将自动填充行或列中的可用空间。 没有指定具体尺寸时，它们也会相对于 flex 容器中的其他 flex 项目收缩。 你可以使用 cols 属性定义 `MCol` 的列宽，并提供 **1 到 12 的值**。

<example file="" />

#### 外边距辅助

使用[外边距工具类](/stylesandanimations/flex)可以强行把同级列分开。

<example file="" />

#### 嵌套网格

与其他框架类似，网格可以被嵌套，以实现非常自定义的布局。

<example file="" />

#### 一列宽度

使用自动布局时，你可以只定义一列的宽度，并且仍然可以让它的同级元素围绕它自动调整大小。

<example file="" />

#### 行和列断点

根据分辨率动态地改变你的布局。 **（调整你的屏幕大小，并观看顶部 `row` 布局在 sm、md 和 lg 断点上的变化）**。

<example file="" />

#### 空白

**MSpacer** 组件在你想要填充可用空间或在两个组件之间留出空间时非常有用。

<example file="" />

#### 独特的布局

Masa.Blazor 网格系统的强大和灵活性使你能够创建出色的用户界面。

<example file="" />

#### 可变内容宽度

为列分配断点宽度可以根据其内容的性质宽度来配置调整大小。

<example file="" />