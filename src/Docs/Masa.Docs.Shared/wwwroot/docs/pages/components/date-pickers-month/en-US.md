---
title: Date pickers month
desc: "**MDatePicker** can be used as a standalone month picker component."
related:
  - /components/date-pickers
  - /components/menus
  - /components/time-pickers
---

## Usage

Month pickers come in two orientation variations, portrait (default) and landscape.

<date-pickers-month-usage></date-pickers-month-usage>

## Caveats

<!--alert:warning--> 
**MDatePicker** accepts ISO 8601 * * date * * string (yyyy-mm-dd). For more information on ISO 8601 and other standards, please visit ISO (International Organization for Standardization) [international standards] https://www.iso.org/standards.html Official website.

## Examples

### Props

#### AllowedMonths

You can specify allowed months using arrays, objects or functions.

<example file="" />

#### Colors

Month picker colors can be set using the `Color` and `HeaderColor` props. If `HeaderColor` prop is not provided
header will use the `Color` prop value.

<example file="" />

#### Icons

Use the `Icons` prop You can override the default icons used in the picker.

<example file="" />

#### Multiple

Month pickers can now select multiple months with the `Multiple` prop. If using `Multiple` then the month picker expects its model to be an array.

<example file="" />

#### Readonly

Selecting new date could be disabled by adding `Readonly` prop.

<example file="" />

#### ShowCurrent

By default the current month is displayed using outlined button - `ShowCurrent` prop allows you to remove the border or select different month to be displayed as the current one.

<example file="" />

#### Width

Use the `Width` prop You can specify allowed the picker’s width or make it full width.

<example file="" />

### Misc

#### DialogAndMenu

When integrating a picker into a **MTextField**, it is recommended to use the `Readonly` prop. This will prevent mobile
keyboards from triggering. To save vertical space, you can also hide the picker title.

Pickers expose a slot that allow you to hook into save and cancel functionality. This will maintain an old value which
can be replaced if the user cancels.

<example file="" />

#### Internationalization(TODO)

The date picker supports internationalization through the JavaScript Date object. Specify a BCP 47 language tag using the locale prop, and then set the first day of the week with the first-day-of-week prop.

<example file="" />

#### Orientation

Date pickers come in two orientation variations, portrait (default) and landscape.

<example file="" />