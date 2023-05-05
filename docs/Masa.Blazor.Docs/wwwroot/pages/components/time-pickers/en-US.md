---
title: Time pickers
desc: "The MTimePicker is stand-alone component that can be utilized in many existing MASA Blazor components. It offers the user a visual representation for selecting the time."
related:
  - /blazor/components/buttons
  - /blazor/components/date-pickers
  - /blazor/components/text-fields
---

## Usage

`Subheaders` in their simplest form display a subheading with default theme.

<time-pickers-usage></time-pickers-usage>

## Examples

### Props

#### AllowedTimes

You can specify allowed times using arrays, objects, and functions. You can also specify time step/precision/interval -
e.g. 10 minutes.

<masa-example file="Examples.components.time_pickers.AllowedTimes"></masa-example>

#### AMPMInTitle

You can move AM/PM switch to picker’s title.

<masa-example file="Examples.components.time_pickers.AMPMInTitle"></masa-example>

#### Colors

Time picker colors can be set using the `Color` and `HeaderColor` props. If `HeaderColor` prop is not provided
header will use the `Color` prop value.

<masa-example file="Examples.components.time_pickers.Colors"></masa-example>

#### Disabled

You can’t interact with disabled picker.

<masa-example file="Examples.components.time_pickers.Disabled"></masa-example>

#### Elevation

Emphasize the **MTimePicker** component by providing an `Elevation` from 0 to 24. Elevation modifies the `box-shadow`
css property.

<masa-example file="Examples.components.time_pickers.Elevation"></masa-example>

#### Format

A time picker can be switched to 24hr format. Note that the `Format` prop defines only the way the picker is displayed,
picker’s value (model) is always in 24hr format.

<masa-example file="Examples.components.time_pickers.Format"></masa-example>

#### NoTitle

You can remove picker’s title.

<masa-example file="Examples.components.time_pickers.NoTitle"></masa-example>

#### Range

This is an example of joining pickers together using `Min` and `Max` prop.

<masa-example file="Examples.components.time_pickers.Range"></masa-example>

#### Readonly

use the `Readonly`prop Read-only picker behaves same as disabled one, but looks like default one.

<masa-example file="Examples.components.time_pickers.Readonly"></masa-example>

#### Scrollable

use the `Scrollable`prop You can edit time picker’s value using mouse wheel.

<masa-example file="Examples.components.time_pickers.Scrollable"></masa-example>

#### UseSeconds

use the `UseSeconds`prop Time picker can have seconds input.

<masa-example file="Examples.components.time_pickers.UseSeconds"></masa-example>

#### Width

use the `Width`prop You can specify the picker’s width or make it full width.

<masa-example file="Examples.components.time_pickers.Width"></masa-example>

### Misc

#### DialogAndMenu

Due to the flexibility of pickers, you can really dial in the experience exactly how you want it.

<masa-example file="Examples.components.time_pickers.DialogAndMenu"></masa-example>