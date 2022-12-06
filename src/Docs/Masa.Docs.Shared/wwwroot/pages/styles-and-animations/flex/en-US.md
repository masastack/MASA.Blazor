# Flex

Control the layout of flex containers with alignment, justification and more with responsive flexbox utilities.

## Examples

### Misc

#### Enabling flexbox

Using the `display` tool class, you can convert any element into a flexbox container and convert the child elements of the element into a flex element. Using the additional flex attribute tool class, you can further customize their interaction.

You can also apply custom flex tool classes based on various breakpoints.

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

#### Flex direction

By default, `d-flex` is applied to `flex-direction: row` and can generally be omitted. However, in some cases, you may need to define it explicitly.

<masa-example file="Examples.styles_and_animations.flex.Direction"></masa-example>

#### Flex direction expand

`flex-column` and `flex-column-reverse` can be used to change the orientation of the flexbox container. Please note that IE11 and Safari may have column orientation issues.

There are also responsive changes in `flex-direction`.

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

#### Flex justify

The `justify-content` setting can be changed through the flex's justify class. By default, this will modify the flex item on the x-axis, but when using `flex-direction: column` it will be inverted to modify the **y-axis* *. Choose a value from `start` (browser default), `end`, `center`, `space-between`, or `space-acound`.

`justify-content` also has some elastic variables.

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

#### Flex align

The `align-items` setting can be changed through the align class of flex. By default, this will modify the flex items on the **y-axis**, but when using `flex-direction: column`, it will be reversed and modified* *x axis**. Choose a value from `start`, `end`, `center`, `baseline`, or `stretch` (browser default).

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
        When using flex align with IE11 you will need to set an explicit
        <code>height</code> as <code>min-height</code> will not suffice and
        cause undesired results.
      </p>
    </div>
    <div class="m-alert__border m-alert__border--left"></div>
  </div>
</div>

`align-items` also has some elastic variables.

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

#### Flex align self

The `align-self` setting can be changed through the align-self class of flex. By default, this will modify the flex item on the **x** axis, but it will be reversed when using `flex-direction: column` Modify **y** axis. Choose a value from `start`, `end`, `center`, `baseline`, `auto`, or `stretch` (browser default).

`align-self-items` also has some elastic variables.

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

#### Auto margins

Using margin helpers like `flex-row` or `flex-column` in a flexbox container, you can control the position of flex items on the **x axis** and **y axis** respectively.

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
        <strong>IE11</strong> does not properly support auto margins on flex
        items that have a parent with a non-default
        <code>justify-content</code> value.
        <a
          href="https://stackoverflow.com/a/37535548"
          target="_blank"
          rel="noopener"
          class="app-link text-decoration-none primary--text font-weight-medium d-inline-block"
          >See this StackOverflow answer<span
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
              ></path></svg></span
        ></a>
        for more details.
      </p>
    </div>
    <div class="m-alert__border m-alert__border--left"></div>
  </div>
</div>

<masa-example file="Examples.styles_and_animations.flex.Margins"></masa-example>

#### Auto margins expand

Mixing `flex-direction: column` and `align-items`, you can use `.mt-auto` and `.mb-auto` auxiliary classes to adjust the position of flex items.

<masa-example file="Examples.styles_and_animations.flex.MarginsEx"></masa-example>

#### Flex wrap

By default `.d-flex` does not provide any wrapping (its behavior is similar to `flex-wrap: nowrap`). This can be modified by using the flex-wrap auxiliary class in the format of `flex-(condition)`, the value of condition can be `nowrap`, `wrap` or `wrap-reverse`.

* **.flex-nowrap**
* **.flex-wrap**
* **.flex-wrap-reverse**

These auxiliary classes can also create more flexible variables in the format of `flex-{breakpoint}-{condition}` based on breakpoints. The following combinations are available:

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

#### Flex order

You can use the `order` tool class to change the visual order of flex items.

`order` also has some elastic variables.

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

#### Flex align content

The `align-content` setting can be changed through the align-content class of flex. By default, this will modify the flex item on the **x axis**, but it will be reversed when using `flex-direction: column` Modify **y-axis**. Choose a value from `start` (browser default), `end`, `center`, `between`, `around`, or `stretch`.

`align-content` also has some elastic variables.

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

#### Flex grow and shrink

MASA Blazor has helper classes for manually applying growth and shrinkage factors. It can be used by adding helper classes in the format of `flex-{condition}-{value}`. The condition can be either `grow` or `shrink`, value Can be either `0` or `1`. `grow` will allow the element to grow to fill the available space, while `shrink` will allow the item to shrink to the space required by its content. But only if the item must This happens only when it shrinks to fit its container, such as when the container is resized or affected by `flex-grow-1`. The value `0` will prevent this condition from happening, while `1` will allow it. The following classes are available:

* **flex-grow-0**
* **flex-grow-1**
* **flex-shrink-0**
* **flex-shrink-1**

These auxiliary classes can also create more flexible variables in the format of `flex-{breakpoint}-{condition}-{state}` based on breakpoints. The following combinations are available:

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