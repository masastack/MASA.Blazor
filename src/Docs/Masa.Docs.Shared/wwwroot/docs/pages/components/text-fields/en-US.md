---
title: Text fields
desc: "Text fields components are used for collecting user provided information."
related:
  - /components/forms
  - /components/selects
  - /components/textareas
---

## Usage

A simple text field with placeholder and/or label.

<text-fields-usage></text-fields-usage>

## Examples

### Props

#### Counter

Use a `Counter` prop to inform a user of the character limit. The counter does not perform any validation by itself -
you will need to pair it with either the internal validation system, or a 3rd party library. The counter can be
customised with the `CounterValue` prop and **CounterContent**.

<example file="" />

#### Clearable

When `Clearable`, you can customize the clear icon with `ClearIcon`.

<example file="" />

#### Custom colors

You can optionally change a text field into any color in the Material design palette. Below is an example implementation of a custom form with validation.

<example file="" />

#### Custom text colors

You can optionally change a text in the input box into any color in the Material design palette.

<example file="" />

#### Dense

You can reduce the text field height with `Dense` prop.

<example file="" />

#### Disabled and readonly

Text fields can be `Disabled` or `Readonly`.

<example file="" />

#### Filled

Text fields can be used with an alternative box design.

<example file="" />

#### HideDatails

When `HideDetails` is set to `auto` messages will be rendered only if there’s a message (hint, error message, counter value etc) to display.

<example file="" />

#### Hint

The `Hint` property on text fields adds the provided string beneath the text field. Using `PersistentHint` keeps the
hint visible when the text field is not focused. Hint prop is not supported in `Solo` mode.

<example file="" />

#### Icons

You can add icons to the text field with `PrependIcon`, `AppendIcon` and `AppendOuterIcon` props.

<example file="" />

#### Input Number

Numeric-only input box.

<example file="" />

#### Outlined

Text fields can be used with an alternative outlined design.

<example file="" />

#### Prefixes and suffixes

The `Prefix` and `Suffix` properties allows you to prepend and append inline non-modifiable text next to the text field.

<example file="" />

#### Shaped

`Shaped` text fields are rounded if they’re `Outlined` and have higher border-radius if `Filled`.

<example file="" />

#### SingleLine

`SingleLine` text fields do not float their label on focus or with data.

<example file="" />

#### Solo

Text fields can be used with an alternative `Solo` design.

<example file="" />

#### Validation

MASA Blazor includes simple validation through the rules prop. The prop accepts a mixed array of types function, boolean and string. When the input value changes, each element in the array will be validated. Functions pass the current value as an argument and must return either true / false or a string containing an error message.

<example file="" />

### Events

#### IconEvents

`OnPrependClick`, `OnAppendClick`, `OnAppendOuterClick`, and `OnClearClick` will be emitted when you click on the respective icon.
Note that these events will not be fired if the icon content is used instead.

<example file="" />

### Contents

#### IconSlots

Instead of using `Prepend`/`Append`/`AppendOuter` icons you can use contents to extend input’s functionality.

<example file="" />

#### Label

Text field label can be defined in **LabelContent**。

<example file="" />

#### Progress

You can display a progress bar instead of the bottom line. You can use the default indeterminate progress having same
color as the text field or designate a custom one using the **ProgressContent**

<example file="" />

### Misc

#### Full width with counter

Full width text fields allow you to create boundless inputs. In this example, we use a **MDivider** to separate the fields.

<example file="" />

#### Password input

Using the HTML input type [password](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input/password) can be
used with an appended icon and callback to control the visibility.

<example file="" />

