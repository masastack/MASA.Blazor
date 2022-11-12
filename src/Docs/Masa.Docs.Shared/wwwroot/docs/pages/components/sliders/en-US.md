---
title: Sliders
desc: "The **MSlider** component is a better visualization of the number input. It is used for gathering numerical user data."
related:
  - /components/forms
  - /components/selects
  - /components/range-sliders
---

## Usage

Sliders reflect a range of values along a bar, from which users may select a single value. They are ideal for adjusting settings such as volume, brightness, or applying image filters.

<sliders-usage></sliders-usage>

## Examples

### Props

#### Colors

You can set the colors of the slider using the props `Color`, `TrackColor` and `ThumbColor`.

<example file="" />

#### Disabled

You cannot interact with `Disabled` sliders.

<example file="" />

#### Discrete

Discrete sliders offer a thumb label that displays the exact current amount. Using the `Step` prop you can disallow selecting values outside of steps.

<example file="" />

#### Icons

You can add icons to the slider with the `AppendIcon` and `PrependIcon` props. With `OnAppendClick` and `OnPrependClick` you can trigger a callback function when click the icon.

<example file="" />

#### InverseLabel

**MSlider** with `InverseLabel` property displays label at the end of it.

<example file="" />

#### MinAndMax

You can set `Min` and `Max` values of sliders.

<example file="" />

#### Readonly

You cannot interact with `Readonly` sliders, but they look as ordinary ones.

<example file="" />

#### Step

`MSlider` can have steps other than 1. This can be helpful for some applications where you need to adjust values with more or less accuracy.

<example file="" />

#### Thumb

You can display a thumb label while sliding or always with the `ThumbLabel` prop . It can have a custom color by setting `ThumbColor` prop and a custom size with the `ThumbSize` prop. With the **AlwaysDirty** prop its color will never change, even when on the **Min** value.

<example file="" />

#### Ticks

Tick marks represent predetermined values to which the user can move the slider.

<example file="" />

#### Validation

Support validation.

<example file="" />

#### VerticalSliders

You can use the `Vertical` prop to switch sliders to a vertical orientation. If you need to change the height of the slider, use css.

<example file="" />

#### Contents

### AppendAndPrepend

Use slots such as `PrependContent` and `AppendContent` to easily customize the **MSlider** to fit any situation.

<example file="" />

### AppendTextField

Sliders can be combined with other components in its **AppendContent**, such as **MTextField** , to add additional functionality to the component.

<example file="" />