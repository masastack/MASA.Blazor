---
title: Dialogs
desc: "The **Dialog** component inform users about a specific task and may contain critical information, require decisions, or involve multiple tasks. Use dialogs sparingly because they are interruptive."
related:
  - /blazor/components/buttons
  - /blazor/components/cards
  - /blazor/components/menus
---

## Usage

A dialog contains two slots, one for its activator and one for its content (default). Good for Privacy Policies.

<masa-example file="Examples.components.dialogs.Usage"></masa-example>

## Examples

### Props

#### Fullscreen

Due to limited space, full-screen dialogs may be more appropriate for mobile devices than dialogs used on devices with larger screens.

<masa-example file="Examples.components.dialogs.Fullscreen"></masa-example>

#### Transitions

You can make the dialog appear from the top or the bottom.

<masa-example file="Examples.components.dialogs.Transitions"></masa-example>

#### Persistent

Similar to a Simple Dialog, except that it’s not dismissed when touching outside or pressing esc key.

<masa-example file="Examples.components.dialogs.Persistent"></masa-example>

#### Scrollable

Example of a dialog with scrollable content.

<masa-example file="Examples.components.dialogs.Scrollable"></masa-example>

### Misc

#### Form

Just a simple example of a form in a dialog.

<masa-example file="Examples.components.dialogs.Form"></masa-example>

#### Loader

The **MDialog** component makes it easy to create a customized loading experience for your application.

<masa-example file="Examples.components.dialogs.Loader"></masa-example>

#### Nesting

Dialogs can be nested: you can open one dialog from another.

<masa-example file="Examples.components.dialogs.Nesting"></masa-example>

#### Overflowed

Modals that do not fit within the available window space will scroll the container.

<masa-example file="Examples.components.dialogs.Overflowed"></masa-example>

#### Without activator

If for some reason you are unable to use the activator slot, be sure to add the `OnClickStopPropagation` modifier to the event that triggers the dialog.

<masa-example file="Examples.components.dialogs.WithoutActivator"></masa-example>

