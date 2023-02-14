---
title: Checkboxes
desc: "The **MCheckbox** component provides users the ability to choose between two distinct values. These are very similar to a switch and can be used in complex forms and checklists."
related:
  - /blazor/components/switches
  - /blazor/components/forms
  - /blazor/components/text-fields
---

## Usage

A **Checkbox** in its simplest form provides a toggle between 2 values.

<checkboxes-usage></checkboxes-usage>

## Examples

### Props

#### Color

**MCheckbox** can be colored by using any of the builtin colors and contextual names using the color prop.

<masa-example file="Examples.components.checkboxes.Color"></masa-example>

#### Custom truthy and falsy state

**MCheckbox** will have a typed value as its `Value`.

<masa-example file="Examples.components.checkboxes.CustomState"></masa-example>

#### States

**MCheckbox** can have different states such as  `default`, `disabled`, and `indeterminate`.

<masa-example file="Examples.components.checkboxes.States"></masa-example>

### Contents

#### Label

**MCheckbox** labels can be defined in **LabelContent** - that will allow to use HTML content.

<masa-example file="Examples.components.checkboxes.LabelContent"></masa-example>

### Misc

#### Inline TextField

You can place **MCheckbox** in line with other components such as **MTextField** .

<masa-example file="Examples.components.checkboxes.InlineTextField"></masa-example>





