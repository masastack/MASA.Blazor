---
title: Textareas
desc: "Textarea components are used for collecting large amounts of textual data."
related:
  - /blazor/components/forms
  - /blazor/components/selects
  - /blazor/components/text-fields
---

## Usage

**MTextarea** in its simplest form is a multi-line text-field, useful for larger amounts of text.

<textareas-usage></textareas-usage>

## Examples

### Props

#### Auto grow

When using the `AutoGrow` prop, textarea’s will automatically increase in size when the contained text exceeds its size.

<masa-example file="Examples.components.textareas.AutoGrow"></masa-example>

#### Background color

The `BackgroundColor` and `Color` give you more control over styling **MTextarea**.

<masa-example file="Examples.components.textareas.BackgroundColor"></masa-example>

#### Browser autocomplete

The `Autocomplete` prop gives you the option to enable the browser to predict user input.

<masa-example file="Examples.components.textareas.BrowserAutocomplete"></masa-example>

#### Clearable

You can clear the text from a **MTextarea** by using the `Clearable` prop, and customize the icon used with the `ClearableIcon` prop.

<masa-example file="Examples.components.textareas.Clearable"></masa-example>

#### Counter

The `Counter` prop informs the user of a character limit for the **MTextarea**.

<masa-example file="Examples.components.textareas.Counter"></masa-example>

#### Icon

The `AppendIcon` and `PrependIcon` props help add context to **MTextarea**.

<masa-example file="Examples.components.textareas.Icon"></masa-example>

#### NoResize

**MTextarea** have the option to remain the same size regardless of their content’s size, using the `NoResize` prop.

<masa-example file="Examples.components.textareas.NoResize"></masa-example>

#### Rows

The `Rows` prop allows you to define how many rows the textarea has, when combined with the `RowHeight` prop you can
further customize your rows by defining their height.

<masa-example file="Examples.components.textareas.Row"></masa-example>

### Misc

#### Signup form

Utilizing alternative input styles, you can create amazing interfaces that are easy to build and easy to use.

<masa-example file="Examples.components.textareas.SignupForm"></masa-example>