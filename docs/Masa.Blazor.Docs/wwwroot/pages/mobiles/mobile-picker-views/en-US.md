---
title: Mobile picker views
desc: "A picker designed for the mobile. Provides multiple sets of options for users to choose, and supports single-column selection, multi-column selection and cascading selection."
tag: Preset
related:
  - /blazor/mobiles/mobile-pickers
  - /blazor/mobiles/mobile-date-time-pickers
  - /blazor/mobiles/mobile-time-pickers
---

**MMobilePickerView** is the content area of [PMobilePicker](/blazor/mobiles/mobile-pickers).

## Installation {released-on=v1.10.0}

```shell
dotnet add package Masa.Blazor.MobileComponents
```

## Examples

### Props

#### Cascade

Use the cascading `Columns` and `ItemChildren` fields to achieve the effect of cascading options.

<!--alert:warning-->
The data nesting depth of the cascade selection needs to be consistent, and if some of the options do not have sub
options, you can use an empty string for placeholder.
<!--/alert:warning-->

<masa-example file="Examples.mobiles.mobile_picker_views.Cascade"></masa-example>

#### Disable item

<masa-example file="Examples.mobiles.mobile_picker_views.ItemDisabled"></masa-example>

#### Custom item height

You can customize the height of the option through `Itemheight`. Currently, only `px` is supported.

<masa-example file="Examples.mobiles.mobile_picker_views.ItemHeight"></masa-example>
