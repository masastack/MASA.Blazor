---
order: 3
title:
  zh-CN: Flex 纵轴对齐
  en-US: Align
---

## zh-CN

可以通过 flex 的 align 类改变 `align-items` 设置. 默认情况下, 这将修改 **y轴** 上的 flex 项目, 但是当使用 `flex-direction: column` 时将被反转从而修改 **x轴**. 从 `start` , `end`, `center`, `baseline`, 或 `stretch`(浏览器默认) 选择一个值.

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

## en-US

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