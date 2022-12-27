---
title: File inputs
desc: "The **MFileInput**  component is a specialized input that provides a clean interface for selecting files, showing detailed selection information and upload progress. It is meant to be a direct replacement for a standard file input."
related:
  - /blazor/components/text-fields
  - /blazor/components/forms
  - /blazor/components/icons
---

## Usage

At its core, the `MFileInput` component is a basic container that extends [MTextField](/blazor/components/text-fields).

<file-inputs-usage></file-inputs-usage>

## Examples

### Props

#### Accept

**MFileInput** component can accept only specific media formats/file types if you want. For more information, checkout the documentation on the [accept attribute](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input/file#accept).

<masa-example file="Examples.components.file_inputs.Accept"></masa-example>

#### Chips

A selected file can be displayed as a [chip](/blazor/components/chips). When using the `Chips` and `Multiple` props, each chip will be displayed (as opposed to the file count).

<masa-example file="Examples.components.file_inputs.Chips"></masa-example>

#### Counter

When using the `ShowSize` property along with `Counter`, the total number of files and size will be displayed under the input.

<masa-example file="Examples.components.file_inputs.Counter"></masa-example>

#### Dense

You can reduces the file input height with `Dense` prop.

<masa-example file="Examples.components.file_inputs.Dense"></masa-example>

#### Multiple

The **MFileInput** can contain **Multiple** files at the same time when using the multiple prop.

<masa-example file="Examples.components.file_inputs.Multiple"></masa-example>

#### PrependIcon

The **MFileInput** has a default `PrependIcon` that can be set on the component or adjusted globally. More information on changing global components can be found on the [customizing icons page](/blazor/features/icon-fonts).

<masa-example file="Examples.components.file_inputs.PrependIcon"></masa-example>

#### ShowSize

The displayed size of the selected file(s) can be configured with the `ShowSize` property. Display sizes can be either 1024 (the default used when providing **true**) or 1000.

<masa-example file="Examples.components.file_inputs.ShowSize"></masa-example>

#### Validation

Similar to other inputs, you can use the rules prop to create your own custom validation parameters.

<masa-example file="Examples.components.file_inputs.Validation"></masa-example>

### Contents

#### Selection

Using the **SelectionContent**, you can customize the appearance of your input selections. This is typically done with [chips](/blazor/blazor/components/chips), however any component or markup can be used.

<masa-example file="Examples.components.file_inputs.Selection"></masa-example>

### Misc

#### Complex selection content

The flexibility of the selection slot allows you accommodate complex use-cases. In this example we show the first 2 selections as chips while adding a number indicator for the remaining amount.

<masa-example file="Examples.components.file_inputs.ComplexSelectionContent"></masa-example>