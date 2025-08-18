---
title: Cells（单元格）
desc: "用于各个类别行的信息展示。"
release: v1.11.0
related: 
  - /blazor/mobiles/swipe-actions
---

## 安装

```shell
dotnet add package Masa.Blazor.MobileComponents
```

## 使用 {#usage}

`Value` 用于展示右侧内容，与 `ChildContent` 互斥。当 `Href` 或 `OnClick` 存在时会显示右侧箭头。

<masa-example file="Examples.mobiles.cell.Usage"></masa-example>

## 示例 {#example}

### 属性 {#props}

#### 边框

<masa-example file="Examples.mobiles.cell.Outlined"></masa-example>

### 其他 {#misc}

#### 微信 {#weixin}

<masa-example file="Examples.mobiles.cell.WeiXin"></masa-example>