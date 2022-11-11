---
title: Selects
desc: "The **Select** component is used for collecting user provided information from a list of options."
related:
  - /components/autocompletes
  - /components/combobox
  - /components/forms
---

## Usage

<selects-usage></selects-usage>

## 注意

<!--alert:info--> 
The default setting of browser auto completion is off, which may be changed or ignored by different browsers. **[MDN](https://developer.mozilla.org/en-US/docs/Web/Security/Securing_your_site/Turning_off_form_autocompletion)**
<!--/alert:info--> 

<!--alert:warning--> 
The **Auto** attribute of **menupprops** only supports the default input style.
<!--/alert:warning--> 

<!--alert:error--> 
When using an object as the attribute of **items**, you must use **itemtext** and **itemvalue** to associate with the incoming object. These values are **text** and **value** by default and can be changed.
<!--/alert:error--> 

## Examples

### Props

#### Custom text and value

You can specify the specific properties within your items array correspond to the text and value fields. 
By default, this is text and value. In this example we also use the return-object prop which will return the entire object of the selected item on selection.

<example file="" />

#### Dense

You can use `Dense` prop to reduce the field height and lower max height of list items.

<example file="" />

#### Disabled

Applying the `Disabled` prop to a **MSelect** will prevent a user from interacting with the component.

<example file="" />

#### Icon

Use a custom prepended or appended icon.

<example file="" />

#### Light

A standard single select has a multitude of configuration options.

<example file="" />

#### MenuProps

Custom props can be passed directly to **MMenu** using `MenuProps` prop. In this example menu is force directed to top and shifted to top.

<example file="" />

#### Multiple

A multi-select can utilize **MChip** as the display for selected items.

<example file="" />

#### Readonly

You can use the `Readonly` prop on **MSelect** which will prevent a user from changing its value.

<example file="" />

#### Contents

### Append and prepend item

The **MSelect** components can be optionally expanded with prepended and appended items. This is perfect for customized select-all functionality.

<example file="" />

### Selection

The **SelectionContent** can be used to customize the way selected values are shown in the input. This is great when you want something like `foo (+2 others)` or don’t want the selection to occupy multiple lines.

<example file="" />
