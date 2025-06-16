---
title: Enqueued snackbars
desc: "Allows snackbars to be stacked on top of one another."
tag: Preset
related:
  - /blazor/components/snackbars
  - /blazor/components/dialogs
  - /blazor/components/popup-service
---

## Usage

Allows you to set the `Position` of the snackbar queue, whether it can be closed, and the max count.

<enqueued-snackbars-usage></enqueued-snackbars-usage>

## Examples

### Props

#### Type

**PEnqueuedSnackbars** uses an enhanced snackbar with a **MAlert** component nested, you can set up `Title`, `Content`, and `Type`.

<masa-example file="Examples.components.enqueued_snackbars.Type"></masa-example>

#### Filter Duplicate {released-on=v1.10.0}

By default, duplicate messages are filtered to prevent multiple identical messages from appearing in a short time, which can lead to message pile-up on the interface.

By setting the `DuplicateMessageFilterDuration` parameter, you can control the time window (in milliseconds) for filtering duplicate messages. Within this time window, if a message with the same title and content appears, it will be automatically filtered out. The default value is 1000 milliseconds (1 second).

If set to 0, duplicate message filtering will be disabled, and all messages will be displayed.

<masa-example file="Examples.components.enqueued_snackbars.FilterDuplicate"></masa-example>

### Events

#### OnAction

When the user completes an action, you can pop up a snackbar that provider an optional action, such as the _Undo_ in the example.

<masa-example file="Examples.components.enqueued_snackbars.Action"></masa-example>
