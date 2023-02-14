---
title: Windows
desc: "The **MWindow** component provides the baseline functionality for transitioning content from 1 pane to another. Other components such as **MTabs**, **MCarousel** and **MStepper** utilize this component at their core."
related:
  - /blazor/components/carousels
  - /blazor/components/steppers
  - /blazor/components/tabs
---

## Usage

Designed to easily cycle through content, **MWindow** provides a simple interface to create truly custom implementations.

<masa-example file="Examples.components.windows.Usage"></masa-example>

## Examples

### Props

#### Reverse

Reverse **MWindow** always displays reverse transition.

<masa-example file="Examples.components.windows.Reverse"></masa-example>

#### Vertical

**MWindow** can be vertical. Vertical windows have Y axis transition instead of X axis transition.

<masa-example file="Examples.components.windows.Vertical"></masa-example>

#### Customized Arrows

Arrows can be customized by using **PrevContent** and **NextContent** slots.

<masa-example file="Examples.components.windows.CustomizedArrows"></masa-example>

### Misc

#### Account Creation

Create rich forms with smooth animations. **MWindow** automatically tracks the current selection index to automatically change the transition direction. 
This can be manually controlled with the `Reverse` prop.

<masa-example file="Examples.components.windows.AccountCreation"></masa-example>

#### Onboarding

**MWindow** makes it easy to create custom components such as a different styled stepper.

<masa-example file="Examples.components.windows.Onboarding"></masa-example>