---
title: Modals
desc: "When requiring users to interact with the application, but without jumping to a new page and interrupting the user's workflow, you can use **Modal** to create a new floating layer over the current page to get user feedback or display information."
tag: Preset
related:
  - /blazor/components/dialogs
  - /blazor/components/popup-service
  - /blazor/components/drawers
--- 

## Usage

<masa-example file="Examples.components.modals.Usage"></masa-example>

## Examples

### Props

#### Action Props

<masa-example file="Examples.components.modals.ActionProps"></masa-example>

#### Actions

<masa-example file="Examples.components.modals.Actions"></masa-example>

#### FormModel {updated-in=v1.10.0}

`FormModel` property is similar to the `Model` property of the [MForm](/blazor/components/forms) component,
allowing built-in form validation functionality within the modal. The `OnSave` event is triggered after the form validation passes,
and the `OnValidating` event provides access to the validation results.

<masa-example file="Examples.components.modals.FormModel"></masa-example>

#### AutoScrollToTop

<masa-example file="Examples.components.modals.ScrollToTopOnHide"></masa-example>

#### Without activator

<masa-example file="Examples.components.modals.WithoutActivator"></masa-example>

### Contents

#### Custom actions

<masa-example file="Examples.components.modals.CustomActions"></masa-example>