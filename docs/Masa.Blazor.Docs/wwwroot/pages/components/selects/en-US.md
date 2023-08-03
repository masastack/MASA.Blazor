---
title: Selects
desc: "The **Select** component is used for collecting user provided information from a list of options."
related:
  - /blazor/components/autocompletes
  - /blazor/components/combobox
  - /blazor/components/forms
---

## Usage

<selects-usage></selects-usage>

## Caveats

<app-alert type="info" content="The default setting of browser auto completion is off, which may be changed or ignored by different browsers. **[MDN](https://developer.mozilla.org/en-US/docs/Web/Security/Securing_your_site/Turning_off_form_autocompletion)**"></app-alert>

<app-alert type="warning" content="The `Auto` attribute of `MenuProps` only supports the default input style."></app-alert>

<app-alert type="error" content="When using an object as the attribute of `Items`, you must use `ItemText` and `ItemValue` to associate with the incoming object. These values are `Text` and `Value` by default and can be changed."></app-alert>

## Examples

### Props

#### Custom text and value

You can specify the specific properties within your items array correspond to the text and value fields. 
By default, this is text and value. In this example we also use the return-object prop which will return the entire object of the selected item on selection.

<masa-example file="Examples.components.selects.CustomTextAndValue"></masa-example>

#### Dense

You can use `Dense` prop to reduce the field height and lower max height of list items.

<masa-example file="Examples.components.selects.Dense"></masa-example>

#### Disabled

Applying the `Disabled` prop to a **MSelect** will prevent a user from interacting with the component.

<masa-example file="Examples.components.selects.Disabled"></masa-example>

#### Icon

Use a custom prepended or appended icon.

<masa-example file="Examples.components.selects.Icon"></masa-example>

#### Light

A standard single select has a multitude of configuration options.

<masa-example file="Examples.components.selects.Light"></masa-example>

#### MenuProps

Custom props can be passed directly to **MMenu** using `MenuProps` prop. In this example menu is force directed to top and shifted to top.

<masa-example file="Examples.components.selects.MenuProps"></masa-example>

#### Multiple

A multi-select can utilize **MChip** as the display for selected items.

<masa-example file="Examples.components.selects.Multiple"></masa-example>

#### Readonly

You can use the `Readonly` prop on **MSelect** which will prevent a user from changing its value.

<masa-example file="Examples.components.selects.Readonly"></masa-example>

#### Contents

### Append and prepend item

The **MSelect** components can be optionally expanded with prepended and appended items. This is perfect for customized select-all functionality.

<masa-example file="Examples.components.selects.AppendAndPrependItem"></masa-example>

### Selection

The **SelectionContent** can be used to customize the way selected values are shown in the input. This is great when you want something like `foo (+2 others)` or don’t want the selection to occupy multiple lines.

<masa-example file="Examples.components.selects.Selection"></masa-example>
