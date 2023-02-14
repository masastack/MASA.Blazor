---
title: Switches
desc: The **MSwitch** component provides users the ability to choose between two distinct values. These are very similar to a toggle, or on/off switch, though aesthetically different than a checkbox.
related:
  - /blazor/components/checkboxes
  - /blazor/components/forms
  - /blazor/components/radio
---

## Usage

A **MSwitch** in its simplest form provides a toggle between 2 values.

<switches-usage></switches-usage>

## Examples

### Props

#### Colors

Switches can be colored by using any of the builtin colors and contextual names using the `Color` prop.

<masa-example file="Examples.components.switches.Color"></masa-example>

#### Custom truthy and falsy state

**MSwitch** have a typed value as its `Value`.

<masa-example file="Examples.components.switches.CustomState"></masa-example>

#### Flat

You can make switch render without elevation of thumb using `Flat` property.

<masa-example file="Examples.components.switches.Flat"></masa-example>

#### Inset

You can make switch render in `Inset` mode.

<masa-example file="Examples.components.switches.Inset"></masa-example>

#### State

**MSwitch** can have different states such as `default`, `Disabled` , and `Loading`.

<masa-example file="Examples.components.switches.State"></masa-example>

### Contents

#### LabelContent

Switch labels can be defined in **LabelContent**.

<masa-example file="Examples.components.switches.Label"></masa-example>

### Misc

#### CustomText

switch customize show text

<masa-example file="Examples.components.switches.CustomText"></masa-example>

