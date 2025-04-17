---
title: Pull refresh（下拉刷新）
desc: "下拉刷新，通常用于移动端应用"
---

<app-alert type="warning" content="仅在移动端生效，因为只实现了触摸事件。因此需要打开浏览器的移动模式（`F12`，`Ctrl+Shift+M`）才能看到效果。"></app-alert>

## 安装 {#installation}

```shell
dotnet add package Masa.Blazor.MobileComponents
```

## 使用

<masa-example file="Examples.mobiles.pull_refresh.Usage"></masa-example>

## 示例

### 属性

#### 禁用

<masa-example file="Examples.mobiles.pull_refresh.Disabled"></masa-example>

#### 处理错误

<masa-example file="Examples.mobiles.pull_refresh.OnError"></masa-example>

### 插槽

#### 自定义下拉刷新内容

<masa-example file="Examples.mobiles.pull_refresh.Contents"></masa-example>
