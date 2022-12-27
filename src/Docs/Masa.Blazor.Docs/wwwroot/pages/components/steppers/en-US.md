---
title: Steppers
desc: "The **MStepper** component displays progress through numbered steps."
related:
  - /blazor/components/tabs
  - /blazor/components/buttons
  - /blazor/components/windows
---

## Usage

A stepper can be used for a multitude of scenarios, including shopping carts, record creation and more.

<masa-example file="Examples.components.steppers.Usage"></masa-example>

## Examples

### Props

#### Alternate label

Steppers also have an alternative label style which places the title under the step itself.

<masa-example file="Examples.components.steppers.AlternateLabel"></masa-example>

#### Non linear

`NonLinear` steppers allow the user to move through your process in whatever way they choose.

<masa-example file="Examples.components.steppers.NonLinear"></masa-example>

#### Vertical

Vertical steppers move users along the y-axis and otherwise work exactly the same as their horizontal counterpart.

<masa-example file="Examples.components.steppers.Vertical"></masa-example>

### Misc

#### Alternative label with errors

The error state can also be applied to the alternative label style.

<masa-example file="Examples.components.steppers.AlternativeLabelWithErrors"></masa-example>

#### Dynamic steps

Steppers can have their steps dynamically added or removed. If a currently active step is removed, be sure to account
for this by changing the applied model.

<masa-example file="Examples.components.steppers.DynamicSteps"></masa-example>

#### Editable steps

An editable step can be selected by a user at any point and will navigate them to that step.

<masa-example file="Examples.components.steppers.EditableSteps"></masa-example>

#### Errors

An error state can be displayed to notify the user of some action that must be taken.

<masa-example file="Examples.components.steppers.Errors"></masa-example>

#### Horizontal steps

Horizontal steppers move users along the x-axis through the defined steps.

<masa-example file="Examples.components.steppers.HorizontalSteps"></masa-example>

#### Linear steppers

Linear steppers will always move a user through your defined path.

<masa-example file="Examples.components.steppers.LinearSteppers"></masa-example>

#### Non editable steps

Non-editable steps force a user to process linearly through your process.

<masa-example file="Examples.components.steppers.NonEditableSteps"></masa-example>

#### Optional steps

An optional step can be called out with sub-text.

<masa-example file="Examples.components.steppers.OptionalSteps"></masa-example>

#### Vertical errors

The same state also applies to Vertical steppers.

<masa-example file="Examples.components.steppers.VerticalErrors"></masa-example>