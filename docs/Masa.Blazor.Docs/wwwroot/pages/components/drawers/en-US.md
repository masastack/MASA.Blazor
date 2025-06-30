---
title: Drawers
desc: ""
tag: "Preset"
related:
  - /blazor/components/dialogs
  - /blazor/components/popup-service
  - /blazor/components/modals
---

## Usage

<masa-example file="Examples.components.drawers.Usage"></masa-example>

## Examples

### Props

#### Action Props

<masa-example file="Examples.components.drawers.ActionProps"></masa-example>

#### Actions

<masa-example file="Examples.components.drawers.Actions"></masa-example>

#### FormModel {updated-in=v1.10.0}

`FormModel` property is similar to the `Model` property of the [MForm](/blazor/components/forms) component,
allowing built-in form validation functionality within the drawer. The `OnSave` event is triggered after the form validation passes,
and the `OnValidating` event provides access to the validation results.

<masa-example file="Examples.components.drawers.FormModel"></masa-example>

#### Left

<masa-example file="Examples.components.drawers.Left"></masa-example>

#### AutoScrollToTop

<masa-example file="Examples.components.drawers.ScrollToTopOnHide"></masa-example>

#### Without activator

<masa-example file="Examples.components.drawers.WithoutActivator"></masa-example>

### Contents

#### Custom actions

<masa-example file="Examples.components.drawers.CustomActions"></masa-example>
