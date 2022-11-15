---
title: Date pickers
desc: "**MDatePicker** is a fully featured date selection component that lets users select a date, or range of dates."
related:
  - /components/buttons
  - /components/text-fields
  - /components/time-pickers
---

## Usage

Date pickers come in two orientation variations, portrait (default) and landscape. 
By default they are emitting input event when the day (for date picker) or month (for month picker), 
but with `Reactive` prop they can update the model even after clicking year/month.

<date-pickers-usage></date-pickers-usage>

## Caveats

<!--alert:warning--> 
**MDatePicker** accepts ISO 8601 * * date * * string (yyyy-mm-dd). For more information on ISO 8601 and other standards, please visit ISO (International Organization for Standardization) [international standards] https://www.iso.org/standards.html Official website.

## Examples

### Props

#### AllowedDates

You can specify allowed dates using arrays, objects, and functions.

<example file="" />

#### Colors

Date picker colors can be set using the Color and `HeaderColor` props. If `HeaderColor` prop is not provided header will
use the `Color` prop value.

<example file="" />

#### Elevation

The **MDatePicker** component supports elevation up to a maximum value of 24. For more information on elevations, visit
the official [Material Design elevations](https://material.io/design/environment/elevation.html) page.

<example file="" />

#### Icons

Use the `Color`prop You can override the default icons used in the picker.

<example file="" />

#### Multiple

Date picker can now select multiple dates with the `Multiple` prop. If using `Multiple` then date picker expects its
model to be an array.

<example file="" />

#### Picker date

You can watch the `OnPickerDateUpdate` which is the displayed month/year (depending on the picker type and active
view) to perform some action when it changes. This uses the .sync modifier.

<example file="" />

#### Range

Date picker can select date range with the  prop. When using `Range` prop date picker expects its model to be
an array of length 2 or empty.

<example file="" />

#### Readonly

Selecting new date could be disabled by adding `Readonly` prop.

<example file="" />

#### ShowCurrent

By default the current date is displayed using outlined button - `ShowCurrent` prop allows you to remove the border or select different date to be displayed as the current one.

<example file="" />

#### ShowAdjacentMonths

By default days from previous and next months are not visible. They can be displayed using the `ShowAdjacentMonths` prop.

<example file="" />

#### Width

Use the `Width` prop You can specify the picker’s width or make it full width.

<example file="" />

### Event

#### Date buttons(TODO)

<example file="" />

#### DateEvents(TODO)

<example file="" />

### Misc

#### Active picker

You can create a birthday picker - starting with year picker by default, restricting dates range and closing the picker
menu after selecting the day make the perfect birthday picker.

<example file="" />

#### DialogAndMenu

When integrating a picker into a **MTextField**, it is recommended to use the `Readonly` prop. This will prevent mobile
keyboards from triggering. To save vertical space, you can also hide the picker title.

Pickers expose a slot that allow you to hook into save and cancel functionality. This will maintain an old value which
can be replaced if the user cancels.

<example file="" />

#### Formatting

If you need to display the date in a custom format (different from YYYY-MM-DD), you need to use the `Formatting` prop to format the function.

<example file="" />

#### FormattingWithExternalLibraries(TODO)

Formatting dates is possible also with external libs such as Moment.js or date-fns

<example file="" />

#### Internationalization(TODO)

The date picker supports internationalization through the JavaScript Date object. Specify a BCP 47 language tag using the locale prop, and then set the first day of the week with the first-day-of-week prop.

<example file="" />

#### Orientation

Date pickers come in two orientation variations, portrait (default) and landscape.

<example file="" />