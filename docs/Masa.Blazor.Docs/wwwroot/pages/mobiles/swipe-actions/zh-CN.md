---
title: Swipe actions（滑动操作）
desc: "给内容添加滑动操作，常与 [MCell](/blazor/mobiles/cell) 组件一起使用。"
release: v1.11.0
related:
  - /blazor/mobiles/cell
---

## 安装

```shell
dotnet add package Masa.Blazor.MobileComponents
```

## 使用 {#usage}

<masa-example file="Examples.mobiles.swipe_actions.Usage"></masa-example>

## 示例 {#example}

### 属性 {#props}

#### 点击后关闭

默认点击后会自动关闭，适用于不需要等待用户操作的场景。在相反的场景下，可以设置 `CloseOnClick` 为 `false` 后，通过插槽提供的
`SwipeActionContext` 上下文中的 `Close` 方法来手动关闭。

<masa-example file="Examples.mobiles.swipe_actions.CloseOnClick"></masa-example>

### 插槽 {#contents}

#### 默认内容 {#childcontent}

你可以使用除 **MCell** 组件外的任何内容作为默认内容。

<masa-example file="Examples.mobiles.swipe_actions.ChildContent"></masa-example>

#### 左侧按钮 {#leftcontent}

<masa-example file="Examples.mobiles.swipe_actions.LeftContent"></masa-example>
