---
title: Error handler
desc: "The `MErrorHandler` component is used to manage unhandled exceptions."
---

## Usage

<masa-example file="Examples.components.error_handler.Usage"></masa-example>

<app-alert type="warning" content="Exceptions that occur outside the MASA Blazor components can catch but do not prevent state refresh, for example: `<button @onclick=''>throw exception</button>`."></app-alert>

<app-alert content="When an exception occurs in the life cycle, try to use `ErrorContent` instead."></app-alert>

## Examples

### Props

#### Error popup type

**Toasts** is used by default to display an error message when an exception occurs. You can set the type of popup through `PopupType`. When setting `ErrorPopupType.Error`, `ErrorContent` will be used to handle errors.

<masa-example file="Examples.components.error_handler.PopupType"></masa-example>

#### Show error detail

Use `ShowDetail` to control whether the exception details are displayed.

<masa-example file="Examples.components.error_handler.ShowDetail"></masa-example>

### Contents

#### Error content

<masa-example file="Examples.components.error_handler.ErrorContent"></masa-example>

### Events

#### Custom handler

`OnHandle` replaces the default handler, which you can use if you need to use a custom error handler. `OnAfterHandle` is the event called after the exception handled.

<masa-example file="Examples.components.error_handler.CustomHandler"></masa-example>
