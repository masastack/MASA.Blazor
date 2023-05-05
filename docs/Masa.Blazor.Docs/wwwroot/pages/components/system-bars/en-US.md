---
title: System bars
desc: "The **MSystemBar** component can be used to display status to the user. It looks like an Android system bar and can contain icons, spaces, and some text."
related:
  - /blazor/components/buttons
  - /blazor/components/toolbars
  - /blazor/components/tabs
---

## Usage

**MSystemBar** in its simplest form displays a small container with default theme.

<system-bars-usage></system-bars-usage>

## Anatomy

The recommended placement of elements inside of `MSystemBar` is:

* Place informational icons to the right
* Place time or other textual information to the far right

![System Bar Anatomy](https://cdn.masastack.com/stack/doc/masablazor/anatomy/system-bar-anatomy.png)

| Element / Area | Description |
| - | - |
| 1. Container | The System Bar container has a default slot with content justified right |
| 2. Icon items (optional) | Used to convey information through the use of icons |
| 3. Text (optional) | Textual content that is typically used to show time |

## Examples

### Props

#### Color

You can choose to use colors to change the color of the **MSystemBar**.

<masa-example file="Examples.components.system_bars.Color"></masa-example>

#### Lights Out

You can use the `LightsOut` property to reduce the opacity of the **MSystemBar**.

<masa-example file="Examples.components.system_bars.LightOut"></masa-example>

#### Themes

You can apply `Dark` or `Light` theme variables to **MSystemBar**.

<masa-example file="Examples.components.system_bars.Theme"></masa-example>

#### Window

System bar with window controls and status information.

<masa-example file="Examples.components.system_bars.Window"></masa-example>