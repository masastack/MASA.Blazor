---
title: Range sliders
desc: "The **MRangeSlider**  component is a better visualization of the number input. It is used for gathering numerical user data."
related:
  - /components/forms
  - /components/selects
  - /components/sliders
---

## Usage

Sliders reflect a range of values along a bar, from which users may select a single value. They are ideal for adjusting settings such as volume, brightness, or applying image filters.

<range-sliders-usage></range-sliders-usage>

## Examples

### Props

#### Disabled

You cannot interact with `Disabled` sliders.

<example file="" />

#### MinAndMax

You can set `Min` and `Max` values of sliders.

<example file="" />

#### Step

**MRangeSlider** can have steps other than 1. This can be helpful for some applications where you need to adjust values with more or less accuracy.

<example file="" />

#### VerticalSliders

You can use the `Vertical` prop to switch sliders to a vertical orientation. If you need to change the height of the slider, use css.

<example file="" />

#### Contents

### ThumbLabel

Using the `TickLabels` prop along with the **ThumbLabelContent**, you can create a very customized solution.

<example file="" />