---
title: Badges
desc: "The **MBadge** component overwrites or subscribes to an icon like an avatar or text on the content to highlight the user's information or just draw attention to a specific element. The content in the badge usually contains numbers or icons."
related:
  - /blazor/components/avatars
  - /blazor/components/icons
  - /blazor/components/toolbars
---

## Usage

Badges in their simplest form display to the upper right of the content that it wraps and requires the badge slot.

<badges-usage></badges-usage>

## Examples

### Misc

#### Customization options

The **MBadge** component is flexible and can be used in various use cases for many elements. The options for adjusting the position can also be passed through `offset-x` and `offset-y` props.

<masa-example file="Examples.components.badges.Customize"></masa-example>

#### Dynamic notifications 

You can combine badges with dynamic content to create things like notification systems.

<masa-example file="Examples.components.badges.DynamicNotification"></masa-example>

#### Show on hover

You can do many things with visibility controls, for example, show badges on mouse hover.

<masa-example file="Examples.components.badges.Hover"></masa-example>

#### Tabs

Badges help convey information to users in various ways.

<masa-example file="Examples.components.badges.Tabs"></masa-example>


