---
title: Time pickers
desc: "The MTimePicker is stand-alone component that can be utilized in many existing Vuetify components. It offers the user a visual representation for selecting the time."
related:
  - /components/buttons
  - /components/date-pickers
  - /components/text-fields
---

## Usage

`Subheaders` in their simplest form display a subheading with default theme.

<time-pickers-usage></time-pickers-usage>

## Examples

### Props

#### AllowedTimes

You can specify allowed times using arrays, objects, and functions. You can also specify time step/precision/interval -
e.g. 10 minutes.

<example file="" />

#### AMPMInTitle

You can move AM/PM switch to picker’s title.

<example file="" />

#### Colors

Time picker colors can be set using the `Color` and `HeaderColor` props. If `HeaderColor` prop is not provided
header will use the `Color` prop value.

<example file="" />

#### Disabled

You can’t interact with disabled picker.

<example file="" />

#### Elevation

Emphasize the **MTimePicker** component by providing an `Elevation` from 0 to 24. Elevation modifies the `box-shadow`
css property.

<example file="" />

#### Format

A time picker can be switched to 24hr format. Note that the `Format` prop defines only the way the picker is displayed,
picker’s value (model) is always in 24hr format.

<example file="" />

#### NoTitle

You can remove picker’s title.

<example file="" />

#### Range

This is an example of joining pickers together using `Min` and `Max` prop.

<example file="" />

#### Readonly

use the `Readonly`prop Read-only picker behaves same as disabled one, but looks like default one.

<example file="" />

#### Scrollable

use the `Scrollable`prop You can edit time picker’s value using mouse wheel.

<example file="" />

#### UseSeconds

use the `UseSeconds`prop Time picker can have seconds input.

<example file="" />

#### Width

use the `Width`prop You can specify the picker’s width or make it full width.

<example file="" />

### Misc

#### DialogAndMenu

Due to the flexibility of pickers, you can really dial in the experience exactly how you want it.

<example file="" />