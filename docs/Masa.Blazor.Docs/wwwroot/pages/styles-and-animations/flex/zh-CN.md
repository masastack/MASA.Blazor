# Flex（弹性布局）

使用响应的 flexbox 实用程序通过对齐、排列等方式控制 flex 容器的布局, 采用 Flex 布局的元素，称为 Flex 容器（flex container），简称”容器”。它的所有子元素自动成为容器成员，称为 Flex 项目（flex item），简称”项目”。

## 示例

### 其他

#### 启用 flexbox

使用 **display** 工具类, 你可以将任何元素转换为 flexbox 容器并将该元素的 **子元素** 转成 **flex** 元素. 使用附加的 **flex** 属性工具类，您可以进一步定制它们的交互。

您还可以基于各种断点应用自定义的 **flex** 工具类。

* **.d-flex**
* **.d-inline-flex**
* **.d-sm-flex**
* **.d-sm-inline-flex**
* **.d-md-flex**
* **.d-md-inline-flex**
* **.d-lg-flex**
* **.d-lg-inline-flex**
* **.d-xl-flex**
* **.d-xl-inline-flex**

<masa-example file="Examples.styles_and_animations.flex.Enabling"></masa-example>

#### Flex (主轴) 方向

默认情况下, `d-flex` 应用于 `flex-direction: row` 并且一般可以省略。 但是，在某些情况下，您可能需要显式地定义它。

<masa-example file="Examples.styles_and_animations.flex.Direction"></masa-example>

#### Flex (主轴) 方向扩展

`flex-column` 和 `flex-column-reverse` 可以用于改变 **flexbox** 容器的方向. 请注意, **IE11** 和 **Safari** 可能存在列方向的问题.

`flex-direction` 也有响应式变化。

* **.flex-row**
* **.flex-row-reverse**
* **.flex-column**
* **.flex-column-reverse**
* **.flex-sm-row**
* **.flex-sm-row-reverse**
* **.flex-sm-column**
* **.flex-sm-column-reverse**
* **.flex-md-row**
* **.flex-md-row-reverse**
* **.flex-md-column**
* **.flex-md-column-reverse**
* **.flex-lg-row**
* **.flex-lg-row-reverse**
* **.flex-lg-column**
* **.flex-lg-column-reverse**
* **.flex-xl-row**
* **.flex-xl-row-reverse**
* **.flex-xl-column**
* **.flex-xl-column-reverse**

<masa-example file="Examples.styles_and_animations.flex.DirectionEx"></masa-example>

#### Flex 横轴对齐

可以通过 **flex** 的 **justify** 类改变 `justify-content` 设置. 默认情况下, 这将修改 x轴 上的 **flex** 项目, 但是当使用 `flex-direction: column` 时将被反转从而修改 **y轴**. 从 `start` (浏览器默认), `end`, `center`, `space-between`, 或 `space-acound` 选择一个值.

`justify-content` 同样也有一些弹性变量.

* **.justify-start**
* **.justify-end**
* **.justify-center**
* **.justify-space-between**
* **.justify-space-around**
* **.justify-sm-start**
* **.justify-sm-end**
* **.justify-sm-center**
* **.justify-sm-space-between**
* **.justify-sm-space-around**
* **.justify-md-start**
* **.justify-md-end**
* **.justify-md-center**
* **.justify-md-space-between**
* **.justify-md-space-around**
* **.justify-lg-start**
* **.justify-lg-end**
* **.justify-lg-center**
* **.justify-lg-space-between**
* **.justify-lg-space-around**
* **.justify-xl-start**
* **.justify-xl-end**
* **.justify-xl-center**
* **.justify-xl-space-between**
* **.justify-xl-space-around**

<masa-example file="Examples.styles_and_animations.flex.Justify"></masa-example>

#### Flex 纵轴对齐

可以通过 **flex** 的 **align** 类改变 `align-items` 设置. 默认情况下, 这将修改 **y轴** 上的 flex 项目, 但是当使用 `flex-direction: column` 时将被反转从而修改 **x轴**. 从 `start` , `end`, `center`, `baseline`, 或 `stretch`(浏览器默认) 选择一个值.

<div
  role="alert"
  type="info"
  class="m-alert m-alert--doc m-sheet theme--dark m-alert--border m-alert--text m-alert--border-left info--text"
>
  <div class="m-alert__wrapper">
    <span
      aria-hidden="true"
      class="m-icon notranslate m-alert__icon theme--dark info--text"
      ><svg
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 24 24"
        role="img"
        aria-hidden="true"
        class="m-icon__svg"
      >
        <path
          d="M11,9H13V7H11M12,20C7.59,20 4,16.41 4,12C4,7.59 7.59,4 12,4C16.41,4 20,7.59 20,12C20,16.41 16.41,20 12,20M12,2A10,10 0 0,0 2,12A10,10 0 0,0 12,22A10,10 0 0,0 22,12A10,10 0 0,0 12,2M11,17H13V11H11V17Z"
        ></path></svg
    ></span>
    <div class="m-alert__content">
      <p>
        当在 IE11 使用 flex 纵轴对齐时，你需要显示设置一个
        <code>height</code>，因为
        <code>min-height</code> 的不足会导致不符合预期的结果。
      </p>
    </div>
    <div class="m-alert__border m-alert__border--left"></div>
  </div>
</div>

`align-items` 同样也有一些弹性变量.

* **.align-start**
* **.align-end**
* **.align-center**
* **.align-baseline**
* **.align-stretch**
* **.align-sm-start**
* **.align-sm-end**
* **.align-sm-center**
* **.align-sm-baseline**
* **.align-sm-stretch**
* **.align-md-start**
* **.align-md-end**
* **.align-md-center**
* **.align-md-baseline**
* **.align-md-stretch**
* **.align-lg-start**
* **.align-lg-end**
* **.align-lg-center**
* **.align-lg-baseline**
* **.align-lg-stretch**
* **.align-xl-start**
* **.align-xl-end**
* **.align-xl-center**
* **.align-xl-baseline**
* **.align-xl-stretch**

<masa-example file="Examples.styles_and_animations.flex.Align"></masa-example>

#### Flex 自身对齐

可以通过 **flex** 的 **align-self** 类改变 `align-self` 设置. 默认情况下, 这将修改 **x**轴 上的 flex 项目, 但是当使用 `flex-direction: column` 时将被反转从而修改 **y**轴. 从 `start` , `end`, `center`, `baseline`, `auto`, 或 `stretch`(浏览器默认) 选择一个值.

`align-self-items` 同样也有一些弹性变量.

* **.align-self-start**
* **.align-self-end**
* **.align-self-center**
* **.align-self-baseline**
* **.align-self-auto**
* **.align-self-stretch**
* **.align-self-sm-start**
* **.align-self-sm-end**
* **.align-self-sm-center**
* **.align-self-sm-baseline**
* **.align-self-sm-auto**
* **.align-self-sm-stretch**
* **.align-self-md-start**
* **.align-self-md-end**
* **.align-self-md-center**
* **.align-self-md-baseline**
* **.align-self-md-auto**
* **.align-self-md-stretch**
* **.align-self-lg-start**
* **.align-self-lg-end**
* **.align-self-lg-center**
* **.align-self-lg-baseline**
* **.align-self-lg-auto**
* **.align-self-lg-stretch**
* **.align-self-xl-start**
* **.align-self-xl-end**
* **.align-self-xl-center**
* **.align-self-xl-baseline**
* **.align-self-xl-auto**
* **.align-self-xl-stretch**

<masa-example file="Examples.styles_and_animations.flex.AlignSelf"></masa-example>

#### 自动边距

在 **flexbox** 容器中使用 `flex-row` 或 `flex-column` 这样的边距辅助类, 你可以分别控制 **flex** 项目在 **x轴** 和 **y轴** 上的位置.

<div
  role="alert"
  type="error"
  class="m-alert m-alert--doc m-sheet theme--dark m-alert--border m-alert--text m-alert--border-left error--text"
>
  <div class="m-alert__wrapper">
    <span
      aria-hidden="true"
      class="m-icon notranslate m-alert__icon theme--dark error--text"
      ><svg
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 24 24"
        role="img"
        aria-hidden="true"
        class="m-icon__svg"
      >
        <path
          d="M8.27,3L3,8.27V15.73L8.27,21H15.73C17.5,19.24 21,15.73 21,15.73V8.27L15.73,3M9.1,5H14.9L19,9.1V14.9L14.9,19H9.1L5,14.9V9.1M11,15H13V17H11V15M11,7H13V13H11V7"
        ></path></svg
    ></span>
    <div class="m-alert__content">
      <p>
        <strong>IE11</strong>不能正确支持具有非默认“justify content”值的 flexbox
        容器的 flex 元素的自动边距。 更多详细信息请
        <a
          href="https://stackoverflow.com/a/375355548"
          target="_blank"
          rel="noopener"
          class="app-link text-decoration-none primary--text font-weight-medium d-inline-block"
          >参阅此 StackOverflow 答案<span
            aria-hidden="true"
            class="m-icon notranslate theme--dark primary--text ml-1"
            style="font-size: 0.875rem; height: 0.875rem; width: 0.875rem"
            ><svg
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 24 24"
              role="img"
              aria-hidden="true"
              class="m-icon__svg"
              style="font-size: 0.875rem; height: 0.875rem; width: 0.875rem"
            >
              <path
                d="M14,3V5H17.59L7.76,14.83L9.17,16.24L19,6.41V10H21V3M19,19H5V5H12V3H5C3.89,3 3,3.9 3,5V19A2,2 0 0,0 5,21H19A2,2 0 0,0 21,19V12H19V19Z"
              ></path></svg></span></a
        >。
      </p>
    </div>
    <div class="m-alert__border m-alert__border--left"></div>
  </div>
</div>

<masa-example file="Examples.styles_and_animations.flex.Margins"></masa-example>

#### 自动边距 使用 align-items

合 `flex-direction: column` 和 `align-items`, 你可以利用 `.mt-auto` 和 `.mb-auto` 辅助类来调整 flex 项目的位置.

<masa-example file="Examples.styles_and_animations.flex.MarginsEx"></masa-example>

#### Flex 堆叠

默认情况下 `.d-flex` 不提供任何包装(其行为类似于 `flex-wrap：nowrap`)。 这可以通过使用 `flex-{condition}` 格式的 flex-wrap 辅助类进行修改, condition 的值可以为 `nowrap`, `wrap` 或 `wrap-reverse`.

* **.flex-nowrap**
* **.flex-wrap**
* **.flex-wrap-reverse**

这些辅助类同样可以基于断点以 `flex-{breakpoint}-{condition}` 的格式创建更多的弹性变量. 以下组合可用：

* **.flex-sm-nowrap**
* **.flex-sm-wrap**
* **.flex-sm-wrap-reverse**
* **.flex-md-nowrap**
* **.flex-md-wrap**
* **.flex-md-wrap-reverse**
* **.flex-lg-nowrap**
* **.flex-lg-wrap**
* **.flex-lg-wrap-reverse**
* **.flex-xl-nowrap**
* **.flex-xl-wrap**
* **.flex-xl-wrap-reverse**

<masa-example file="Examples.styles_and_animations.flex.Wrap"></masa-example>

#### Flex 排序

您可以使用 `order` 工具类改变 **flex** 项目的视觉排序.

`order` 同样也有一些弹性变量.

* **.order-first**
* **.order-0**
* **.order-1**
* **.order-2**
* **.order-3**
* **.order-4**
* **.order-5**
* **.order-6**
* **.order-7**
* **.order-8**
* **.order-9**
* **.order-10**
* **.order-11**
* **.order-12**
* **.order-last**
* **.order-sm-first**
* **.order-sm-0**
* **.order-sm-1**
* **.order-sm-2**
* **.order-sm-3**
* **.order-sm-4**
* **.order-sm-5**
* **.order-sm-6**
* **.order-sm-7**
* **.order-sm-8**
* **.order-sm-9**
* **.order-sm-10**
* **.order-sm-11**
* **.order-sm-12**
* **.order-sm-last**
* **.order-md-first**
* **.order-md-0**
* **.order-md-1**
* **.order-md-2**
* **.order-md-3**
* **.order-md-4**
* **.order-md-5**
* **.order-md-6**
* **.order-md-7**
* **.order-md-8**
* **.order-md-9**
* **.order-md-10**
* **.order-md-11**
* **.order-md-12**
* **.order-md-last**
* **.order-lg-first**
* **.order-lg-0**
* **.order-lg-1**
* **.order-lg-2**
* **.order-lg-3**
* **.order-lg-4**
* **.order-lg-5**
* **.order-lg-6**
* **.order-lg-7**
* **.order-lg-8**
* **.order-lg-9**
* **.order-lg-10**
* **.order-lg-11**
* **.order-lg-12**
* **.order-lg-last**
* **.order-lg-first**
* **.order-xl-0**
* **.order-xl-1**
* **.order-xl-2**
* **.order-xl-3**
* **.order-xl-4**
* **.order-xl-5**
* **.order-xl-6**
* **.order-xl-7**
* **.order-xl-8**
* **.order-xl-9**
* **.order-xl-10**
* **.order-xl-11**
* **.order-xl-12**
* **.order-xl-last**

<masa-example file="Examples.styles_and_animations.flex.Order"></masa-example>

#### Flex 内容对齐

可以通过 **flex** 的 **align-content** 类改变 `align-content` 设置. 默认情况下, 这将修改 **x轴** 上的 flex 项目, 但是当使用 `flex-direction: column` 时将被反转从而修改 **y轴**. 从 `start` (浏览器默认) , `end`, `center`, `between`, `around`, 或 `stretch` 选择一个值.

`align-content` 同样也有一些弹性变量.

* **align-content-start**
* **align-content-end**
* **align-content-center**
* **align-content-space-between**
* **align-content-space-around**
* **align-content-stretch**
* **align-sm-content-start**
* **align-sm-content-end**
* **align-sm-content-center**
* **align-sm-content-space-between**
* **align-sm-content-space-around**
* **align-sm-content-stretch**
* **align-md-content-start**
* **align-md-content-end**
* **align-md-content-center**
* **align-md-content-space-between**
* **align-md-content-space-around**
* **align-md-content-stretch**
* **align-lg-content-start**
* **align-lg-content-end**
* **align-lg-content-center**
* **align-lg-content-space-between**
* **align-lg-content-space-around**
* **align-lg-content-stretch**
* **align-xl-content-start**
* **align-xl-content-end**
* **align-xl-content-center**
* **align-xl-content-space-between**
* **align-xl-content-spacearound**
* **align-xl-content-stretch**

<masa-example file="Examples.styles_and_animations.flex.AlignContent"></masa-example>

#### Flex 增长系数和收缩系数

MASA Blazor 有用于手动应用增长和收缩系数的辅助类. 通过添加 `flex-{condition}-{value}` 格式的辅助类来使用。 condition 可以是 `grow` 或 `shrink` 两者之一, value可以是 `0` 或 `1` 两者之一。 `grow` 将允许元素增长以填充可用的空间, 然而 `shrink` 将允许项目收缩到它的内容所需要的空间. 但是，只有当项目必须收缩以适合其容器时才会发生这种情况，例如容器大小调整或受到 `flex-grow-1` 的影响。 值`0`将阻止该条件的发生，而`1`则允许出现这种情况。 以下类可用：

* **flex-grow-0**
* **flex-grow-1**
* **flex-shrink-0**
* **flex-shrink-1**

这些辅助类同样可以基于断点以 `flex-{breakpoint}-{condition}-{state}` 的格式创建更多的弹性变量. 以下组合可用：

* **flex-sm-grow-0**
* **flex-md-grow-0**
* **flex-lg-grow-0**
* **flex-xl-grow-0**
* **flex-sm-grow-1**
* **flex-md-grow-1**
* **flex-lg-grow-1**
* **flex-xl-grow-1**
* **flex-sm-shrink-0**
* **flex-md-shrink-0**
* **flex-lg-shrink-0**
* **flex-xl-shrink-0**
* **flex-sm-shrink-1**
* **flex-md-shrink-1**
* **flex-lg-shrink-1**
* **flex-xl-shrink-1**

<masa-example file="Examples.styles_and_animations.flex.GrowShrink"></masa-example>



