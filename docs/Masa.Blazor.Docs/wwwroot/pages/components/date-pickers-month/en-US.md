---
title: Date pickers month
desc: "**MDatePicker** can be used as a standalone month picker component."
related:
  - /blazor/components/date-pickers
  - /blazor/components/menus
  - /blazor/components/time-pickers
---

## Usage

Month pickers come in two orientation variations, portrait (default) and landscape.

<masa-example file="Examples.components.date_pickers_month.Usage"></masa-example>

## Caveats

<app-alert type="warning" content="**MDatePicker** accepts ISO 8601 * * date * * string (yyyy-mm-dd). For more information on ISO 8601 and other standards, please visit ISO (International Organization for Standardization) [international standards] https://www.iso.org/standards.html Official website."></app-alert>

## Examples

### Props

#### AllowedMonths

You can specify allowed months using arrays, objects or functions.

<masa-example file="Examples.components.date_pickers_month.AllowedDates"></masa-example>

#### Colors

Month picker colors can be set using the `Color` and `HeaderColor` props. If `HeaderColor` prop is not provided
header will use the `Color` prop value.

<masa-example file="Examples.components.date_pickers_month.Colors"></masa-example>

#### Icons

Use the `Icons` prop You can override the default icons used in the picker.

<masa-example file="Examples.components.date_pickers_month.Icons"></masa-example>

#### Multiple

Month pickers can now select multiple months with the `Multiple` prop. If using `Multiple` then the month picker expects its model to be an array.

<masa-example file="Examples.components.date_pickers_month.Multiple"></masa-example>

#### Readonly

Selecting new date could be disabled by adding `Readonly` prop.

<masa-example file="Examples.components.date_pickers_month.Readonly"></masa-example>

#### ShowCurrent

By default the current month is displayed using outlined button - `ShowCurrent` prop allows you to remove the border or select different month to be displayed as the current one.

<masa-example file="Examples.components.date_pickers_month.ShowCurrent"></masa-example>

#### Width

Use the `Width` prop You can specify allowed the picker’s width or make it full width.

<masa-example file="Examples.components.date_pickers_month.Width"></masa-example>

### Misc

#### DialogAndMenu

When integrating a picker into a **MTextField**, it is recommended to use the `Readonly` prop. This will prevent mobile
keyboards from triggering. To save vertical space, you can also hide the picker title.

Pickers expose a slot that allow you to hook into save and cancel functionality. This will maintain an old value which
can be replaced if the user cancels.

<masa-example file="Examples.components.date_pickers_month.DialogAndMenu"></masa-example>

#### Orientation

Date pickers come in two orientation variations, portrait (default) and landscape.

<masa-example file="Examples.components.date_pickers_month.Orientation"></masa-example>