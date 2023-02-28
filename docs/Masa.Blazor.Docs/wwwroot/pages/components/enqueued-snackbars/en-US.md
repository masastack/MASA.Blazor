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

### Events

#### OnAction

When the user completes an action, you can pop up a snackbar that provider an optional action, such as the _Undo_ in the example.

<masa-example file="Examples.components.enqueued_snackbars.Action"></masa-example>
