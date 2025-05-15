---
title: Mobile pickers（移动端选择器）
desc: "专为移动设备设计的选择器。提供多个选项集合供用户选择，支持单列选择、多列选择和级联选择。"
related:
  - /blazor/mobiles/mobile-picker-views
  - /blazor/mobiles/mobile-date-time-views
  - /blazor/mobiles/mobile-time-pickers
---

## 安装 {#installation released-on=v1.10.0}

```shell
dotnet add package Masa.Blazor.MobileComponents
```

## 示例 {#example}

### 属性 {#props}

#### 级联 {#cascade}

使用级联的 `Columns` 和 `ItemChildren` 字段可以实现选项级联的效果。

<app-alert type="warning" content="级联选择的数据嵌套深度需要一致，如果某些选项没有子选项，则可以使用空字符串作为占位符。"></app-alert>

<masa-example file="Examples.mobiles.mobile_pickers.Cascade"></masa-example>

#### 自定义项高度 {#custom-item-height}

通过 `1Itemheight` 可以自定义选项的高度。目前只支持 `px` 。

<masa-example file="Examples.mobiles.mobile_pickers.ItemHeight"></masa-example>