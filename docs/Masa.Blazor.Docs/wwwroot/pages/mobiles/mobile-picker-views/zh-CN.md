---
title: Mobile picker views（移动端选择视图）
desc: "专为移动设备设计的选择视图。提供多个选项集合供用户选择，支持单列选择、多列选择和级联选择。"
tag: "预置"
related:
  - /blazor/mobiles/mobile-pickers
  - /blazor/mobiles/mobile-date-time-pickers
  - /blazor/mobiles/mobile-time-pickers
---

**MMobilePickerView** is the content area of [PMobilePicker](/blazor/mobiles/mobile-pickers).

## 安装 {#installation released-on=v1.10.0}

```shell
dotnet add package Masa.Blazor.MobileComponents
```

## 示例 {#example}

### 属性 {#props}

#### 级联 {#cascade}

使用级联的 `Columns` 和 `ItemChildren` 字段可以实现选项级联的效果。

<!--alert:warning-->
级联选择的数据嵌套深度需要一致，如果某些选项没有子选项，则可以使用空字符串作为占位符。
<!--/alert:warning-->

<masa-example file="Examples.mobiles.mobile_picker_views.Cascade"></masa-example>

#### 禁止选择某项 {#disabled-item}

<masa-example file="Examples.mobiles.mobile_picker_views.ItemDisabled"></masa-example>

#### 自定义项高度 {#custom-item-height}

通过 `Itemheight` 可以自定义选项的高度。目前只支持 `px` 。

<masa-example file="Examples.mobiles.mobile_pickers.ItemHeight"></masa-example>
