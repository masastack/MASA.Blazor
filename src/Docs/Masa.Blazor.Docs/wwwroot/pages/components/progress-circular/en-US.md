---
title: Progress circular
desc: "The component is used to display circular data to users. It also can be put into an indeterminate state to portray loading."
related:
  - /blazor/components/cards
  - /blazor/components/progress-linear
  - /blazor/components/lists
---

## Usage

In its simplest form, **MProgressCircular** displays a circular progress bar. Use the `value` prop to control the progress.

<progress-circular-usage></progress-circular-usage>

## Examples

### Props

#### Color

You can use the `Color` attribute to set other colors for **MProgressCircular**.

<masa-example file="Examples.components.progress_circular.Color"></masa-example>

#### Indeterminate

Using the `Indeterminate` prop, a **MProgressCircular** continues to animate indefinitely.

<masa-example file="Examples.components.progress_circular.Indeterminate"></masa-example>

#### Rotate

The `Rotate` prop gives you the ability to customize the **MProgressCircular**'s origin.

<masa-example file="Examples.components.progress_circular.Rotate"></masa-example>

#### SizeOrWidth

The `Size` and `Width` properties allow you to easily modify the size and width of the **MProgressCircular**
component.

<masa-example file="Examples.components.progress_circular.SizeOrWidth"></masa-example>
