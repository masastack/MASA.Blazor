---
title: Breadcrumbs
desc: "Breadcrumbs component is suitable for page-level navigation."
related:
  - /blazor/components/buttons
  - /blazor/components/navigation-drawers
  - /blazor/components/icons
---

## Usage

By default, breadcrumbs use a text divider. This can be any string.

<breadcrumbs-usage></breadcrumbs-usage>

## Caveats

<app-alert type="info" content="By default **MBreadcrumbs** will disable the linkage with router. You can enable the feature by using `Linkage` prop."></app-alert>

## Examples

### Props

#### Divider

For the icon variant, breadcrumbs can use any icon in Material Design Icons.

<masa-example file="Examples.components.breadcrumbs.Divider"></masa-example>

#### Routable

<masa-example file="Examples.components.breadcrumbs.Routable"></masa-example>

#### Large

Breadcrumbs Basic Usage

<masa-example file="Examples.components.breadcrumbs.Large"></masa-example>

### Contents

#### Icon Dividers

For the icon variant, breadcrumbs can use any icon in Material Design Icons.

<masa-example file="Examples.components.breadcrumbs.IconDividers"></masa-example>

#### Item

You can use the **ItemContent** slot to customize each breadcrumb.

<masa-example file="Examples.components.breadcrumbs.Item"></masa-example>