---
title: Date pickers
desc: "**MDatePicker** is a fully featured date selection component that lets users select a date, or range of dates."
related:
  - /blazor/components/buttons
  - /blazor/components/text-fields
  - /blazor/components/time-pickers
---

## Usage

Date pickers come in two orientation variations, portrait (default) and landscape. 
By default they are emitting input event when the day (for date picker) or month (for month picker), 
but with `Reactive` prop they can update the model even after clicking year/month.

<masa-example file="Examples.components.date_pickers.Usage"></masa-example>

## Caveats

<app-alert type="warning" content="**MDatePicker** accepts ISO 8601 * * date * * string (yyyy-mm-dd). For more information on ISO 8601 and other standards, please visit ISO (International Organization for Standardization) [international standards] https://www.iso.org/standards.html Official website."></app-alert>

## Examples

### Props

#### AllowedDates

You can specify allowed dates using arrays, objects, and functions.

<masa-example file="Examples.components.date_pickers.AllowedDates"></masa-example>

#### Colors

Date picker colors can be set using the Color and `HeaderColor` props. If `HeaderColor` prop is not provided header will
use the `Color` prop value.

<masa-example file="Examples.components.date_pickers.Colors"></masa-example>

#### Elevation

The **MDatePicker** component supports elevation up to a maximum value of 24. For more information on elevations, visit
the official [Material Design elevations](https://material.io/design/environment/elevation.html) page.

<masa-example file="Examples.components.date_pickers.Elevation"></masa-example>

#### Icons

Use the `Color`prop You can override the default icons used in the picker.

<masa-example file="Examples.components.date_pickers.Icons"></masa-example>

#### Multiple

Date picker can now select multiple dates with the `Multiple` prop. If using `Multiple` then date picker expects its
model to be an array.

<masa-example file="Examples.components.date_pickers.Multiple"></masa-example>

#### Picker date

You can watch the `OnPickerDateUpdate` which is the displayed month/year (depending on the picker type and active
view) to perform some action when it changes. This uses the .sync modifier.

<masa-example file="Examples.components.date_pickers.PickerDate"></masa-example>

#### Range

Date picker can select date range with the  prop. When using `Range` prop date picker expects its model to be
an array of length 2 or empty.

<masa-example file="Examples.components.date_pickers.Range"></masa-example>

#### Readonly

Selecting new date could be disabled by adding `Readonly` prop.

<masa-example file="Examples.components.date_pickers.Readonly"></masa-example>

#### ShowCurrent

By default the current date is displayed using outlined button - `ShowCurrent` prop allows you to remove the border or select different date to be displayed as the current one.

<masa-example file="Examples.components.date_pickers.ShowCurrent"></masa-example>

#### ShowAdjacentMonths

By default days from previous and next months are not visible. They can be displayed using the `ShowAdjacentMonths` prop.

<masa-example file="Examples.components.date_pickers.ShowSiblingMonths"></masa-example>

#### Width

Use the `Width` prop You can specify the picker’s width or make it full width.

<masa-example file="Examples.components.date_pickers.Width"></masa-example>

### Event

#### DateEvents

You can specify events using arrays or functions. To change the default color of the event use `EventColor` parameter. Your `Events` function can return an array of colors (material or css) in case you want to display multiple event indicators.

<masa-example file="Examples.components.date_pickers.DateEvents"></masa-example>

### Misc

#### Active picker

You can create a birthday picker - starting with year picker by default, restricting dates range and closing the picker
menu after selecting the day make the perfect birthday picker.

<masa-example file="Examples.components.date_pickers.ActivePicker"></masa-example>

#### DialogAndMenu

When integrating a picker into a **MTextField**, it is recommended to use the `Readonly` prop. This will prevent mobile
keyboards from triggering. To save vertical space, you can also hide the picker title.

Pickers expose a slot that allow you to hook into save and cancel functionality. This will maintain an old value which
can be replaced if the user cancels.

<masa-example file="Examples.components.date_pickers.DialogAndMenu"></masa-example>

#### Formatting

If you need to display the date in a custom format (different from YYYY-MM-DD), you need to use the `Formatting` prop to format the function.

<masa-example file="Examples.components.date_pickers.Formatting"></masa-example>

#### Internationalization

Specify a [BCP 47](https://tools.ietf.org/html/bcp47) language tag using the **Locale** prop, and then set the first day of the week with the **FirstDayOfWeek** prop.

<masa-example file="Examples.components.date_pickers.Internationalization"></masa-example>

#### Orientation

Date pickers come in two orientation variations, portrait (default) and landscape.

<masa-example file="Examples.components.date_pickers.Orientation"></masa-example>