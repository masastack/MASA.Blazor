---
title: Breadcrumbs
desc: "Breadcrumbs component is suitable for page-level navigation."
related:
  - /components/buttons
  - /components/navigation-drawers
  - /components/icons
---

## Usage

By default, breadcrumbs use a text divider. This can be any string.

<breadcrumbs-usage></breadcrumbs-usage>

## Caveats

<!--alert:info-->
By default **MBreadcrumbs** will disable the linkage with router. You can enable the feature by using `Linkage` prop.

## Examples

### Props

#### Divider

For the icon variant, breadcrumbs can use any icon in Material Design Icons.

<masa-example file="Examples.breadcrumbs.Divider"></masa-example>

#### Linkage

<masa-example file="Examples.breadcrumbs.Linkage"></masa-example>

#### Large

Breadcrumbs Basic Usage

<masa-example file="Examples.breadcrumbs.Large"></masa-example>

### Contents

#### Icon Dividers

For the icon variant, breadcrumbs can use any icon in Material Design Icons.

<masa-example file="Examples.breadcrumbs.IconDividers"></masa-example>

#### Item

You can use the **ItemContent** slot to customize each breadcrumb.

<masa-example file="Examples.breadcrumbs.Item"></masa-example>