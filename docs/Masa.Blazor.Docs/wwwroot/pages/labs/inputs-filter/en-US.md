---
title: Inputs filter
desc: "The **MInputsFilter** component provides the ability to trigger a filter action when the user presses the enter key, select an item or clear the input. Usually used with the **MDataTable** component."
related:
  - /blazor/components/text-fields
  - /blazor/labs/date-digital-clock-pickers
---

## Usage

The following form input components are supported:

| Component     | Action to trigger the filter                                                  |
|---------------|-------------------------------------------------------------------------------|
| MAutocomplete | Select an item, or click the outside when multiple, or click the clear button |
| MCascader     | Select the last item, or click the clear button                               |
| MCheckbox     | Toggle the checkbox                                                           |
| MRadioGroup   | Select a radio button                                                         |
| MSelect       | Select an item, or click the outside when multiple, or click the clear button |
| MSwitch       | Toggle the switch                                                             |
| MTextField    | Press the enter key, or click the clear button                                |

<masa-example file="Examples.labs.inputs_filter.Usage"></masa-example>

## Data filter

**PDataFilter** component has a built-in **MInputsFilter** component, which includes the built-in reset, search and expand/collapse actions.

<masa-example file="Examples.labs.inputs_filter.DataFilter"></masa-example>