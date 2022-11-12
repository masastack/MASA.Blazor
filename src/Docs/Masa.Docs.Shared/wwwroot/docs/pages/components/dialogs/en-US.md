---
title: Dialogs
desc: "The **Dialog** component inform users about a specific task and may contain critical information, require decisions, or involve multiple tasks. Use dialogs sparingly because they are interruptive."
related:
  - /components/buttons
  - /components/cards
  - /components/menus
---

## Usage

A dialog contains two slots, one for its activator and one for its content (default). Good for Privacy Policies.

<dialogs-usage></dialogs-usage>

## Examples

### Props

#### Fullscreen

Due to limited space, full-screen dialogs may be more appropriate for mobile devices than dialogs used on devices with larger screens.

<example file="" />

#### Transitions

You can make the dialog appear from the top or the bottom.

<example file="" />

#### Persistent

Similar to a Simple Dialog, except that it’s not dismissed when touching outside or pressing esc key.

<example file="" />

#### Scrollable

Example of a dialog with scrollable content.

<example file="" />

### Misc

#### Form

Just a simple example of a form in a dialog.

<example file="" />

#### Loader

The **MDialog** component makes it easy to create a customized loading experience for your application.

<example file="" />

#### Nesting

Dialogs can be nested: you can open one dialog from another.

<example file="" />

#### Overflowed

Modals that do not fit within the available window space will scroll the container.

<example file="" />

#### Without activator

If for some reason you are unable to use the activator slot, be sure to add the `StopPropagation` modifier to the event that triggers the dialog.

<example file="" />

