---
title: Sliders
desc: "The **MSlider** component is a better visualization of the number input. It is used for gathering numerical user data."
related:
  - /blazor/components/forms
  - /blazor/components/selects
  - /blazor/components/range-sliders
---

## Usage

Sliders reflect a range of values along a bar, from which users may select a single value. They are ideal for adjusting settings such as volume, brightness, or applying image filters.

<sliders-usage></sliders-usage>

## Examples

### Props

#### Colors

You can set the colors of the slider using the props `Color`, `TrackColor` and `ThumbColor`.

<masa-example file="Examples.components.sliders.Colors"></masa-example>

#### Disabled

You cannot interact with `Disabled` sliders.

<masa-example file="Examples.components.sliders.Disabled"></masa-example>

#### Discrete

Discrete sliders offer a thumb label that displays the exact current amount. Using the `Step` prop you can disallow selecting values outside of steps.

<masa-example file="Examples.components.sliders.Discrete"></masa-example>

#### Icons

You can add icons to the slider with the `AppendIcon` and `PrependIcon` props. With `OnAppendClick` and `OnPrependClick` you can trigger a callback function when click the icon.

<masa-example file="Examples.components.sliders.Icons"></masa-example>

#### InverseLabel

**MSlider** with `InverseLabel` property displays label at the end of it.

<masa-example file="Examples.components.sliders.InverseLabel"></masa-example>

#### MinAndMax

You can set `Min` and `Max` values of sliders.

<masa-example file="Examples.components.sliders.MinAndMax"></masa-example>

#### Readonly

You cannot interact with `Readonly` sliders, but they look as ordinary ones.

<masa-example file="Examples.components.sliders.Readonly"></masa-example>

#### Step

**MSlider** can have steps other than 1. This can be helpful for some applications where you need to adjust values with more or less accuracy.

<masa-example file="Examples.components.sliders.Step"></masa-example>

#### Thumb

You can display a thumb label while sliding or always with the `ThumbLabel` prop . It can have a custom color by setting `ThumbColor` prop and a custom size with the `ThumbSize` prop. With the **AlwaysDirty** prop its color will never change, even when on the **Min** value.

<masa-example file="Examples.components.sliders.Thumb"></masa-example>

#### Ticks

Tick marks represent predetermined values to which the user can move the slider.

<masa-example file="Examples.components.sliders.Ticks"></masa-example>

#### Validation

<masa-example file="Examples.components.sliders.Validation"></masa-example>

<example file="" />

#### VerticalSliders

You can use the `Vertical` prop to switch sliders to a vertical orientation. If you need to change the height of the slider, use css.

<masa-example file="Examples.components.sliders.VerticalSliders"></masa-example>

#### Contents

### AppendAndPrepend

Use slots such as `PrependContent` and `AppendContent` to easily customize the **MSlider** to fit any situation.

<masa-example file="Examples.components.sliders.AppendAndPrepend"></masa-example>

### AppendTextField

Sliders can be combined with other components in its **AppendContent**, such as **MTextField** , to add additional functionality to the component.

<masa-example file="Examples.components.sliders.AppendTextField"></masa-example>