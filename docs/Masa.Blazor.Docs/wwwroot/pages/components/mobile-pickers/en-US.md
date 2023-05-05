---
title: Mobile pickers
desc: "A picker designed for the mobile. Provides multiple sets of options for users to choose, and supports single-column selection, multi-column selection and cascading selection."
related:
  - /blazor/components/mobile-picker-views
  - /blazor/components/mobile-date-time-views
  - /blazor/components/mobile-time-pickers
---

## Examples

### Props

#### Cascade

Use the cascading `Columns` and `ItemChildren` fields to achieve the effect of cascading options.

<app-alert type="warning" content="The data nesting depth of the cascade selection needs to be consistent, and if some of the options do not have sub
options, you can use an empty string for placeholder."></app-alert>

<masa-example file="Examples.components.mobile_pickers.Cascade"></masa-example>

#### Custom item height.

You can customize the height of the option through `Itemheight`. Currently, only `px` is supported.

<masa-example file="Examples.components.mobile_pickers.ItemHeight"></masa-example>


