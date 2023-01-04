---
title: Toast
desc: "The component is used to convey important information to the user through the use contextual types icons and color.These default types come in in 4 variations: `Success`,`Info`,`Warning`, and `Error`. Default icons are assigned which help represent different actions each type portrays and also can customized content to fit almost any situation."
tag: Preset
related:
  - /blazor/components/cards
  - /blazor/components/icons
  - /blazor/components/grid-system
---

## Usage

The **PToast** component can be used by PopupService, see the documentation of [PopupService](/blazor/components/popup-service) for details.

<masa-example file="Examples.components.toasts.Usage"></masa-example>

## Examples

### Props

#### Duration (default 4000ms)

<masa-example file="Examples.components.toasts.Duration"></masa-example>

#### MaxCount

`MaxCount` prop Set the maximum number of impressions。

<masa-example file="Examples.components.toasts.MaxCount"></masa-example>

#### Toast Position

`Value` prop Set the location of the message popup。

<masa-example file="Examples.components.toasts.Position"></masa-example>

### Event

#### Custom Toast content

<masa-example file="Examples.components.toasts.CustomToast"></masa-example>