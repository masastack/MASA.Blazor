---
title: Progress linear
desc: "The component is used to convey data circularly to users. It also can be put into an indeterminate state to portray loading."
related:
  - /blazor/components/cards
  - /blazor/components/progress-circular
  - /blazor/components/lists
---

## Usage

In its simplest form, **MProgressLinear** displays a horizontal progress bar. Use the `value` prop to control the progress.

<progress-linear-usage></progress-linear-usage>

## Examples

### Props

#### Buffer value

A buffer state represents two values simultaneously. The primary value is controlled by `@bind-Value`, whereas the
buffer is controlled by the `BufferValue` prop.

<masa-example file="Examples.components.progress_linear.BufferValue"></masa-example>

#### Colors

You can also set the `Color` using the props color and `BackgroundColor`.

<masa-example file="Examples.components.progress_linear.Color"></masa-example>

#### Indeterminate

Using the `Indeterminate` prop, **MProgressLinear** continuously animates.

<masa-example file="Examples.components.progress_linear.Indeterminate"></masa-example>

#### Query

The `Query` props value is controlled by the truthiness of indeterminate, while the `Query` prop set to `true`.

<masa-example file="Examples.components.progress_linear.Query"></masa-example>

#### Reversed

Displays `Reversed` progress (right to left in LTR mode and left to right in RTL).

<masa-example file="Examples.components.progress_linear.Reversed"></masa-example>

#### Rounded

The `Rounded` prop is an alternative style that adds a border radius to the **MProgressLinear** component.

<masa-example file="Examples.components.progress_linear.Rounded"></masa-example>

#### Stream

The `Stream` property works with `BufferValue` to convey to the user that there is some action taking place. You can
use any combination of `BufferValue` and `Value` to achieve your design.

<masa-example file="Examples.components.progress_linear.Stream"></masa-example>

#### Striped

This applies a `Striped` background over the value portion of the **MProgressLinear**.

<masa-example file="Examples.components.progress_linear.Striped"></masa-example>

### Contents

#### Default

The **MProgressLinear** component will be responsive to user input when using `@bind-Value`. You can use the default
slot or bind a local model to display inside of the progress. If you are looking for advanced features on a linear type component, check out [MSlider](/blazor/components/sliders).

<masa-example file="Examples.components.progress_linear.Default"></masa-example>

### Misc

#### Determinate

The progress linear component can have a determinate state modified by `@bind-Value`.

<masa-example file="Examples.components.progress_linear.Determinate"></masa-example>

#### File loader

The **MProgressLinear** component is good for translating to the user that they are waiting for a response.

<masa-example file="Examples.components.progress_linear.FileLoader"></masa-example>

#### Toolbar loader

Using the `Absolute` prop we are able to position the **MProgressLinear** component at the bottom of the **MToolbar**.
We also use the `Active` prop which allows us to control the visibility of the progress.

<masa-example file="Examples.components.progress_linear.ToolbarLoader"></masa-example>