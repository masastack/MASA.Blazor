---
title: Combobox
desc: "**MCombobox** is a component that allows users to input values that are not present in the provided items, extending the functionality of [MAutocomplete](/blazor/components/autocompletes)."
release: v1.10.0
related:
  - /blazor/components/autocompletes
  - /blazor/components/cascaders
  - /blazor/components/selects
---

> This component was introduced in [v1.10.0](/blazor/getting-started/release-notes?v=v1.10.0).

## Usage

<masa-example file="Examples.components.combobox.Usage"></masa-example>

## Examples

### Props

#### Delimiters

When users type the Enter and Tab keys, the **MCombobox** component adds the input value to the options. You can set additional delimiters using the `Delimiters` property.

<masa-example file="Examples.components.combobox.Delimiters"></masa-example>

#### Chips

Double-clicking the default chips allows you to edit the text.

<masa-example file="Examples.components.combobox.Chips"></masa-example>

### Contents

#### No Data

Customizing the no data content slot can prompt users to input content.

<masa-example file="Examples.components.combobox.NoDataContent"></masa-example>
