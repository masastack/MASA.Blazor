---
title: Cells
desc: "Used for information display in various category rows."
release: v1.11.0
related:
  - /blazor/mobiles/swipe-actions
---

## Installation

```shell
dotnet add package Masa.Blazor.MobileComponents
```

## Usage

The `Value` is used to display the content on the right side and is mutually exclusive with `ChildContent`. When `Href`
or `OnClick` exists, an arrow will be displayed on the right side.

<masa-example file="Examples.mobiles.cell.Usage"></masa-example>

## Example

### Props

#### Outlined

<masa-example file="Examples.mobiles.cell.Outlined"></masa-example>

### Misc

#### Weixin

<masa-example file="Examples.mobiles.cell.WeiXin"></masa-example>