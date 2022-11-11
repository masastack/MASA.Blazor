---
title: Radio
desc: "The **MRadio** component is a simple radio button. When combined with the **MRadioGroup** component you can provide groupable functionality to allow users to select from a predefined set of options."
related:
  - /components/button-groups
  - /components/forms
  - /components/checkboxes
---

## Usage

Although **MRadio** can be used on its own, it is best used in conjunction with **MRadioGroup** . Using the **@bind-Value** on the **MRadioGroup** you can access the value of the selected radio button inside the group.

<radio-usage></radio-usage>

## Examples

### Props

#### Color

Radios can be colored by using any of the builtin colors and contextual names using the `Color` prop.

<example file="" />

#### Direction

Radio-groups can be presented either as a row or a column, using their respective props. The default is as a column.

<example file="" />

#### Mandatory

Radio-groups are by default not mandatory. This can be changed with the `Mandatory` prop.

<example file="" />

### Contents

#### LabelContent

Radio Group labels can be defined in **LabelContent** - that will allow to use HTML content.

<example file="" />