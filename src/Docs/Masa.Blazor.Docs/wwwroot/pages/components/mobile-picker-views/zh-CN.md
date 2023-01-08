---
title: Mobile picker views（移动端选择视图）
desc: "专为移动设备设计的选择视图。提供多个选项集合供用户选择，支持单列选择、多列选择和级联选择。"
tag: "预置"
related:
  - /blazor/components/mobile-pickers
  - /blazor/components/mobile-date-time-pickers
  - /blazor/components/mobile-time-pickers
---

**MMobilePickerView** is the content area of [PMobilePicker](/blazor/components/mobile-pickers).

## 示例

### 属性

#### 级联

使用级联的 `Columns` 和 `ItemChildren` 字段可以实现选项级联的效果。

<!--alert:warning-->
级联选择的数据嵌套深度需要一致，如果某些选项没有子选项，则可以使用空字符串作为占位符。
<!--/alert:warning-->

<masa-example file="Examples.components.mobil_picker_views.Cascade"></masa-example>

#### 禁止选择某项

<masa-example file="Examples.components.mobil_picker_views.ItemDisabled"></masa-example>

#### 自定义项高度

通过 `Itemheight` 可以自定义选项的高度。目前只支持 `px` 。

<masa-example file="Examples.components.mobile_pickers.ItemHeight"></masa-example>
