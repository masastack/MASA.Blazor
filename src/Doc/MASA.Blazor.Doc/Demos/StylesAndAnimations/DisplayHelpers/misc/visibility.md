---
order: 0
title:
  zh-CN: 可见性
  en-US: Visibility
---

## zh-CN

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

## en-US

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
