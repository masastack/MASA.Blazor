---
title: Pull refresh（下拉刷新）
desc: "下拉刷新，通常用于移动端应用"
---

<app-alert type="warning" content="仅在移动端生效，因为只实现了触摸事件。因此需要打开浏览器的移动模式（`F12`，`Ctrl+Shift+M`）才能看到效果。"></app-alert>

## 安装 {#installation released-on=v1.10.0}

```shell
dotnet add package Masa.Blazor.MobileComponents
```

## 使用 {#usage}

<masa-example file="Examples.mobiles.pull_refresh.Usage"></masa-example>

## 示例 {#example}

### 属性 {#props}

#### 禁用 {#disabled}

<masa-example file="Examples.mobiles.pull_refresh.Disabled"></masa-example>

#### 处理错误 {#on-error}

<masa-example file="Examples.mobiles.pull_refresh.OnError"></masa-example>

### 插槽 {#contents}

#### 自定义下拉刷新内容 {#custom-contents}

<masa-example file="Examples.mobiles.pull_refresh.Contents"></masa-example>
