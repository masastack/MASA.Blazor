---
title: Date digital clock pickers
desc: "The **PDateDigitalClockPicker** is a date-time selection component with a digital clock."
tag: "Preset"
related:
  - /blazor/components/date-pickers
  - /blazor/components/digital-clocks
  - /blazor/components/date-time-pickers
---

## Usage

<masa-example file="Examples.components.date_digital_clock_pickers.Picker"></masa-example>

Using the `DateTimePickerViewType` enumeration to set the rendering mode of the view:

| Enum item | Description                                                                                                   |
|-----------|---------------------------------------------------------------------------------------------------------------|
| `Auto`    | Switch to Desktop or Mobile view automatically according to Mobile breakpoint                                 |
| `Compact` | Always show compact view, but automatically use menu or dialog pop-up according to Mobile breakpoint          |
| `Dialog`  | Always use dialog pop-up, but automatically choose whether to use compact view according to Mobile breakpoint |
| `Desktop` | Desktop view, use non-compact view and use menu pop-up                                                        |
| `Mobile`  | Mobile view, use compact view and use dialog pop-up                                                           |

## Examples

### Configure default activator {#default-activator released-on=v1.7.0}

By default, use a [MTextField](/blazor/components/text-fields) as the activator. You can configure the default activator with **PDefaultDateTimePickerActivator**.

<masa-example file="Examples.components.date_digital_clock_pickers.DefaultActivator"></masa-example>

### Custom activator {#custom-activator}

Customize the activator using the `ActivatorContent` slot.

<masa-example file="Examples.components.date_digital_clock_pickers.CustomActivator"></masa-example>

## View components

### DateDigitalClockPickerView

<masa-example file="Examples.components.date_digital_clock_pickers.Default"></masa-example>

### DateDigitalClockCompactPickerView

<masa-example file="Examples.components.date_digital_clock_pickers.Compact"></masa-example>
