---
title: Text fields
desc: "Text fields components are used for collecting user provided information."
related:
  - /blazor/components/forms
  - /blazor/components/selects
  - /blazor/components/textareas
---

## Usage

A simple text field with placeholder and/or label.

<text-fields-usage></text-fields-usage>

## Anatomy

The recommended placement of elements inside of `MTextField` is:

* Place a `MIcon` at the start of the input or label
* Place label after prepended content

![Text-field Anatomy](https://cdn.masastack.com/stack/doc/masablazor/anatomy/text-field-anatomy.png)

| Element / Area | Description |
| - | - |
| 1. Container | The Text field container contains the `MInput` and `MField` components |
| 2. Prepend icon | A custom icon that is located before `MTextField` |
| 3. Prepend-inner icon | A custom icon that is located at the start of `MTextField` |
| 4. Label | A content area for displaying text to users that correlates to the input |
| 5. Append-inner icon | A custom icon that is located at the end of `MTextField` component |
| 6. Append icon | A custom icon that is located after `MTextField` component |

## Examples

### Props

#### Counter

Use a `Counter` prop to inform a user of the character limit. The counter does not perform any validation by itself -
you will need to pair it with either the internal validation system, or a 3rd party library. The counter can be
customised with the `CounterValue` prop and **CounterContent**.

<masa-example file="Examples.components.text_fields.Counter"></masa-example>

#### Clearable

When `Clearable`, you can customize the clear icon with `ClearIcon`.

<masa-example file="Examples.components.text_fields.Clearable"></masa-example>

#### Custom colors

You can optionally change a text field into any color in the Material design palette. Below is an example implementation of a custom form with validation.

<masa-example file="Examples.components.text_fields.CustomColors"></masa-example>

#### Custom text colors

You can optionally change a text in the input box into any color in the Material design palette.

<masa-example file="Examples.components.text_fields.CustomTextColors"></masa-example>

#### Dense

You can reduce the text field height with `Dense` prop.

<masa-example file="Examples.components.text_fields.Dense"></masa-example>

#### Disabled and readonly

Text fields can be `Disabled` or `Readonly`.

<masa-example file="Examples.components.text_fields.DisabledAndReadonly"></masa-example>

#### Filled

Text fields can be used with an alternative box design.

<masa-example file="Examples.components.text_fields.Filled"></masa-example>

#### HideDetails

When `HideDetails` is set to `auto` messages will be rendered only if there’s a message (hint, error message, counter value etc) to display.

<masa-example file="Examples.components.text_fields.HideDetails"></masa-example>

#### Hint

The `Hint` property on text fields adds the provided string beneath the text field. Using `PersistentHint` keeps the
hint visible when the text field is not focused. Hint prop is not supported in `Solo` mode.

<masa-example file="Examples.components.text_fields.Hint"></masa-example>

#### Icons

You can add icons to the text field with `PrependIcon`, `AppendIcon` and `AppendOuterIcon` props.

<masa-example file="Examples.components.text_fields.Icons"></masa-example>

#### Input Number

Numeric-only input box.

<masa-example file="Examples.components.text_fields.Number"></masa-example>

#### Outlined

Text fields can be used with an alternative outlined design.

<masa-example file="Examples.components.text_fields.Outlined"></masa-example>

#### Prefixes and suffixes

The `Prefix` and `Suffix` properties allows you to prepend and append inline non-modifiable text next to the text field.

<masa-example file="Examples.components.text_fields.PrefixesAndSuffixes"></masa-example>

#### Shaped

`Shaped` text fields are rounded if they’re `Outlined` and have higher border-radius if `Filled`.

<masa-example file="Examples.components.text_fields.Shaped"></masa-example>

#### SingleLine

`SingleLine` text fields do not float their label on focus or with data.

<masa-example file="Examples.components.text_fields.SingleLine"></masa-example>

#### Solo

Text fields can be used with an alternative `Solo` design.

<masa-example file="Examples.components.text_fields.Solo"></masa-example>

#### Validation

MASA Blazor includes simple validation through the `Rules` prop. The prop accepts a mixed array of types function, boolean and string. When the input value changes, each element in the array will be validated. Functions pass the current value as an argument and must return either true / false or a string containing an error message.

<masa-example file="Examples.components.text_fields.Validation"></masa-example>

### Events

#### IconEvents

`OnPrependClick`, `OnAppendClick`, `OnAppendOuterClick`, and `OnClearClick` will be emitted when you click on the respective icon.
Note that these events will not be fired if the icon content is used instead.

<masa-example file="Examples.components.text_fields.IconEvents"></masa-example>

### Contents

#### IconSlots

Instead of using `Prepend`/`Append`/`AppendOuter` icons you can use contents to extend input’s functionality.

<masa-example file="Examples.components.text_fields.IconSlots"></masa-example>

#### Label

Text field label can be defined in **LabelContent**。

<masa-example file="Examples.components.text_fields.Label"></masa-example>

#### Progress

You can display a progress bar instead of the bottom line. You can use the default indeterminate progress having same
color as the text field or designate a custom one using the **ProgressContent**

<masa-example file="Examples.components.text_fields.Progress"></masa-example>

### Misc

#### Full width with counter

Full width text fields allow you to create boundless inputs. In this example, we use a **MDivider** to separate the fields.

<masa-example file="Examples.components.text_fields.FullWidthWithCounter"></masa-example>

#### Password input

Using the HTML input type [password](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input/password) can be
used with an appended icon and callback to control the visibility.

<masa-example file="Examples.components.text_fields.PasswordInput"></masa-example>

