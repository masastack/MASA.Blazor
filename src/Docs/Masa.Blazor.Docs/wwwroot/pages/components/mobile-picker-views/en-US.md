---
title: Mobile picker views
desc: "A picker designed for the mobile. Provides multiple sets of options for users to choose, and supports single-column selection, multi-column selection and cascading selection."
tag: Preset
related:
  - /blazor/components/mobile-pickers
  - /blazor/components/mobile-date-time-pickers
  - /blazor/components/mobile-time-pickers
---

**MMobilePickerView** is the content area of [PMobilePicker](/blazor/components/mobile-pickers).

## Examples

### Props

#### Cascade

Use the cascading `Columns` and `ItemChildren` fields to achieve the effect of cascading options.

<!--alert:warning-->
The data nesting depth of the cascade selection needs to be consistent, and if some of the options do not have sub
options, you can use an empty string for placeholder.
<!--/alert:warning-->

<masa-example file="Examples.components.mobil_picker_views.Cascade"></masa-example>

#### Disable item

<masa-example file="Examples.components.mobil_picker_views.ItemDisabled"></masa-example>

#### Custom item height

You can customize the height of the option through `Itemheight`. Currently, only `px` is supported.

<masa-example file="Examples.components.mobil_picker_views.ItemHeight"></masa-example>
