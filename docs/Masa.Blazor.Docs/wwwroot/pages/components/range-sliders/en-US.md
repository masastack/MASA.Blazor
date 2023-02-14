---
title: Range sliders
desc: "The **MRangeSlider**  component is a better visualization of the number input. It is used for gathering numerical user data."
related:
  - /blazor/components/forms
  - /blazor/components/selects
  - /blazor/components/sliders
---

## Usage

Sliders reflect a range of values along a bar, from which users may select a single value. They are ideal for adjusting settings such as volume, brightness, or applying image filters.

<range-sliders-usage></range-sliders-usage>

## Examples

### Props

#### Disabled

You cannot interact with `Disabled` sliders.

<masa-example file="Examples.components.range_sliders.Disabled"></masa-example>

#### MinAndMax

You can set `Min` and `Max` values of sliders.

<masa-example file="Examples.components.range_sliders.MinAndMax"></masa-example>

#### Step

**MRangeSlider** can have steps other than 1. This can be helpful for some applications where you need to adjust values with more or less accuracy.

<masa-example file="Examples.components.range_sliders.Step"></masa-example>

#### VerticalSliders

You can use the `Vertical` prop to switch sliders to a vertical orientation. If you need to change the height of the slider, use css.

<masa-example file="Examples.components.range_sliders.VerticalSliders"></masa-example>

#### Contents

### ThumbLabel

Using the `TickLabels` prop along with the **ThumbLabelContent**, you can create a very customized solution.

<masa-example file="Examples.components.range_sliders.ThumbLabel"></masa-example>